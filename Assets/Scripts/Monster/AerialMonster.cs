using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialMonster : Monster
{
    void Update()
    {
        if (target == null) Move();
        else
        {
            if (!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(AttackCoolCoroutine());
            }
        }
        anim.SetBool("isAttack", isAttacking);
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override IEnumerator AttackCoolCoroutine()
    {
        Attack();
        yield return new WaitForSeconds(status.hitSpeed);
        isAttacking = false;
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
