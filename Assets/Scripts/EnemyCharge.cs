using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharge : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;

    [SerializeField] private float _chargeTime = 0.5f;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void ChargeBegin()
    {
        _rb.velocity = Vector2.zero;
        _rb.bodyType = RigidbodyType2D.Kinematic;

        StartCoroutine(ChargeDuration());
    }

    private void ChargeEnd()
    {
        _anim.SetBool("charging", false);
        _anim.SetBool("attacking", true);
    }

    private IEnumerator ChargeDuration()
    {
        yield return new WaitForSeconds(_chargeTime);

        ChargeEnd();
    }
}
