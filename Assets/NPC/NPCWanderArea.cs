using UnityEngine;

public class WanderArea : MonoBehaviour
{
    [SerializeField] private Vector2 center;
    [SerializeField] private Vector2 size = new Vector2(10, 6);

    public Vector2 GetRandomPoint()
    {
        var half = size / 2;
        float x = Random.Range(center.x - half.x, center.x + half.x);
        float y = Random.Range(center.y - half.y, center.y + half.y);
        return new Vector2(x, y);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, size);
    }
#endif
}
