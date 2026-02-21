using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float speed = 5f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 movement = inputReader.Move;
        movement.Normalize();
        _rb.linearVelocity = movement * speed;
    }
}
