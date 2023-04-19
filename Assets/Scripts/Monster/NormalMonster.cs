using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : Monster
{
    private Animator anim;
    private Transform target;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(target == null) Move();
        else
        {
            if(!isAttack)
            {
                isAttack = true;
                StartCoroutine(AttackCoroutine());
            }
        }
        anim.SetBool("isAttack", isAttack);
    }

    protected override void Move()
    {
        base.Move();
    }

    private IEnumerator AttackCoroutine()
    {
        Debug.Log("Coroutine Start");
        //anim.SetBool("isAttack", true);
        Attack();
        yield return new WaitForSeconds(status.hitSpeed);
        isAttack = false;
        Debug.Log("Coroutine End");
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void Dead()
    {
        base.Dead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.transform.name}");

        if (collision.transform.CompareTag("Character") && 
            transform.position.x >= collision.transform.position.x)
        {
            Debug.Log($"{collision.transform.name}");
            target = collision.transform;
        }
    }
}
