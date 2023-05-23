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

    IEnumerator AttackCoroutine()
    {
        Attack();
        yield return new WaitForSeconds(status.hitSpeed);
        isAttack = false;
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
