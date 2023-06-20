using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartMonster : Monster
{
    [SerializeField]
    private float   lineChangeTime;
    private bool    isMovingLine = false;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(ChangeLineCoroutine());
    }

    private void Update()
    {
        if (isMovingLine)
        {
            MoveLine();
            return;
        }

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

    IEnumerator ChangeLineCoroutine()
    {
        Debug.Log("ChangeLineCoroutine start");
        yield return new WaitForSeconds(lineChangeTime);
        
        if (attackable) {
            attackable = false;
            StopCoroutine(AttackCoolCoroutine());
        }

        isMovingLine = true;
        ChangeLine();
    }

    private void ChangeLine()
    {
        int upLineNumber = (currentLine == 4) ? currentLine : currentLine + 1;
        int downLineNumber = (currentLine == 0) ? currentLine : currentLine - 1;
        Line upLine = Map.GetInstance().GetLineInfo(upLineNumber);
        Line downLine = Map.GetInstance().GetLineInfo(downLineNumber);

        // 라인 번호 변경
        currentLine = (upLine.hpSum > downLine.hpSum) ? 
            currentLine = upLineNumber : currentLine = downLineNumber;
    }

    private void MoveLine()
    {
        
    }

    protected override void Move()
    {
        transform.position = new Vector3(transform.position.x + currentSpeed * (-1) * Time.deltaTime,
            transform.position.y, transform.position.z);
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override IEnumerator AttackCoolCoroutine()
    {
        Attack();
        yield return new WaitForSeconds(status.hitSpeed);
        attackable = false;
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
