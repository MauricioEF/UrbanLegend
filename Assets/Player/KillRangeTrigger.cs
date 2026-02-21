using UnityEngine;

public class KillRangeTrigger : MonoBehaviour
{
    [SerializeField] PlayerNPCTargeting playerTargeting;


    private void Awake()
    {
        if (playerTargeting != null)
        {
            playerTargeting = GetComponentInParent<PlayerNPCTargeting>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerTargeting.NotifyEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerTargeting.NotifyExit(collision);
    }
}
