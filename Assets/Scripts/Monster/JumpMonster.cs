using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMonster : Monster
{
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float runningSpeed;
    [SerializeField]
    private float delta = 0.8f;
    private Vector3 jumpTargetPos;

    private BoxCollider2D boxCollider;
    private bool isJumpTried = false;
    private bool isJumping = false;

    protected override void Start()
    {
        base.Start();
        anim.SetBool("isJumpTried", false);
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected override void Update()
    {
        if (target == null && !isDead)
        {
            if (isJumpTried) Move(currentSpeed);
            else if(isJumping) ChangeJumpToWalk(); 
            else Move(runningSpeed);
            anim.SetBool("isTargetIn", false);
        }
        else // 타겟을 정했다면
        {
            anim.SetBool("isTargetIn", true);

            if (!isJumping) // 뛰어넘기를 아직 안 했으면
            {
                isJumping = true;
                jumpTargetPos = target.position;
            }
            else if(!isJumpTried) // 뛰어넘기 중인 상태면
            {
                boxCollider.enabled = false;
                ChangeJumpToWalk();
                Move(jumpSpeed);
                return;
            }
            else if (!isAttacking)
            {
                anim.SetTrigger("attackTrigger");
                isAttacking = true;
            }
        }
    }

    private void ChangeJumpToWalk()
    {
        if(jumpTargetPos.x - transform.position.x > delta || target == null)
        {
            Debug.Log("Out of Target Range");
            target = null;
            boxCollider.enabled = true;
            isJumpTried = true;
            isJumping = false;
            anim.SetBool("isJumpTried", true);
            anim.SetBool("isTargetIn", false);
        }
    }
}
