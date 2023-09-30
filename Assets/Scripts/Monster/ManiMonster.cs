using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiMonster : Monster
{
    [SerializeField]
    private DetectAreaScript detectObj;
    [SerializeField]
    private GameObject monsterPosEffect;
    [SerializeField]
    private GameObject skillEffectPrerfab;
    private GameObject characterPosEffect1; // ��ų ȿ���� �����Ǵ� ��ġ�� ��ų ������Ʈ
    private GameObject characterPosEffect2; // ��ų ȿ���� �����Ǵ� ��ġ�� ��ų ������Ʈ
    [SerializeField]
    private float skillStartTime;

    private bool isSkillUsing = false;
    private bool isSkillUsed = false;
    private float monsterCreatedTime;       // ���Ͱ� ������ �ð�

    protected override void Start()
    {
        base.Start();
        monsterCreatedTime = Time.time;
        monsterPosEffect.SetActive(false);
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
        DisableSkillEffects();
        anim.SetBool("isSkillUsing", false);
        isSkillUsing = false;
        isSkillUsed = true;
    }

    private void EnableSkillEffects(Vector2 seat1, Vector2 seat2)
    {
        monsterPosEffect.SetActive(true);
        monsterPosEffect.GetComponent<ParticleSystem>().Play();
        Vector3 skillPos1 = Map.GetInstance().GetSeatPosition(seat1);
        Vector3 skillPos2 = Map.GetInstance().GetSeatPosition(seat2);

        skillPos1 += Vector3.down * 0.5f;
        skillPos2 += Vector3.down * 0.5f;

        Quaternion newRotation = Quaternion.Euler(-84, 0, 0);

        characterPosEffect1 = Instantiate(skillEffectPrerfab, skillPos1, newRotation);
        characterPosEffect2 = Instantiate(skillEffectPrerfab, skillPos2, newRotation);
        characterPosEffect1.GetComponent<ParticleSystem>().Play();
        characterPosEffect2.GetComponent<ParticleSystem>().Play();
    }

    private void DisableSkillEffects()
    {
        monsterPosEffect.SetActive(false);
        Destroy(characterPosEffect1);
        Destroy(characterPosEffect2);
    }
}
