using UnityEngine;

public class NPCBrain : MonoBehaviour
{
    [SerializeField] private NPCTaskState taskState;
    [SerializeField] private WanderArea wanderArea;

    [Header("Action weights")]
    [SerializeField] private int idleWeight = 4;
    [SerializeField] private int walkPointWeight = 6;
    [SerializeField] private int walkNPCWeight = 3;
    [SerializeField] private int smokeWeight = 2;
    [SerializeField] private int drinkWeight = 2;

    [Header("Durations")]
    [SerializeField] private Vector2 idleDuration = new(1.5f, 4.0f);
    [SerializeField] private Vector2 smokeDuration = new(2.0f, 5.0f);
    [SerializeField] private Vector2 drinkDuration = new(2.0f, 5.0f);

    private NPCContext _context;

    private void Awake()
    {
        var rb = GetComponent<Rigidbody2D>();
        var mover = GetComponent<NPCMover>();
        var identity = GetComponent<NPCIdentity>();
        var killable = GetComponent<NPCKillable>();

        _context = new NPCContext(transform, rb, mover, identity, killable);
        taskState.Init(_context);
    }
    private void Update()
    {

        if (_context.Killable != null && !_context.Killable.IsAlive)
            return;

        if (taskState.CurrentTask == null)
        {
            taskState.TryStart(PickRandomTask());
        }
    }

    private INPCTask PickRandomTask()
    {
        int total = Mathf.Max(0, idleWeight) +
             Mathf.Max(0, walkPointWeight) +
             Mathf.Max(0, walkNPCWeight) +
             Mathf.Max(0, smokeWeight) +
             Mathf.Max(0, drinkWeight);
        ;
        if (total <= 0)
            return new NPCIdleTask(idleDuration.x, idleDuration.y);
        int roll = Random.Range(0, total);
        roll -= Mathf.Max(0, idleWeight);
        if (roll < 0)
            return new NPCIdleTask(idleDuration.x, idleDuration.y);

        roll -= Mathf.Max(0, walkPointWeight);
        if (roll < 0)
            return new NPCWalkToRandomPointTask(wanderArea);
        roll -= Mathf.Max(0, walkNPCWeight);
        if (roll < 0)
            return new NPCWalkToRandomNPCTask();
        roll -= Mathf.Max(0, smokeWeight);
        if (roll < 0)
            return new NPCSmokeTask(smokeDuration.x, smokeDuration.y);

        return new NPCDrinkTask(drinkDuration.x, drinkDuration.y);
    }
}
