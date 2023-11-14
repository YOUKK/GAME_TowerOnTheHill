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

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if(!isAttacking)
        {
            isAttacking = true;
            switch (pattern)
            {
                case AttackPattern.Normal:
                    {
                        break;
                    }
                case AttackPattern.First:
                    {
                        break;
                    }
                case AttackPattern.Second:
                    { 
                        break; 
                    }
                case AttackPattern.Third:
                    { 
                        break; 
                    }
                default:
                    break;
            }
        }
    }

    protected override void Move(float speed)
    {
        base.Move(speed);
    }

    private void ChangeAttackPattern()
    {
        
    }

    private IEnumerator NormalAttackCoroutine()
    {
        yield return null;
    }

    private IEnumerator FirstAttackCoroutine()
    {
        yield return null;
    }

    private IEnumerator SecondAttackCoroutine()
    {
        yield return null;
    }

    private IEnumerator ThirdAttackCoroutine()
    {
        yield return null;
    }
}
