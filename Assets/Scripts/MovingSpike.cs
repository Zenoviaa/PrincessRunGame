using System;
using UnityEngine;

internal class MovingSpike : MonoBehaviour
{
    private float _swoopTimer;
    private bool _goingUp;
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    public float movementSpeed;
    public bool snaptoGround;

    public LayerMask spikeLayer;


    public bool swooping;
    public float swoopMovementSpeed;
    public float swoopDuration;
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
        if (swooping)
        {
            _swoopTimer += Time.fixedDeltaTime;
            if(_swoopTimer >= swoopDuration)
            {
                _swoopTimer = 0;
                _goingUp = !_goingUp;
            }

            if (_goingUp)
            {
                _velocity.y = Mathf.Lerp(_velocity.y, swoopMovementSpeed, Time.fixedDeltaTime);
            }
            else
            {
                _velocity.y = Mathf.Lerp(_velocity.y, -swoopMovementSpeed, Time.fixedDeltaTime);
            }
        }


        _rigidbody.velocity = _velocity;


        GameManager gameManager = GameManager.Instance;
        if (gameManager.EndedRun)
        {
            _rigidbody.velocity = Vector2.zero;
        }  
    }
}
