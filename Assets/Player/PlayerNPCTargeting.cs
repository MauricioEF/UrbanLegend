using System.Collections.Generic;
using UnityEngine;

public class PlayerNPCTargeting : MonoBehaviour
{
    [SerializeField] private LayerMask npcLayerMask;
    [SerializeField] private Transform playerOrigin;

    private readonly HashSet<Collider2D> _npcsInRange = new();
    private IHighlighteable _currentHighlightedNPC;
    private Transform _currentTargetTransform;

    public IKillable FocusedKillable
    {
        get; private set;
    }
    private void Awake()
    {
        if (playerOrigin == null)
            playerOrigin = transform;
    }

    private void Update()
    {
        var next = SelectBestTarget();
        ApplyHighlight(next);
    }

    private Transform SelectBestTarget()
    {
        Transform best = null;
        float bestRangeScore = float.PositiveInfinity;

        //removeList when iterating
        var toRemove = (List<Collider2D>)null;

        foreach (var col in _npcsInRange)
        {
            if (col == null)
            {
                toRemove ??= new List<Collider2D>();
                toRemove.Add(col);
                continue;
            }

            if (((1 << col.gameObject.layer) & npcLayerMask.value) == 0)
                continue;
            var npcRoot = col.GetComponentInParent<NPCIdentity>();
            if (npcRoot == null)
                continue;

            var killable = npcRoot.GetComponent<IKillable>();
            if (killable == null)
                continue;
            if (!killable.IsAlive)
                continue;
            var npcTransform = npcRoot.transform;
            //calculate Distance Score based on distance + cardinal penalty
            Vector2 delta = (Vector2)(npcTransform.position - playerOrigin.position);
            float distance = delta.magnitude;

            // Cardinal-ness: smaller penalty when close to an axis.
            // Example: if dx is big and dy small => horizontal alignment => small penalty.
            float absX = Mathf.Abs(delta.x);
            float absY = Mathf.Abs(delta.y);
            float offAxis = Mathf.Min(absX, absY);// 0 means perfectly cardinal
            float score = distance + offAxis * 0.25f;

            if (score < bestRangeScore)
            {
                bestRangeScore = score;
                best = npcTransform;
            }
            else if (Mathf.Approximately(score, bestRangeScore) && best != null)
            {
                if (npcTransform.GetInstanceID() < best.GetInstanceID())
                {
                    best = npcTransform;
                }
            }
        }
        if (toRemove != null)
        {
            foreach (var c in toRemove)
            {
                _npcsInRange.Remove(c);
            }
        }
        return best;
    }
    private void ApplyHighlight(Transform nextTarget)
    {
        if (nextTarget == _currentTargetTransform)
            return;
        //turn off old NPC
        if (_currentHighlightedNPC != null)
        {
            _currentHighlightedNPC.SetHighlighted(false);
        }
        _currentHighlightedNPC = null;
        _currentTargetTransform = nextTarget;

        FocusedKillable = _currentTargetTransform != null ? _currentTargetTransform.GetComponent<IKillable>() : null;
        //Now we highlight the new NPC
        if (_currentTargetTransform != null)
        {
            _currentHighlightedNPC = _currentTargetTransform.GetComponentInChildren<IHighlighteable>();
            _currentHighlightedNPC?.SetHighlighted(true);
        }
    }

    public void NotifyEnter(Collider2D other)
    {
        if (other != null)
            _npcsInRange.Add(other);
    }
    public void NotifyExit(Collider2D other)
    {
        if (other != null)
            _npcsInRange.Remove(other);
    }
}
