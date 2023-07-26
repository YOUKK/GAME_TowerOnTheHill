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
    private Transform jumpTarget;

    private bool isJumpTried = false;
    private bool isJumping = false;
    
    // ĳ���Ͱ� �� ���� ���� ���� ���� �����Ǿ����� ��,
    // Jump���Ͱ� �н��� Ÿ���� ����ġ�� ���� �ٷ� �ڿ� �ִ� ĳ���͸� Ÿ������ �ٽ� ��ƹ�����
    // (�Ÿ��� �ʹ� ���Ƽ� �� Ÿ���� ����ġ�⵵ ���� �ڿ� �ִ� ĳ���Ͷ� Trigger�Ǿ Ÿ������ ����.)
    // Ÿ�ٰ� �Ÿ��� �־����� �����̵� ����� �������� ����. (Ÿ���� ��� �ڿ� ĳ���ͷ� ���ŵ�.)
    // ĳ���͵��� �� ���� ���� �Ϸķ� �����Ǿ� ������ �� ������ ���� ������ ��� �����̵��Ѵٴ� ��.

    // ���1: ù Ÿ�ٸ��� ���� Transform ������ ���� ����. ��� Ÿ���� �����ϴ� ������ �ذ��� �� ����.
    // ���2: Map�� ũ�⸦ Ű���� ĳ���͵��� �������.

    protected override void Start()
    {
        base.Start();
        anim.SetBool("isJumpTried", false);
    }

    protected override void Update()
    {
        if (target == null && !isDead)
        {
            if (isJumpTried) Move(currentSpeed);
            else Move(runningSpeed);
            anim.SetBool("isTargetIn", false);
        }
        else // Ÿ���� ���ߴٸ�
        {
            anim.SetBool("isTargetIn", true);

            if (!isJumpTried) // �پ�ѱ⸦ ���� �� ������
            {
                isJumping = true;
                jumpTarget = target;
            }

            if(isJumping) // �پ�ѱ� ���� ���¸�
            {
                ChangeJumpToWalk();
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

    private void ChangeJumpToWalk()
    {
        if(jumpTarget.position.x - transform.position.x > delta)
        {
            Debug.Log("Out of Target Range");
            isJumpTried = true;
            isJumping = false;
            target = null;
            jumpTarget = null;
            anim.SetBool("isJumpTried", true);
            anim.SetBool("isTargetIn", false);
        }
    }
}
