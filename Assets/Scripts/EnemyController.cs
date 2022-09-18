using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform target; 
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D collider;
    
    [SerializeField] private float horizontalMoveSpeed;

    [SerializeField] private float verticalMoveSpeed;
    [SerializeField] private float horizontalAttackRange = 2f;
    [SerializeField] private float verticalAttackRange = 0.25f;

    public bool grabbed = false;
    
    public bool isInAttackRange;

    private float _dirX;
    private float _dirY;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        if(grabbed) Grabbed();
    }

    public void ChasePlayer()
    {
        Vector2 positionDelta = target.position - transform.position;

        if (positionDelta.x < 0) _dirX = -1;
        else if (positionDelta.x > 0) _dirX = 1;
        else _dirX = 0;
        
        if (positionDelta.y < 0) _dirY = -1;
        else if (positionDelta.y > 0) _dirY = 1;
        else _dirY = 0;

        if (Math.Abs(positionDelta.x) < horizontalAttackRange && Math.Abs(positionDelta.y) < verticalAttackRange)
        {
            isInAttackRange = true;
        }
        else
        {
            isInAttackRange = false;
        }
        
        rb.velocity = new Vector2(_dirX * horizontalMoveSpeed, _dirY * verticalMoveSpeed);

    }

    public void Grabbed()
    {
        animator.SetBool("grabbed", true);
        rb.bodyType = RigidbodyType2D.Kinematic;
        collider.enabled = false;
        rb.simulated = false;
        
        transform.localScale = new Vector2(0.25f, 0.25f);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }
    
}
