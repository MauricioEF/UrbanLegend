public class NPCSmokeTask : NPCTimedTaskBase, IScoreTaggedTask
{
    public override string Name => "Smoking";

    public NPCSmokeTask(float min, float max) : base(min, max) { }

    public NPCActionKind ActionKind => NPCActionKind.Smoking;
    public override void OnEnter(NPCContext context)
    {
        base.OnEnter(context);
        //Here we could play the animator to make it smoking
    }
}
