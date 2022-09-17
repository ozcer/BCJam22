using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform myPos;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BoxCollider2D collider2D;
    
    [SerializeField] private float horizontalMoveSpeed;

    [SerializeField] private float verticalMoveSpeed;
    [SerializeField] private float horizontalAttackRange = 2f;
    [SerializeField] private float verticalAttackRange = 0.25f;

    public bool isInAttackRange;

    private float _dirX;

    private float _dirY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ChasePlayer();
    }

    public void ChasePlayer()
    {
        //
        // float xPosDiff = (myPos.position.x + collider2D.size.x/2)
        //                  - (playerPos.position.x + playerPos.GetComponent<BoxCollider2D>().size.x/2);
        // float yPosDiff = myPos.position.y - playerPos.position.y;
        float xPosDiff = (myPos.position.x + collider2D.size.x/2)
            - (playerPos.position.x + playerPos.GetComponent<BoxCollider2D>().size.x/2);
        float yPosDiff = (myPos.position.y + collider2D.size.y/2)
            - (playerPos.position.y + playerPos.GetComponent<BoxCollider2D>().size.y/2);

        if (xPosDiff > 0) _dirX = -1;
        else if (xPosDiff < 0) _dirX = 1;
        else _dirX = 0;
        
        if (yPosDiff > 0) _dirY = -1;
        else if (yPosDiff < 0) _dirY = 1;
        else _dirY = 0;

        if (Math.Abs(xPosDiff) < horizontalAttackRange && Math.Abs(yPosDiff) < verticalAttackRange)
        {
            isInAttackRange = true;
        }
        else
        {
            isInAttackRange = false;
        }
        
        _rb.velocity = new Vector2(_dirX * horizontalMoveSpeed, _dirY * verticalMoveSpeed);

    }

    public void StopMovement()
    {
        _rb.velocity = Vector2.zero;
    }
    
}
