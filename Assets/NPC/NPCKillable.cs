using UnityEngine;

public class NPCKillable : MonoBehaviour, IKillable
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D[] collidersToDisable;
    [SerializeField] private Behaviour[] behavioursToDisable;

    public bool IsAlive
    {
        get; private set;
    } = true;

    public void Kill()
    {
        if (!IsAlive)
            return;
        IsAlive = false;

        //Disable everything: movement, interactions, logic, etc.
        if (behavioursToDisable != null)
        {
            foreach (var b in behavioursToDisable)
                if (b != null)
                    b.enabled = false;
        }

        if (collidersToDisable != null)
        {
            foreach (var c in collidersToDisable)
                if (c != null)
                    c.enabled = false;
        }

        //animation (when set)
        if (animator != null)
            animator.SetTrigger("Die");
        Destroy(gameObject);
    }
}
