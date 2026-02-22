public class NPCDrinkTask : NPCTimedTaskBase, IScoreTaggedTask
{
    public override string Name => "Drinking";

    public NPCDrinkTask(float min, float max) : base(min, max) { }

    public NPCActionKind ActionKind => NPCActionKind.Drinking;
    public override void OnEnter(NPCContext context)
    {
        base.OnEnter(context);
        //Animator to play Drink
    }
}
