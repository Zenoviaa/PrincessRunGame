using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;
    private Animator _animator;
    private bool _isGrounded;
    private bool _endedJumpEarly;
    private bool _doJump;
    private bool _isDead;

    private enum DeathState
    {
        Delayed,
        MarioUp,
        MarioDown
    }
    private DeathState _deathState;
    private float _deathTimer;
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

    [Header("Death Time")]
    public float upDeathTime;
    public float downDeathTime;
    public float deathDelay;
    public float deathUpSpeed;
    public float deathFallSpeed;


    [Header("Feedback")]
    [SerializeField] private GameObject _hurtFXPrefab;
    [SerializeField] private AudioClip _hurtFXSound;
    [SerializeField] private GameObject _deathFXPrefab;
    [SerializeField] private AudioClip _deathFXSound;
    [SerializeField] private int _screenshakePixelStrength;
    [SerializeField] private float _screenshakeDuration;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        if (_isDead)
        {
            HandleDeath();
        }

    }

    private void FixedUpdate()
    {
        if (!_isDead)
        {
            HandleMovement();
        }
    }

    private void HandleDeath()
    {
        _animator.SetBool("death", true);
        float progress;
        float easedProgress;
        switch (_deathState)
        {
            case DeathState.Delayed:
                _collider.isTrigger = true;
                _velocity = Vector2.zero;
                _deathTimer += Time.deltaTime;
                if (_deathTimer >= deathDelay)
                {
                    _deathTimer = 0;
                    _deathState = DeathState.MarioUp;
                }
                break;
            case DeathState.MarioUp:

                _deathTimer += Time.deltaTime;
                progress = _deathTimer / upDeathTime;
                easedProgress = Easing.SpikeOutCirc(progress);
                _velocity.y = Mathf.Lerp(_velocity.y, deathUpSpeed * easedProgress, Time.deltaTime * 15f);
                if (_deathTimer >= upDeathTime)
                {
                    GameManager gameManager = GameManager.Instance;
                    gameManager.GameOver();

                    _deathTimer = 0f;
                    _deathState = DeathState.MarioDown;
                }

                break;
            case DeathState.MarioDown:
                _deathTimer += Time.deltaTime;
                _velocity.y = Mathf.Lerp(_velocity.y, -deathFallSpeed, Time.deltaTime * 5f);
                break;
        }
        ApplyMovement();
    }

    private void HandleMovement()
    {
        _animator.SetBool("run", GameManager.Instance.StartedLevel && !GameManager.Instance.EndedRun);
        CheckCollisions();
        if (_doJump && _isGrounded)
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

    public void ExecuteSuperJump(float jumpSpeedModifier)
    {
        _velocity.y = jumpSpeed * jumpSpeedModifier;
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
            fXManager.Screenshake(_screenshakePixelStrength, _screenshakeDuration);
            fXManager.SpriteFlash(_spriteRenderer);
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

    //the minions are escaping.

    private void Lose()
    {
        
        _isDead = true;

        GameManager gameManager = GameManager.Instance;
        gameManager.EndRun();

         
        FXManager fXManager = FXManager.Instance;
        fXManager.Screenshake(_screenshakePixelStrength, _screenshakeDuration);
        fXManager.SpriteFlash(_spriteRenderer);
        fXManager.PlaySound(_deathFXSound);
        if (_deathFXPrefab != null)
        {
            Instantiate(_deathFXPrefab, transform.position, _deathFXPrefab.transform.rotation);
        }

 
    }
}
