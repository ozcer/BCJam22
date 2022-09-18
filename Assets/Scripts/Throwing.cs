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
        for (int i = 0; i < grabbedEnemies.Count; i++) {
            EnemyController enemy = grabbedEnemies[i];
            
            elementMarkerList[i].enabled = true;
            SpriteRenderer sr = elementMarkerList[i].GetComponent<SpriteRenderer>();
            switch (enemy.element) {
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
        
        if (Input.GetMouseButtonDown(0)) ThrowEnemy();
    }

    private void ThrowEnemy()
    {
        if (grabbedEnemies.Count > 0) {
            EnemyController enemy = hand.GetChild(0).GetComponent<EnemyController>();
            Element element = enemy.element;
            Destroy(enemy.gameObject);

            ElementalProjectile elementalProjectile = Instantiate(projectile, hand.position, Quaternion.identity);

            Vector2 mousePos = Camera.main.ScreenToWorldPoint( new Vector2(Input.mousePosition.x, Input.mousePosition.y) );
            Vector2 handPos = hand.position;
            Vector2 direction = mousePos - handPos;
            direction.Normalize();
            elementalProjectile.direction = direction;
            elementalProjectile.element = element;
        }
    }

    public int GetGrabbedCount() {
        return grabbedEnemies.Count;
    }
}
