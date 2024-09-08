using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;
    private Animator _animator;
    private bool _isGrounded;
    private bool _endedJumpEarly;
    private bool _doJump;
    private Vector2 _velocity;

    [Header("Stats")]
    public float health;
    public int coins;

    [Header("Jumping")]
    public LayerMask playerLayer;
    public float jumpSpeed;
    public float jumpEndEarlyModifier;
    public float maxFallSpeed;
    public float fallAcceleration;
    public float groundCheckDistance;
    public float groundForce;

    [Header("Feedback")]
    [SerializeField] private GameObject _hurtFXPrefab;
    [SerializeField] private AudioClip _hurtFXSound;
    [SerializeField] private GameObject _deathFXPrefab;
    [SerializeField] private AudioClip _deathFXSound;

    private void Start()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _doJump = true;
        }
    }

    private void FixedUpdate()
    {
        _animator.SetBool("run", GameManager.Instance.StartedLevel);
        CheckCollisions();
        if(_doJump && _isGrounded)
        {
            ExecuteJump();
        }

        HandleJump();
        HandleAnimations();
        ApplyMovement();
    }

    private void CheckCollisions()
    {
        bool prev = Physics2D.queriesStartInColliders;
        Physics2D.queriesStartInColliders = false;

        bool groundHit = Physics2D.CapsuleCast(
            _collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.down, groundCheckDistance, ~playerLayer);

        if(!_isGrounded && groundHit)
        {
            _isGrounded = true;
            _endedJumpEarly = false;
            _animator.SetBool("jump", false);

        }
        else if (_isGrounded && !groundHit)
        {
            _isGrounded = false;
        }
        Physics2D.queriesStartInColliders = prev;
    }

    private void HandleJump()
    {
        if (!_isGrounded && !Input.GetButton("Jump") && _rigidbody.velocity.y > 0)
            _endedJumpEarly = true;

        if (_isGrounded && _velocity.y <= 0f)
        {
            _velocity.y = groundForce;
        }
        else
        {
            var fa = fallAcceleration;
            if (_endedJumpEarly && _velocity.y > 0)
                fa *= jumpEndEarlyModifier;

            float mfs = -maxFallSpeed;
            _velocity.y = Mathf.MoveTowards(_velocity.y, mfs, fa * Time.fixedDeltaTime);
        }
    }

    private void HandleAnimations()
    {
        _animator.SetBool("grounded", _isGrounded);
    }

    private void ExecuteJump()
    {
        _velocity.y = jumpSpeed;
        _animator.SetBool("jump", true);
        _doJump = false;
    }

    private void ApplyMovement()
    {
        _rigidbody.velocity = _velocity;
    }

    public void TakeDamage(float damage)
    {
 
        health -= damage;
        if(health <= 0)
        {
            Lose();
        }
        else
        {
            FXManager fXManager = FXManager.Instance;
            fXManager.PlaySound(_hurtFXSound);
            if (_hurtFXPrefab != null)
            {
                Instantiate(_hurtFXPrefab, transform.position, _hurtFXPrefab.transform.rotation);
            }
        }
    }

    public void Collect(int value)
    {
        coins += value;
    }

    private void Lose()
    {
        FXManager fXManager = FXManager.Instance;
        fXManager.PlaySound(_deathFXSound);
        if (_deathFXPrefab != null)
        {
            Instantiate(_deathFXPrefab, transform.position, _deathFXPrefab.transform.rotation);
        }

        GameManager gameManager = GameManager.Instance;
        gameManager.GameOver();
    }
}
