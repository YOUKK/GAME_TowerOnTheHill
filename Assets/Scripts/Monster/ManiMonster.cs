using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiMonster : Monster
{
    [SerializeField]
    private DetectAreaScript detectObj;
    [SerializeField]
    private float skillStartTime;

    private bool isSkillUsing = false;
    private bool isSkillUsed = false;
    private float monsterCreatedTime;       // 몬스터가 생성된 시간

    protected override void Start()
    {
        base.Start();
        monsterCreatedTime = Time.time;
    }

    protected override void Update()
    {
        if (isSkillUsing == false && isSkillUsed == false)
        {
            if (detectObj.IsDetectedCharacter == true || Time.time - monsterCreatedTime > skillStartTime)
            {
                isSkillUsing = true;
                ManipulateCharacterPosition();
                return;
            }
        }

        if (isSkillUsing == false) base.Update();
    }

    private void ManipulateCharacterPosition()
    {
        StartCoroutine(SkillCoroutine());
    }

    private IEnumerator SkillCoroutine()
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
        { random1Idx = 0; random2Idx = 1; }

        // 이팩트 실행
        yield return new WaitForSeconds(1.5f);

        Map.GetInstance().ChangeCharacterSeat(characterOnSeats[random1Idx], characterOnSeats[random2Idx]);
        // 이펙트 종료
    }
}
