using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartMonster : Monster
{
    [SerializeField]
    private float   changeLineTime;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(ChangeLineCoroutine());
    }

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

    IEnumerator ChangeLineCoroutine()
    {
        Debug.Log("ChangeLineCoroutine start");
        yield return new WaitForSeconds(changeLineTime);
        ChangeLine();
    }

    private void ChangeLine()
    {
        
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
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
}
