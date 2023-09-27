using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiMonster : Monster
{
    [SerializeField]
    private DetectAreaScript detectObj;
    [SerializeField]
    private GameObject skillEffect;
    private GameObject skillEffectPos1; // 스킬 효과가 생성되는 위치의 스킬 오브젝트
    private GameObject skillEffectPos2; // 스킬 효과가 생성되는 위치의 스킬 오브젝트
    [SerializeField]
    private float skillStartTime;

    private bool isSkillUsing = false;
    private bool isSkillUsed = false;
    private float monsterCreatedTime;       // 몬스터가 생성된 시간

    protected override void Start()
    {
        base.Start();
        monsterCreatedTime = Time.time;
        skillEffect.SetActive(false);
    }

    protected override void Update()
    {
        if (isSkillUsing == false && isSkillUsed == false)
        {
            if (detectObj.IsDetectedCharacter == true || Time.time - monsterCreatedTime > skillStartTime)
            {
                isSkillUsing = true;
                anim.SetBool("isSkillUsing", true);
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
        { 
            random1Idx = 0; random2Idx = 1; 
        }
        else
        {
            anim.SetBool("isSkillUsing", false);
            isSkillUsing = false;
            isSkillUsed = true;
            yield break;
        }

        // 이팩트 실행
        EnableSkillEffects(characterOnSeats[random1Idx], characterOnSeats[random2Idx]);

        yield return new WaitForSeconds(2f);

        Map.GetInstance().ChangeCharacterSeat(characterOnSeats[random1Idx], characterOnSeats[random2Idx]);
        // 이펙트 종료

        anim.SetBool("isSkillUsing", false);
        isSkillUsing = false;
        isSkillUsed = true;
    }

    private void EnableSkillEffects(Vector2 seat1, Vector2 seat2)
    {
        skillEffect.SetActive(true);
        skillEffect.GetComponent<ParticleSystem>().Play();
        Vector3 skillPos1 = Map.GetInstance().GetSeatPosition(seat1);
        Vector3 skillPos2 = Map.GetInstance().GetSeatPosition(seat2);

        Quaternion newRotation = Quaternion.Euler(-84, 0, 0);

        skillEffectPos1 = Instantiate(skillEffect, skillPos1, newRotation);
        skillEffectPos2 = Instantiate(skillEffect, skillPos2, newRotation);
        skillEffectPos1.GetComponent<ParticleSystem>().Play();
        skillEffectPos2.GetComponent<ParticleSystem>().Play();
    }

    private void DisableSkillEffects()
    {
        skillEffect.SetActive(false);
        skillEffectPos1 = null;
        skillEffectPos2 = null;
    }
}
