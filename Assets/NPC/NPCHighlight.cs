using UnityEngine;

public class NPCHighlight : MonoBehaviour, IHighlighteable
{
    [SerializeField] private SpriteRenderer highlightRenderer;

    private void Awake()
    {
        highlightRenderer.enabled = false;
    }

    public void SetHighlighted(bool highlighted)
    {
        if (highlightRenderer == null)
            return;
        highlightRenderer.enabled = highlighted;
    }
}
