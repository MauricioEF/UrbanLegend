using UnityEngine;

public class NPCAnimatorDriver : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private static readonly int Action = Animator.StringToHash("Action");

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void SetAction(NPCActionKind action)
    {
        animator.SetInteger(Action, (int)action);
    }
}
