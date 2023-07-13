using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMonster : Monster
{
    [SerializeField]
    private bool jumpTime;

    private bool isJumped = false;
    private bool isJumping = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (target == null && !isDead)
        {
            Move();
            anim.SetBool("isTargetIn", false);
        }
        else
        {
            if(!isJumped)
            {
                isJumping = true;
                // 점프 모션 및 코드
                isJumped = true;
            }
            anim.SetBool("isTargetIn", true);
            if (!isAttacking)
            {
                anim.SetTrigger("attackTrigger");
                isAttacking = true;
            }
        }
    }

    IEnumerator JumpCoroutine()
    {
        yield return new WaitForSeconds(12);
    }
}
