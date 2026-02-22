using UnityEngine;

public class NPCContext
{
    public readonly Transform Transform;
    public readonly Rigidbody2D RigidBody;
    public readonly NPCMover Mover;
    public readonly NPCIdentity Identity;
    public readonly NPCKillable Killable;
    public readonly NPCAnimatorDriver AnimatorDriver;

    public Transform CurrentTarget; //When NPC 
    public Vector2? Destination;

    public NPCContext(Transform transform, Rigidbody2D body, NPCMover mover, NPCIdentity identity, NPCKillable killable, NPCAnimatorDriver animatorDriver)
    {
        Transform = transform;
        RigidBody = body;
        Mover = mover;
        Identity = identity;
        Killable = killable;
        AnimatorDriver = animatorDriver;
    }
}
