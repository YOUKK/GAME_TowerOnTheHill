using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiMonster : Monster
{
    [SerializeField]
    private DetectAreaScript detectObj;
    [SerializeField]
    private GameObject skillEffect;
    private GameObject skillEffectPos1; // ��ų ȿ���� �����Ǵ� ��ġ�� ��ų ������Ʈ
    private GameObject skillEffectPos2; // ��ų ȿ���� �����Ǵ� ��ġ�� ��ų ������Ʈ
    [SerializeField]
    private float skillStartTime;

    private bool isSkillUsing = false;
    private bool isSkillUsed = false;
    private float monsterCreatedTime;       // ���Ͱ� ������ �ð�

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

        // ĳ���Ͱ� �ö�� �ִ� seat�� �ε���
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

        // ����Ʈ ����
        EnableSkillEffects(characterOnSeats[random1Idx], characterOnSeats[random2Idx]);

        yield return new WaitForSeconds(2f);

        Map.GetInstance().ChangeCharacterSeat(characterOnSeats[random1Idx], characterOnSeats[random2Idx]);
        // ����Ʈ ����

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
