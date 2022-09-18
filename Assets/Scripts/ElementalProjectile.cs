using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalProjectile : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float lifetime = 1f;
    public bool movingRight;
    public Vector2 direction;
    [SerializeField] private float speed = 6f;
    private BoxCollider2D collider2D;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        // if (movingRight) rb.velocity = new Vector2(speed, 0);
        // else rb.velocity = new Vector2(-speed, 0);
        rb.velocity = direction * speed;
        
        StartCoroutine(IgniteFuse(lifetime));
    }
    
    


    // every 2 seconds perform the print()
    private IEnumerator IgniteFuse(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ExplosionEffect();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) Physics2D.IgnoreCollision(other, collider2D);
        else if (other.CompareTag("Enemy"))
        {
            ExplosionEffect();
            Destroy(gameObject);
        }
    }

    private void ExplosionEffect()
    {
        Debug.Log("EXPLOSION");
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
