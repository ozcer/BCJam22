using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    private List<EnemyController> grabbedEnemies;
    [SerializeField] private ElementalProjectile projectile;
    [SerializeField] private Transform hand;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) ThrowEnemy();
    }

    private void ThrowEnemy()
    {
        GetGrabbedEnemies();

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

    private void GetGrabbedEnemies()
    {
        grabbedEnemies = new List<EnemyController>(hand.GetComponentsInChildren<EnemyController>());
        Debug.Log("Grabbed Enemies = " + grabbedEnemies.Count);
    }
}
