using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : Monster
{
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
}
