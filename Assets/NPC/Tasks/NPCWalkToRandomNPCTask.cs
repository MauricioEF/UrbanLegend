public class NPCWalkToRandomNPCTask : INPCTask, IScoreTaggedTask
{
    public string Name => "WalkToRandomNPC";
    public int Priority => 0;

    private NPCIdentity _targetNPC;
    private bool _failed;
    public NPCActionKind ActionKind => NPCActionKind.Walking;

    public void OnEnter(NPCContext context)
    {

        _failed = false;
        _targetNPC = null;

        if (!NPCRegistry.TryGetRandomNPC(context.Identity, out var picked) || picked == null)
        {
            _failed = true;
            return;
        }
        _targetNPC = picked;
        context.CurrentTarget = _targetNPC.transform;
        context.Destination = null;

        context.Mover.FollowTarget(_targetNPC.transform);
    }

    public void Tick(NPCContext context, float dt)
    {
        //no need to calculate tick
    }

    public void OnExit(NPCContext context)
    {
    }

    public bool IsDone(NPCContext context)
    {
        if (_failed)
            return true;
        if (context.Mover.HasFailed)
            return true;
        return context.Mover.HasArrived;
    }
    public NPCTaskResult GetResult(NPCContext context)
    {

        if (_failed)
            return NPCTaskResult.Failed;
        if (context.Mover.HasFailed)
            return NPCTaskResult.Failed;
        if (context.Mover.HasArrived)
            return NPCTaskResult.Success;
        return NPCTaskResult.Failed;
    }
}
