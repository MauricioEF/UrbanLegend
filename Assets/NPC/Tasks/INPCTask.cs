public interface INPCTask
{
    string Name
    {
        get;
    }
    int Priority //Useful to determine when a task is higher priority than the other one, to prioritize it in actions
    {
        get;
    }
    void OnEnter(NPCContext context);
    void Tick(NPCContext context, float dt);
    void OnExit(NPCContext context);
    bool IsDone(NPCContext context);
    NPCTaskResult GetResult(NPCContext context);
}

public interface IScoreTaggedTask
{
    NPCActionKind ActionKind
    {
        get;
    }
}