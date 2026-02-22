using UnityEngine;

[DisallowMultipleComponent]
public class NPCMover : MonoBehaviour
{
    public enum MoveMode
    {
        None,
        ToPoint,
        FollowTarget
    }

    [SerializeField] private float speed = 3f;
    [SerializeField] private float stoppingDistance = 0.15f;
    [SerializeField] private float targetRepathInterval = 0.1f;
    [SerializeField] private Rigidbody2D rb;

    public MoveMode Mode
    {
        get;
        private set;
    } = MoveMode.None;

    public bool IsMoving => Mode != MoveMode.None && !_hasArrived && !_hasFailed;
    public bool HasArrived => _hasArrived;
    public bool HasFailed => _hasFailed;
    public string FailureReason => _failureReason;

    public Vector2 CurrentDestination => _destination;

    private Vector2 _destination;
    private Transform _target;

    private bool _hasArrived;
    private bool _hasFailed;
    private string _failureReason;
    private float _nextRepathTime;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Mode == MoveMode.None)
            return;
        if (_hasArrived || _hasFailed)
        {
            ApplyStop();
            return;
        }

        //Update destination if it is following a target
        if (Mode == MoveMode.FollowTarget)
        {
            if (_target == null)
            {
                Fail("Target Became null");
                return;
            }

            //It can't follow dead targets either
            var killable = _target.GetComponentInParent<NPCKillable>();
            if (killable != null && !killable.IsAlive)
            {
                Fail("Target is Dead");
                return;
            }
            if (Time.time >= _nextRepathTime)
            {
                _destination = _target.position;
                _nextRepathTime = Time.time + targetRepathInterval;
            }
        }

        //Arrival check
        var position = rb.position;
        var to = _destination - position;
        var distance = to.magnitude;

        if (distance <= stoppingDistance)
        {
            Arrive();
            return;
        }

        //Move
        var direction = to / distance;
        ApplyVelocity(direction * speed);
    }

    public void MoveToPoint(Vector2 point)
    {
        ResetFlags();
        Mode = MoveMode.ToPoint;
        _target = null;
        _destination = point;
    }

    public void FollowTarget(Transform target)
    {
        ResetFlags();
        Mode = MoveMode.FollowTarget;
        _target = target;
        _destination = target != null ? (Vector2)target.position : rb.position;
        _nextRepathTime = Time.time; // update immediately
    }

    public void Stop()
    {
        Mode = MoveMode.None;
        _target = null;
        _hasArrived = false;
        _hasFailed = false;
        _failureReason = null;
        ApplyStop();
    }
    private void Arrive()
    {
        _hasArrived = true;
        ApplyStop();
    }

    private void Fail(string reason)
    {
        _hasFailed = true;
        _failureReason = reason;
        ApplyStop();
    }

    private void ResetFlags()
    {
        _hasArrived = false;
        _hasFailed = false;
        _failureReason = null;
    }

    private void ApplyVelocity(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
    }

    private void ApplyStop()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
