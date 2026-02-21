using UnityEngine;

public class NPCContext
{
    public readonly Transform Transform;
    public readonly Rigidbody2D RigidBody;
    //public readonly NPCMover Mover;
    public readonly NPCIdentity Identity;
    public readonly NPCKillable Killable;

    public Transform CurrentTarget; //When NPC 
}
