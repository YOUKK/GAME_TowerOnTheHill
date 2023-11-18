using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    public enum AttackPattern { Normal, First, Second, Third, }
    
    private AttackPattern pattern;
    private GameObject firstAttackObj;
    private GameObject secondAttackObj;
    private GameObject thirdAttackObj;
    private List<MonsterSpawnData> monsterSpawns;

    private float patternDuration;
    private bool isMove;

    protected override void Start()
    {
        base.Start();
        pattern = AttackPattern.Normal;
        patternDuration = 5.0f;
        isMove = false;
        StartCoroutine(Think());
    }

    protected override void Update()
    {
        if(isMove)
        {
            Move(currentSpeed);
        }
    }

    private void Idle()
    {
        // Idle animation;
    }

    protected override void Move(float speed)
    {
        base.Move(speed);
    }

    private void ChangeAttackPattern()
    {
        if(currentHP >= (float)status.hp * 2/3)
        {
            pattern = AttackPattern.First;
        }
        else if(currentHP >= (float)status.hp * 1/3)
        {
            pattern = AttackPattern.Second;
        }
        else
        {
            pattern = AttackPattern.Third;
        }
    }

    private IEnumerator Think()
    {
        ChangeAttackPattern();
        // switch문으로 상태 결정
        switch (pattern)
        {
            case AttackPattern.Normal:
                {
                    StartCoroutine(NormalAttackCoroutine());
                    break;
                }
            case AttackPattern.First:
                {
                    StartCoroutine(FirstPatternCoroutine());
                    break;
                }
            case AttackPattern.Second:
                {
                    StartCoroutine(SecondPatternCoroutine());
                    break;
                }
            case AttackPattern.Third:
                {
                    StartCoroutine(ThirdPatternCoroutine());
                    break;
                }
            default:
                break;
        }
        // 각 코루틴 마지막 줄에서 Think 코루틴 다시 실행
        yield return null;
    }

    private IEnumerator NormalAttackCoroutine()
    {
        for(int i = 0; i < 10; ++i)
        {
            isMove = false;
            NormalAttack(); // Attack anim은 하나. Attack 함수는 4개? 어떻게 합치지?
            yield return new WaitForSeconds(2.0f);
            isMove = true;
        }
    }
    private void NormalAttack()
    {
        // TODO : 랜덤한 라인에 몬스터 생성
    }

    private IEnumerator FirstPatternCoroutine()
    {
        yield return null;
        StartCoroutine(Think());
    }

    private IEnumerator SecondPatternCoroutine()
    {
        yield return null;
        StartCoroutine(Think());
    }

    private IEnumerator ThirdPatternCoroutine()
    {
        yield return null;
        StartCoroutine(Think());
    }
}
