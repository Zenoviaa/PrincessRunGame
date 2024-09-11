using UnityEngine;

internal class MovingSpike : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    public float movementSpeed;
    public bool snaptoGround;
    public LayerMask spikeLayer;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (snaptoGround)
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, 100, ~spikeLayer);
            if (hit)
            {
                transform.position = hit.point;
            }
        }
    }

    private void FixedUpdate()
    {
        _velocity.x = movementSpeed;
        _rigidbody.velocity = _velocity;
    }
}
