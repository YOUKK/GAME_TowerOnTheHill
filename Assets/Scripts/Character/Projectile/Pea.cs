using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pea : MonoBehaviour
{
    private Character mainCharacter;

    [SerializeField]
    private GameObject missEffect;

    private void Start()
    {
        mainCharacter = gameObject.GetComponentInParent<Character>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "Enemy")
        {
            if(collision.gameObject.GetComponent<Monster>().GetMonsterType() == MonsterType.Aerial)
            {
                // ȭ���� ���� ���Ϳ� ������ �� �ƴ� �� ������ ����
                int flag = Random.Range(0, 2);
                if (flag == 0) Instantiate(missEffect, transform.position - Vector3.forward, transform.rotation);
                collision.gameObject.GetComponent<Monster>().Hit((int)mainCharacter.Strength * flag);
            }
            else
            {
                collision.gameObject.GetComponent<Monster>().Hit((int)mainCharacter.Strength);
            }
            gameObject.transform.position = mainCharacter.transform.position;
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        gameObject.transform.position = mainCharacter.transform.position;
    }
}
