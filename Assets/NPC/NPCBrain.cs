using UnityEngine;

public class NPCBrain : MonoBehaviour
{
    [SerializeField] private NPCTaskState taskState;

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
        //If no task, choose a new one
        if (taskState.CurrentTask != null)
        {
            taskState.TryStart(PickRandomTask());
        }
    }

    private INPCTask PickRandomTask()
    {
        return new NPCIdleTask(durationMin: 1.5f, durationMax: 4.0f);
    }
}
