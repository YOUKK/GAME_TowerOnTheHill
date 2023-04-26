using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartMonster : Monster
{


    void Update()
    {
        if (target == null) Move();
        else
        {
            if (!isAttack)
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

    protected override void Attack()
    {
        base.Attack();
    }

    private IEnumerator AttackCoroutine()
    {
        Debug.Log("Coroutine Start");
        Attack();
        yield return new WaitForSeconds(status.hitSpeed);
        isAttack = false;
        Debug.Log("Coroutine End");
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
