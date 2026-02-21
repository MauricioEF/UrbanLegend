using UnityEngine;

public class NPCTaskState : MonoBehaviour
{
    public INPCTask CurrentTask => _currentTask;
    public string CurrentTaskName => _currentTask.Name ?? "None";

    private INPCTask _currentTask;
    private NPCTaskCancel _cancel;
    private NPCContext _context;

    public void Init(NPCContext context)
    {
        _context = context;
    }

    private void Update()
    {
        if (_context == null)
            return;
        if (_currentTask == null)
            return;

        if (_context.Killable != null && !_context.Killable.IsAlive)
        {
            CancelCurrent("Killed");
            return;
        }
        _currentTask.Tick(_context, Time.deltaTime);
        if (_currentTask.IsDone(_context))
        {
            var result = _currentTask.GetResult(_context);
            SwitchTo(null, $"Task ended: {result}");
        }
    }
    public bool TryStart(INPCTask nextTask)
    {
        if (nextTask == null)
            return false;
        //If no task, start immediately
        if (_currentTask == null)
        {
            SwitchTo(nextTask, "Start");
            return true;
        }
        //Interruption rules
        if (nextTask.Priority < _currentTask.Priority)
            return false;
        SwitchTo(nextTask, "Interrupted by higher/equal priority");
        return true;
    }

    public void CancelCurrent(string reason)
    {
        if (_currentTask == null)
            return;
        _cancel?.Cancel(reason);
        SwitchTo(null, $"Cancelled: {reason}");
    }
    private void SwitchTo(INPCTask nextTask, string reason)
    {
        if (_currentTask != null)
            _currentTask.OnExit(_context);
        _currentTask = nextTask;
        _cancel = new NPCTaskCancel();
        if (_currentTask != null)
        {
            _currentTask.OnEnter(_context);
        }
        Debug.Log($"{CurrentTask} ({reason})");
    }
}