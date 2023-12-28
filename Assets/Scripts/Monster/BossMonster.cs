using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    public enum AttackPattern { Normal, First, Second, Third, }
    
    private AttackPattern pattern;
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
    private int sortOrderCount = 0;

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

    private GameObject thirdAttackEffect1;
    private GameObject thirdAttackEffect2;

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
            currentLine = lineIndex;
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
                    yield return new WaitForSeconds(patternDuration);
                    StartCoroutine(SecondPatternCoroutine());
                    break;
                }
            case AttackPattern.Third:
                {
                    yield return new WaitForSeconds(patternDuration);
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
        // 무작위 위치에 무작위 몬스터 2마리 소환
        Debug.Log("Normal Attack");
        int randomLine = Random.Range(0, 5);
        int randomMonster = Random.Range(0, 9);
        CreateMonsters(randomLine, randomMonster);
        int randomLine2 = 0;
        do
        {
            randomLine2 = Random.Range(0, 5);
        }
        while (randomLine == randomLine2);
        randomMonster = Random.Range(0, 9);
        CreateMonsters(randomLine2, randomMonster);
    }
    private void FirstAttack()
    {
        Instantiate(firstAttackObject, spawnPoints[currentLine].position, transform.rotation);
    }
    private void SecondAttack()
    {
        Instantiate(secondAttackObject, spawnPoints[currentLine].position, transform.rotation);
    }
    private void ThirdAttack()
    {
        Vector2[] characterOnSeats = Map.GetInstance().GetCharacterPlacedSeats();

        // 캐릭터가 올라와 있는 seat의 인덱스
        int random1Idx = 0;
        int random2Idx = 0;

        if (characterOnSeats.Length > 2)
        {
            random1Idx = Random.Range(0, characterOnSeats.Length - 1);
            do
            {
                random2Idx = Random.Range(0, characterOnSeats.Length - 1);
            }
            while (random1Idx == random2Idx);
        }
        else if (characterOnSeats.Length == 2)
        {
            random1Idx = 0;
            random2Idx = 1;
        }
        else if (characterOnSeats.Length == 1)
        {
            EnableSkillEffects(characterOnSeats[0]);
            Map.GetInstance().GetCharacterInSeat(characterOnSeats[0]).Hit(1000);
            return;
        }
        else return;

        // 이팩트 실행
        EnableSkillEffects(characterOnSeats[random1Idx]);
        EnableSkillEffects(characterOnSeats[random2Idx]);

        // 캐릭터 오브젝트 가져오고 Hit 함수 호출
        Map.GetInstance().GetCharacterInSeat(characterOnSeats[random1Idx]).Hit(1000);
        Map.GetInstance().GetCharacterInSeat(characterOnSeats[random2Idx]).Hit(1000);
        // 이펙트 종료
        //DisableSkillEffects();
    }

    private IEnumerator NormalAttackCoroutine()
    {
        int spawnCount = 0;
        if (secondHurt)
        {
            spawnCount = 5;
        }
        else if (firstHurt)
        {
            spawnCount = 4;
        }
        else
        {
            spawnCount = 2;
        }
        for (int i = 0; i < spawnCount; ++i)
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

    private void CreateMonsters(int line, int monsterNum)
    {
        if (line < 0 || line >= 5 || monsterNum < 0 || monsterNum >= 9)
        {
            Debug.Log("Parameter is not valid");
            return;
        }
        // 이팩트 생성
        Vector3 effectPos = spawnPoints[line].position;
        effectPos += Vector3.down * 0.5f;
        Quaternion effectRot = Quaternion.Euler(-84, 0, 0);
        Instantiate(normalAttackObject, effectPos, effectRot);
        // 몬스터 생성
        GameObject monster = Resources.Load<GameObject>($"Prefabs/Monsters/{(MonsterName)monsterNum}");
        monster.GetComponent<SpriteRenderer>().sortingLayerName = $"Line{line}";
        monster.GetComponent<SpriteRenderer>().sortingOrder = sortOrderCount;
        sortOrderCount++;
        sortOrderCount %= 10;
        Instantiate(monster, spawnPoints[line].position, transform.rotation);
    }

    private void EnableSkillEffects(Vector2 seat)
    {
        Vector3 skillPos = Map.GetInstance().GetSeatPosition(seat);

        skillPos += Vector3.down * 0.5f;

        Quaternion newRotation = Quaternion.Euler(90, 0, 0);

        thirdAttackEffect1 = Instantiate(thirdAttackObject, skillPos, newRotation);
        //thirdAttackEffect1.GetComponent<ParticleSystem>().Play();
        //thirdAttackEffect2.GetComponent<ParticleSystem>().Play();
    }

    private void DisableSkillEffects()
    {
        Destroy(thirdAttackEffect1);
        Destroy(thirdAttackEffect2);
    }
}
