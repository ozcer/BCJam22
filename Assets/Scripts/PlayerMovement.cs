using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private Animator _anim;

    // [SerializeField] private LayerMask _jumpableGround;

    private float _dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    // private enum MovementState { Idle, Running, Jumping, Falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        _dirX = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(_dirX * moveSpeed, _rb.velocity.y);

        if (Input.GetButtonDown("Jump"))// && IsGrounded())
        {
            // jumpSoundEffect.Play();
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }

        // UpdateAnimationState();
    }

    // private void UpdateAnimationState()
    // {
    //     MovementState state;
    //
    //     if (_dirX > 0f)
    //     {
    //         state = MovementState.Running;
    //         _spriteRenderer.flipX = false;
    //     }
    //     else if (_dirX < 0f)
    //     {
    //         state = MovementState.Running;
    //         _spriteRenderer.flipX = true;
    //     }
    //     else
    //     {
    //         state = MovementState.Idle;
    //     }
    //
    //     if (_rb.velocity.y > .1f)
    //     {
    //         state = MovementState.Jumping;
    //     }
    //     else if (_rb.velocity.y < -.1f)
    //     {
    //         state = MovementState.Falling;
    //     }
    //
    //     _anim.SetInteger("state", (int)state);
    // }

    // private bool IsGrounded()
    // {
    //     return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, .1f, _jumpableGround);
    // }
}