using UnityEngine;

public readonly struct NPCStateSnapshot
{
    public readonly string TaskName;
    public readonly Transform Target;

    public NPCStateSnapshot(string taskName, Transform target)
    {
        TaskName = taskName;
        Target = target;
    }
}
