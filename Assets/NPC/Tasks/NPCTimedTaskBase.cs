using UnityEngine;

public abstract class NPCTimedTaskBase : INPCTask
{
    public abstract string Name
    {
        get;
    }

    public virtual int Priority => 0;

    private readonly float _min;
    private readonly float _max;
    private float _endTime;

    protected NPCTimedTaskBase(float minSeconds, float maxSeconds)
    {
        _min = minSeconds;
        _max = maxSeconds;
    }

    public virtual void OnEnter(NPCContext context)
    {
        float duration = Random.Range(_min, _max);
        _endTime = Time.time + duration;
        context?.Mover?.Stop();
    }

    public virtual void Tick(NPCContext context, float dt)
    {
    }

    public virtual void OnExit(NPCContext context)
    {
    }

    public bool IsDone(NPCContext context) => Time.time >= _endTime;
    public virtual NPCTaskResult GetResult(NPCContext context) => NPCTaskResult.Success;
}
