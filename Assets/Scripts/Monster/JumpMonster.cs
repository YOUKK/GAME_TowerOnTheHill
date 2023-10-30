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
        if (isJumping) return;
        if (isDead) return;

        if (target == null)
        {
            if (isJumpTried) Move(currentSpeed);
            else Move(runningSpeed);
            anim.SetBool("isTargetIn", false);
            isAttacking = false;
        }
        else // 타겟을 정했다면
        {
            // 점프 진행
            if(!isJumpTried && !isJumping)
            {
                StartCoroutine(JumpCoroutine());
            }

            anim.SetBool("isTargetIn", true);
            if (!isAttacking)
            {
                anim.SetTrigger("attackTrigger");
                isAttacking = true;
            }
        }
    }

    private IEnumerator JumpCoroutine()
    {
        isJumpTried = true;
        isJumping = true;
        jumpTargetPos = target.position;
        boxCollider.enabled = false;

        while (jumpTargetPos.x - transform.position.x <= delta && target != null)
        {
            Move(jumpSpeed);
            yield return null;
        }

        target = null;
        boxCollider.enabled = true;
        isJumpTried = true;
        isJumping = false;
        anim.SetBool("isJumpTried", true);
        anim.SetBool("isTargetIn", false);
    }
}
