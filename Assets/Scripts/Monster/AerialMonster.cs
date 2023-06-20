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
            if (!attackable)
            {
                attackable = true;
                StartCoroutine(AttackCoolCoroutine());
            }
        }
        anim.SetBool("isAttack", attackable);
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override IEnumerator AttackCoolCoroutine()
    {
        Attack();
        yield return new WaitForSeconds(status.hitSpeed);
        attackable = false;
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
