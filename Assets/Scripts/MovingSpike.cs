using UnityEngine;

internal class MovingSpike : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    public float movementSpeed;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _velocity.x = movementSpeed;
        _rigidbody.velocity = _velocity;
    }
}
