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
        // TODO : ���Ʒ� �̵����� ����
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
        // switch������ ���� ����
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
        // �� �ڷ�ƾ ������ �ٿ��� Think �ڷ�ƾ �ٽ� ����
        yield return null;
    }

    //Attack animation�� ���� ȣ��
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
        // TODO : ������ ���ο� ���� ���� (�� ���� �� 2������ ȣ��)
    }
    private void FirstAttack()
    {

    }
    private void SecondAttack()
    {

    }
    private void ThirdAttack()
    {

    }

    private IEnumerator NormalAttackCoroutine()
    {
        
        for (int i = 0; i < 5; ++i)
        {
            isMove = false;
            anim.SetTrigger("AttackTrigger");
            yield return new WaitForSeconds(1.0f);
            isMove = true;
            yield return new WaitForSeconds(3.0f);
        }
        StartCoroutine(Think());
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
