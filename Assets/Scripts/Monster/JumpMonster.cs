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
    
    // 캐릭터가 빈 간격 없이 라인 따라 나열되어있을 때,
    // Jump몬스터가 패스할 타겟을 지나치는 도중 바로 뒤에 있는 캐릭터를 타겟으로 다시 잡아버려서
    // (거리가 너무 좁아서 앞 타겟을 지나치기도 전에 뒤에 있는 캐릭터랑 Trigger되어서 타겟으로 만듬.)
    // 타겟과 거리가 멀어져도 슬라이딩 모션을 종료하지 못함. (타겟이 계속 뒤에 캐릭터로 갱신됨.)
    // 캐릭터들이 빈 간격 없이 일렬로 나열되어 있으면 빈 간격이 나올 때까지 계속 슬라이딩한다는 뜻.

    // 방안1: 첫 타겟만을 위한 Transform 변수를 따로 만듦. 계속 타겟을 갱신하는 문제를 해결할 수 있음.
    // 방안2: Map의 크기를 키워서 캐릭터들을 띄워놓음.

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
        else // 타겟을 정했다면
        {
            anim.SetBool("isTargetIn", true);

            if (!isJumpTried) // 뛰어넘기를 아직 안 했으면
            {
                isJumping = true;
                jumpTarget = target;
            }

            if(isJumping) // 뛰어넘기 중인 상태면
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
