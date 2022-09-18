using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Transform target; 
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D collider;
    private SpriteRenderer sr;
    public Element element;

    [SerializeField] private GameObject explosionPrefab;
    
    [SerializeField] private float horizontalMoveSpeed;
    [SerializeField] private float verticalMoveSpeed;

    [SerializeField] private float horizontalAttackRange = 2f;
    [SerializeField] private float verticalAttackRange = 0.25f;

    private Vector3 _rangeVector;

    [SerializeField] private Transform _playerDetector;

    public bool grabbed = false;
    // public bool bonked = false;
    private bool facingRight;
    
    public bool isInAttackRange;

    private float _dirX;
    private float _dirY;

    private Vector3 scale = new Vector3(1, 1, 1);
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        _rangeVector = new Vector3(horizontalAttackRange, verticalAttackRange, 1);

        if(grabbed) Grabbed();

        target = GameObject.FindWithTag("Player").transform;
        SetElementSprite();
    }

    private void Update() {
        if (!grabbed) {
            transform.localScale = new Vector3(facingRight ? scale.x : -scale.x, scale.y, 1);
        }
        
        // lower sprites on the screen should overlap higher sprites
        sr.sortingOrder = (int) (-transform.position.y * 10);
    }

    private void SetElementSprite() {
        switch (element) {
            case Element.Water:
                sr.color = Color.blue; break;
            case Element.Fire:
                sr.color = Color.red; break;
            case Element.Air:
                sr.color = Color.white; break;
            case Element.Earth:
                sr.color = Color.yellow; break;
        }
    }

    public void ChasePlayer()
    {
        Vector2 positionDelta = target.position - transform.position;
        facingRight = positionDelta.x < 0;

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

    public void SameElementCollision() {
        Debug.Log("SIZED UP");
        float scaleIncrement = 0.4f;
        scale += new Vector3(scaleIncrement, scaleIncrement, 0);

        if (Math.Abs(scale.x) > 3f && Math.Abs(scale.x)> 3f) {
            Explode();
        }
    }

    public void Explode() {
        ExplosionScript explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity)
            .GetComponent<ExplosionScript>();
        explosion.element = element;
        explosion.transform.localScale = scale;
        Destroy(gameObject);
    }

    public void DiffElementCollision() {
        EnterBonkedState();
        
    }

    public void EnterBonkedState() {
        animator.SetBool("bonked", true);
        StartCoroutine(StartBonkTimer(3f));
    }
    
    private IEnumerator StartBonkTimer(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("bonked", false);
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

    public bool IsGrabbable() {
        return scale.x < 1.4f && scale.y < 1.4f;
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
