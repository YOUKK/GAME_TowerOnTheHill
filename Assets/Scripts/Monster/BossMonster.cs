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
    // 이동에 쓰이는 변수들
    [SerializeField]
    private Transform[] linesPosition; // 이동할 라인 위치
    private int lineIndex = 1;
    private short flag = 1; // 라인 인덱스를 순회할 방향을 결정하는 플래그
    private float totalTime = 5.0f;
    private float elapsedTime = 0.0f;
    private Vector3 initialPosition;
    private bool isMove;

    [SerializeField]
    private float patternDuration;
    // Normal 공격을 할 차례인지를 나타내는 변수
    private bool isNormalAttackTime;
    private bool firstHurt = false;
    private bool secondHurt = false;

    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private GameObject normalAttackObject;
    [SerializeField]
    private GameObject firstAttackObject;
    [SerializeField]
    private GameObject secondAttackObject;
    [SerializeField]
    private GameObject thirdAttackObject;

    protected override void Start()
    {
        base.Start();
        pattern = AttackPattern.Normal;
        isNormalAttackTime = true;
        patternDuration = 5.0f;
        isMove = false;
        transform.position = new Vector3(6.5f, 1, -1);
        initialPosition = transform.position;
        StartCoroutine(Think());
    }

    protected override void Update()
    {
        if (isMove)
        {
            Move(currentSpeed);
        }

        if (currentHP < (float)status.hp * 2 / 3 && firstHurt == false)
        {
            anim.SetTrigger("HurtTrigger");
            firstHurt = true;
        }
        if (currentHP < (float)status.hp * 1 / 3 && secondHurt == false)
        {
            anim.SetTrigger("HurtTrigger");
            secondHurt = true;
        }
    }
    
    private void Idle()
    {
        anim.SetBool("isPatternEnd", true);
        isMove = false;
    }

    protected override void Move(float speed)
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < totalTime)
        {
            float t = elapsedTime / totalTime;
            transform.position = Vector3.Lerp(initialPosition, linesPosition[lineIndex].position, t);
        }
        else
        {
            transform.position = linesPosition[lineIndex].position;
            elapsedTime = 0.0f;
            initialPosition = transform.position;
            // 목적지 라인 변경
            if(lineIndex + flag >= linesPosition.Length)
            {
                flag = -1;
            }
            else if(lineIndex + flag < 0)
            {
                flag = 1;
            }
            lineIndex += flag;
        }
    }

    // 애니메이션 이벤트로 호출하는 함수
    //Walk Animation에 의해 호출
    private void MakeBossMove()
    {
        isMove = true;
    }

    private void ChangeAttackPattern()
    {
        if (isNormalAttackTime)
        {
            pattern = AttackPattern.Normal;
            return;
        }
        // 체력에 따라 실행할 직접 공격 패턴을 결정
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
        Idle();
        yield return new WaitForSeconds(patternDuration);
        anim.SetTrigger("AttackPrepare");
        ChangeAttackPattern();
        anim.SetBool("isPatternEnd", false);
        // switch문으로 상태 결정
        switch (pattern)
        {
            case AttackPattern.Normal:
                {
                    yield return new WaitForSeconds(patternDuration);
                    StartCoroutine(NormalAttackCoroutine());
                    break;
                }
            case AttackPattern.First:
                {
                    yield return new WaitForSeconds(patternDuration);
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

    //Attack animation에 의해 호출
    protected override void Attack()
    {
        switch (pattern)
        {
            case AttackPattern.Normal:
                {
                    NormalAttack();
                    break;
                }
            case AttackPattern.First:
                {
                    FirstAttack();
                    break;
                }
            case AttackPattern.Second:
                {
                    SecondAttack();
                    break;
                }
            case AttackPattern.Third:
                {
                    ThirdAttack();
                    break;
                }
            default:
                break;
        }
    }

    private void NormalAttack()
    {
        // TODO : 랜덤한 라인에 몬스터 생성 (한 공격 당 2마리씩 호출)
        Debug.Log("Normal Attack");
        // Monster spawner에 몬스터 생성 함수 추가.
    }
    private void FirstAttack()
    {
        Debug.Log("First Attack");
    }
    private void SecondAttack()
    {
        Debug.Log("Second Attack");
    }
    private void ThirdAttack()
    {
        Debug.Log("Third Attack");
    }

    private IEnumerator NormalAttackCoroutine()
    {
        for (int i = 0; i < 5; ++i)
        {
            isMove = false;
            anim.SetTrigger("AttackTrigger");
            yield return new WaitForSeconds(4.0f);
        }

        isNormalAttackTime = false;
        anim.SetBool("isPatternEnd", true);
        StartCoroutine(Think());
    }
    private IEnumerator FirstPatternCoroutine()
    {
        isMove = false;
        anim.SetTrigger("AttackTrigger");
        yield return new WaitForSeconds(4.0f);

        isNormalAttackTime = true;
        anim.SetBool("isPatternEnd", true);
        StartCoroutine(Think());
    }
    private IEnumerator SecondPatternCoroutine()
    {
        isMove = false;
        pattern = AttackPattern.First;
        anim.SetTrigger("AttackTrigger");
        yield return new WaitForSeconds(patternDuration);
        pattern = AttackPattern.Second;
        anim.SetTrigger("AttackTrigger");

        yield return new WaitForSeconds(4.0f);
        isNormalAttackTime = true;
        anim.SetBool("isPatternEnd", true);
        StartCoroutine(Think());
    }
    private IEnumerator ThirdPatternCoroutine()
    {
        isMove = false;
        pattern = AttackPattern.Second;
        anim.SetTrigger("AttackTrigger");
        yield return new WaitForSeconds(patternDuration);
        pattern = AttackPattern.Third;
        anim.SetTrigger("AttackTrigger");

        yield return new WaitForSeconds(4.0f);
        isNormalAttackTime = true;
        anim.SetBool("isPatternEnd", true);
        StartCoroutine(Think());
    }

    private void EnableNormalSkillEffects()
    {
        
    }
}
