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
        if (Input.GetMouseButtonDown(0)) {
            if (GetComponent<Throwing>().GetGrabbedCount() >= 1) return;
            GameObject victim = GetGrabbableEnemy();
            if (victim != null) {
                EnemyController enemy = victim.GetComponent<EnemyController>();
                
                if (enemy.IsGrabbable())
                {
                    enemy.Grabbed();
                    victim.transform.SetParent(hand);
                    victim.transform.position = hand.position;
                }
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
