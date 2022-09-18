using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {
    private CircleCollider2D collider2D;
    [SerializeField] private GameObject soundObj;
    [SerializeField] private AudioClip explodeSound;
    public Element element;
    void Start() {
        collider2D = GetComponent<CircleCollider2D>();
        SingleTimeSound soundFX = Instantiate(soundObj, transform.position, Quaternion.identity)
            .GetComponent<SingleTimeSound>();
        soundFX.RandomizePitch(0.25f);
        soundFX.LoadClipAndPlay(explodeSound);
    }

    void Update()
    {
        
    }

    public void ActivateCollider() {
        collider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy.element == element) {
                enemy.Explode();
            }
        }
    }
}
