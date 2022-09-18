using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalProjectile : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject firecoonPrefab;
    public GameObject icecoonPrefab;
    public GameObject grasscoonPrefab;
    public float lifetime = 1f;
    public Vector2 direction;
    [SerializeField] public float speed = 6f;
    [SerializeField] private Sprite fireSprite;
    [SerializeField] private Sprite waterSprite;
    [SerializeField] private Sprite earthSprite;
    private BoxCollider2D collider2D;
    private SpriteRenderer sr;
    public Element element;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        // if (movingRight) rb.velocity = new Vector2(speed, 0);
        // else rb.velocity = new Vector2(-speed, 0);
        rb.velocity = direction * speed;
        rb.AddTorque(100);
        SetElementSprite();
        StartCoroutine(IgniteFuse(lifetime));
    }

    private void Update()
    {
        transform.Rotate (0,0,360*Time.deltaTime);
    }


    // every 2 seconds perform the print()
    private IEnumerator IgniteFuse(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // ExplosionEffect();
        RespawnAsEnemy();
    }

    private void RespawnAsEnemy() {
        Debug.Log("RESPAWNING");
        GameObject enemyPrefab = firecoonPrefab;
        switch (element)
        {
            case Element.Fire:
                enemyPrefab = firecoonPrefab;
                break;
            case Element.Water:
                enemyPrefab = icecoonPrefab;
                break;
            case Element.Earth:
                enemyPrefab = grasscoonPrefab;
                break;
        }
        EnemyController enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<EnemyController>();
        enemy.element = element;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) Physics2D.IgnoreCollision(other, collider2D);
        else if (other.CompareTag("Enemy")) {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (element == enemy.element) {
                enemy.SameElementCollision();
                // ExplosionEffect();
                Destroy(gameObject);
            }
            else
            {
                rb.velocity = -rb.velocity;
            }
            
            Debug.Log("COLLISION");
        }
        else if (other.CompareTag("Wall")) RespawnAsEnemy();
    }
    
    private void SetElementSprite() {
        switch (element) {
            case Element.Water:
                sr.sprite = waterSprite; break;
            case Element.Fire:
                sr.sprite = fireSprite; break;
            case Element.Earth:
                sr.sprite = earthSprite; break;
        }
    }

    private void ExplosionEffect()
    {
        Debug.Log("EXPLOSION");
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
