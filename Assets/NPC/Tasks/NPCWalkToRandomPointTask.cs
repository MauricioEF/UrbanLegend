public class NPCWalkToRandomPointTask : INPCTask, IScoreTaggedTask
{
    public string Name => "WalkRandomPoint";
    public int Priority => 0;

    private readonly WanderArea _area;


    public NPCWalkToRandomPointTask(WanderArea area)
    {
        _area = area;
    }

    private bool _failed;
    private string _failReason;

    public NPCActionKind ActionKind => NPCActionKind.Walking;
    public void OnEnter(NPCContext context)
    {
        if (_area == null)
        {
            _failed = true;
            _failReason = "No wander Area";
            return;
        }
        var destination = _area.GetRandomPoint();
        context.Destination = destination;
        context.CurrentTarget = null;

        context.Mover.MoveToPoint(destination);
        _failed = false;
        _failReason = null;
    }
    public void Tick(NPCContext context, float dt)
    {
    }

    public void OnExit(NPCContext context)
    {
    }

    public bool IsDone(NPCContext context)
    {
        if (_failed)
            return true;
        if (context.Mover.HasFailed)
        {
            _failed = true;
            _failReason = context.Mover.FailureReason;
            return true;
        }
        return context.Mover.HasArrived;
    }

    public NPCTaskResult GetResult(NPCContext context)
    {
        if (_failed)
            return NPCTaskResult.Failed;
        return context.Mover.HasArrived ? NPCTaskResult.Success : NPCTaskResult.Failed;
    }


}
