public enum NPCTaskResult
{
    Success,
    Failed,
    Cancelled
}
public class NPCTaskCancel
{
    public bool IsCancelled
    {
        get; private set;
    }

    public string Reason
    {
        get; private set;
    }

    public void Cancel(string reason)
    {
        IsCancelled = true;
        Reason = reason;
    }
}
