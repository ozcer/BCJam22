using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;
    private Collider2D _collider;

    [SerializeField] private float _initialDashSpeed = 10f;
    [SerializeField] private float _attackDamage = 5f;
    [SerializeField] private GameObject soundObj;
    [SerializeField] private AudioClip attackSound;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void AttackBegin()
    {
        Instantiate(soundObj, transform.position, Quaternion.identity)
            .GetComponent<SingleTimeSound>()
            .LoadClipAndPlay(attackSound);
        _collider.isTrigger = true;

        _rb.velocity = new Vector3(_initialDashSpeed * -1 * transform.localScale.x, 0, 0);

        StartCoroutine(AttackDuration());
    }

    public void AttackEnd()
    {
        _collider.isTrigger = false;
        _rb.bodyType = RigidbodyType2D.Dynamic;

        _anim.SetBool("attacking", false);
    }

    private void OnTrigerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected!");
        }
    }

    private IEnumerator AttackDuration()
    {
        while (Mathf.Abs(_rb.velocity.x) > 0.1f)
        {
            yield return new WaitForSeconds(0.1f);

            _rb.velocity = _rb.velocity * 0.75f;
        }

        AttackEnd();
    }
}
