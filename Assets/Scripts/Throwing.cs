using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    private List<EnemyController> grabbedEnemies;
    [SerializeField] private ElementalProjectile projectile;
    [SerializeField] private Transform hand;
    [SerializeField] private List<SpriteRenderer> elementMarkerList;

    // Update is called once per frame
    void Update()
    {
        grabbedEnemies = new List<EnemyController>(hand.GetComponentsInChildren<EnemyController>());

        foreach (SpriteRenderer sr in elementMarkerList)
        {
            sr.enabled = false;
        }
        for (int i = 0; i < grabbedEnemies.Count; i++)
        {
            elementMarkerList[i].enabled = true;
        }
        
        if (Input.GetMouseButtonDown(0)) ThrowEnemy();
    }

    private void ThrowEnemy()
    {

        if (grabbedEnemies.Count > 0)
        {
            // Element element = grabbedEnemies[0];
            Destroy(hand.GetChild(0).gameObject);

            ElementalProjectile elementalProjectile = Instantiate(projectile, hand.position, Quaternion.identity);
            Vector3 scale = transform.localScale;
            if (scale.x > 0)
            {
                elementalProjectile.movingRight = true;
            } else if (scale.x < 0)
            {
                elementalProjectile.movingRight = false;
            }
        }
    }
}
