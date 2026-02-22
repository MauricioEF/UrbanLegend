public readonly struct NPCStateSnapshot
{
    public readonly string TaskName;

    public readonly NPCActionKind ActionKind;

    public NPCStateSnapshot(string taskName, NPCActionKind actionKind)
    {
        TaskName = taskName;
        ActionKind = actionKind;
    }
}
