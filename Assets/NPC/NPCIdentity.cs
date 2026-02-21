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
}
