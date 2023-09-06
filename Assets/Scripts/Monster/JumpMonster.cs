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
        else // Ÿ���� ���ߴٸ�
        {
            anim.SetBool("isTargetIn", true);

            if (!isJumping) // �پ�ѱ⸦ ���� �� ������
            {
                isJumping = true;
                jumpTargetPos = target.position;
            }
            else if(!isJumpTried) // �پ�ѱ� ���� ���¸�
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
