using UnityEngine;

public enum NPCKind
{
    Student
}
public class NPCIdentity : MonoBehaviour
{
    [SerializeField]
    public string NpcKind
    {
        get; private set;
    }
    public NPCKind Kind
    {
        get; private set;
    }

    public Transform Root => transform;

    private void OnEnable()
    {
        NPCRegistry.Register(this);
    }

    private void OnDisable()
    {
        NPCRegistry.Unregister(this);
    }
}
