using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Transform target; 
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D collider;
    
    [SerializeField] private float horizontalMoveSpeed;
    [SerializeField] private float verticalMoveSpeed;

    [SerializeField] private float horizontalAttackRange = 2f;
    [SerializeField] private float verticalAttackRange = 0.25f;

    private Vector3 _rangeVector;

    [SerializeField] private Transform _playerDetector;

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

        _rangeVector = new Vector3(horizontalAttackRange, verticalAttackRange, 1);

        if(grabbed) Grabbed();

        target = GameObject.FindWithTag("Player").transform;
    }

    public void ChasePlayer()
    {
        Vector2 positionDelta = target.position - transform.position;

        transform.localScale = new Vector3
        (
            (positionDelta.x < 0) ? 1 : -1,
            1,
            1
        );

        if (positionDelta.x < 0) _dirX = -1;
        else if (positionDelta.x > 0) _dirX = 1;
        else _dirX = 0;
        
        if (positionDelta.y < 0) _dirY = -1;
        else if (positionDelta.y > 0) _dirY = 1;
        else _dirY = 0;

        if (DetectPlayer())
        {
            animator.SetBool("charging", true);
        }
        else
        {
            rb.velocity = new Vector2(_dirX * horizontalMoveSpeed, _dirY * verticalMoveSpeed);
        }
    }

    private bool DetectPlayer()
    {
        if (_playerDetector != null)
        {
            Collider2D[] collidersInRange = Physics2D.OverlapBoxAll(_playerDetector.position, _rangeVector, 0);
            foreach (Collider2D collider in collidersInRange)
            {
                if (collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
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
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (_playerDetector != null)
        {
            Gizmos.DrawWireCube
            (
                _playerDetector.position,
                new Vector3(horizontalAttackRange, verticalAttackRange, 1)
            );
        }
    }
}
