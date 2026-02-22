public class NPCSmokeTask : NPCTimedTaskBase, IScoreTaggedTask
{
    public override string Name => "Smoking";

    public NPCSmokeTask(float min, float max) : base(min, max) { }

    public NPCActionKind ActionKind => NPCActionKind.Smoking;
    public override void OnEnter(NPCContext context)
    {
        base.OnEnter(context);
        context.AnimatorDriver.SetAction(NPCActionKind.Smoking);
    }
}
