using System.Collections.Generic;
using UnityEngine;

public class PlayerNPCTargeting : MonoBehaviour
{
    [SerializeField] private LayerMask npcLayerMask;
    [SerializeField] private Transform playerOrigin;

    private readonly HashSet<Collider2D> _npcsInRange = new();
    private IHighlighteable _currentHighlightedNPC;
    private Transform _currentTargetTransform;

    private void Awake()
    {
        if (playerOrigin == null)
            playerOrigin = transform;
    }

    private void Update()
    {
        var next = SelectBestTarget();
        ApplyHighlight(next);
    }

    private Transform SelectBestTarget()
    {
        Transform best = null;
        float bestRangeScore = float.PositiveInfinity;
        foreach (var col in _npcsInRange)
        {
            if (col == null)
                continue;


            var npcRoot = col.GetComponentInParent<NPCIdentity>();
            if (npcRoot == null)
                continue;
            var npcTransform = npcRoot.transform;
            //calculate Distance Score based on distance + cardinal penalty
            Vector2 delta = (Vector2)(npcTransform.position - playerOrigin.position);
            float distance = delta.magnitude;

            // Cardinal-ness: smaller penalty when close to an axis.
            // Example: if dx is big and dy small => horizontal alignment => small penalty.
            float absX = Mathf.Abs(delta.x);
            float absY = Mathf.Abs(delta.y);
            float offAxis = Mathf.Min(absX, absY);// 0 means perfectly cardinal
            float score = distance + offAxis * 0.25f;

            if (score < bestRangeScore)
            {
                bestRangeScore = score;
                best = npcTransform;
            }
            else if (Mathf.Approximately(score, bestRangeScore) && best != null)
            {
                if (npcTransform.GetInstanceID() < best.GetInstanceID())
                {
                    best = npcTransform;
                }
            }
        }
        return best;
    }
    private void ApplyHighlight(Transform nextTarget)
    {
        if (nextTarget == _currentTargetTransform)
            return;
        //turn off old NPC
        if (_currentHighlightedNPC != null)
        {
            _currentHighlightedNPC.SetHighlighted(false);
        }
        _currentHighlightedNPC = null;
        _currentTargetTransform = nextTarget;

        //Now we highlight the new NPC
        if (_currentTargetTransform != null)
        {
            _currentHighlightedNPC = _currentTargetTransform.GetComponentInChildren<IHighlighteable>();
            _currentHighlightedNPC?.SetHighlighted(true);
        }
    }

    public void NotifyEnter(Collider2D other) => _npcsInRange.Add(other);
    public void NotifyExit(Collider2D other) => _npcsInRange.Remove(other);
}
