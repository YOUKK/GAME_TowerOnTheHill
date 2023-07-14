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

    private bool isJumpTried = false;
    private bool isJumping = false;
    

    protected override void Start()
    {
        base.Start();
        anim.SetBool("isJumpTried", false);
    }

    protected override void Update()
    {
        if (target == null && !isDead)
        {
            Move(currentSpeed);
            anim.SetBool("isTargetIn", false);
        }
        else // Ÿ���� ���ߴٸ�
        {
            anim.SetBool("isTargetIn", true);

            if (!isJumpTried) // �پ�ѱ⸦ ���� �� ������
            {
                Move(runningSpeed);
                isJumping = true;
                return;
            }
            if(isJumping) // �پ�ѱ� ���� ���¸�
            {
                CheckJumpCleared();
                Move(jumpSpeed);
                return;
            }
            if (!isAttacking)
            {
                anim.SetTrigger("attackTrigger");
                isAttacking = true;
            }
        }
    }

    private void CheckJumpCleared()
    {
        Debug.Log("Out of Target Range");
        if(target.position.x - transform.position.x > delta)
        {
            isJumpTried = true;
            isJumping = false;
            target = null;
            anim.SetBool("isJumpTried", true);
            anim.SetBool("isTargetIn", false);
        }
    }
}
