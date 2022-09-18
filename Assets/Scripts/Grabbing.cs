using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    public float grabRange = 10;
    public Transform hand;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GameObject victim = GetGrabbableEnemy();
            if (victim != null)
            {
                victim.GetComponent<EnemyController>().Grabbed();
                victim.transform.SetParent(hand);
                victim.transform.position = hand.position;
                victim.transform.localScale = new Vector2(0.25f, 0.25f);
                victim.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
            }
        }
    }

    public GameObject GetGrabbableEnemy()
    {   
        GameObject nearestEnemy = null;
        float nearestDist = float.PositiveInfinity;
        
        // Get all colliders in grab range and get nearest, not factoring self
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, grabRange);
        foreach (Collider2D _collider in collidersInRange)
        {
            if (!_collider.CompareTag("Enemy") || _collider.GetComponent<EnemyController>().grabbed)
            {
                continue;
            }
            float dist = Vector2.Distance(_collider.transform.position, transform.position);
            if (dist < nearestDist)
            {
                nearestEnemy = _collider.gameObject;
                nearestDist = dist;
            }
        }
        return nearestEnemy;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, grabRange);
    }

}