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
    private bool isPatternCool;

    protected override void Start()
    {
        base.Start();
        pattern = AttackPattern.Normal;
    }

    protected override void Update()
    {
        if(!isPatternCool)
        {
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
        else
        {
            Move(currentSpeed);
        }
    }

    protected override void Move(float speed)
    {
        base.Move(speed);
    }

    private void ChangeAttackPattern()
    {
        if(currentHP >= (float)status.hp * 2/3)
        {

        }
        else if(currentHP >= (float)status.hp * 1/3)
        {

        }
        else
        {

        }
    }

    private IEnumerator WalkCoroutine()
    {
        // Walk animation
        isPatternCool = false;
        yield return null;
    }

    private IEnumerator NormalAttackCoroutine()
    {
        yield return null;
        StartCoroutine(WalkCoroutine());
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
