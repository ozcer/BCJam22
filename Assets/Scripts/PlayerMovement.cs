using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    public Animator _anim;

    // [SerializeField] private LayerMask _jumpableGround;

    private float _dirX = 0f;
    private float _dirY = 0f;
    [SerializeField] private float horizontalMoveSpeed = 5f;
    [SerializeField] private float verticalMoveSpeed = 3.5f;
    // [SerializeField] private float jumpForce = 14f;

    // private enum MovementState { Idle, Running, Jumping, Falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        _dirX = Input.GetAxisRaw("Horizontal");
        _dirY = Input.GetAxisRaw("Vertical");
        _rb.velocity = new Vector2(_dirX * horizontalMoveSpeed, _dirY * verticalMoveSpeed);

        // if (Input.GetButtonDown("Jump"))// && IsGrounded())
        // {
        //     // jumpSoundEffect.Play();
        //     _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        // }
        _anim.SetFloat("speed", _rb.velocity.magnitude);
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        // MovementState state;
        var transformLocalScale = transform.localScale;

        if (_dirX > 0f)
        {
            // state = MovementState.Running;
            // _spriteRenderer.flipX = false;
            transformLocalScale.x = Mathf.Abs(transformLocalScale.x);
        }
        else if (_dirX < 0f)
        {
            // state = MovementState.Running;
            // _spriteRenderer.flipX = true;
            transformLocalScale.x = -Mathf.Abs(transformLocalScale.x);
        }
        transform.localScale = transformLocalScale;

        // else
        // {
        //     state = MovementState.Idle;
        // }
    
        // if (_rb.velocity.y > .1f)
        // {
        //     state = MovementState.Jumping;
        // }
        // else if (_rb.velocity.y < -.1f)
        // {
        //     state = MovementState.Falling;
        // }
        //
        // _anim.SetInteger("state", (int)state);
    }

    // private bool IsGrounded()
    // {
    //     return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, .1f, _jumpableGround);
    // }
}