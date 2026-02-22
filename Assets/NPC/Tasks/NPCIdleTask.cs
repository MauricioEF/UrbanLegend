using UnityEngine;

public class NPCIdleTask : INPCTask, IScoreTaggedTask
{
    public string Name => "Idle";
    public int Priority => 0;

    private readonly float _min;
    private readonly float _max;
    private float _endTime;

    public NPCActionKind ActionKind => NPCActionKind.Idle;

    public NPCIdleTask(float durationMin, float durationMax)
    {
        _min = durationMin;
        _max = durationMax;
    }

    public void OnEnter(NPCContext context)
    {
        float duration = Random.Range(_min, _max);
        _endTime = Time.time + duration;
        context.Mover?.Stop();
        //Here we can set the idle animation
    }

    public void Tick(NPCContext context, float dt)
    {
        //Idle doesn't have to do anything per frame
    }

    public void OnExit(NPCContext context)
    {
        // Here we can take the animator and set it Idle to false 'context.Animator.SetIdle(false)'
    }

    public bool IsDone(NPCContext context) => Time.time >= _endTime;
    public NPCTaskResult GetResult(NPCContext context) => NPCTaskResult.Success;
}
