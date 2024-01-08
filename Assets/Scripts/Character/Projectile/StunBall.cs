using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBall : MonoBehaviour
{
    private Character mainCharacter;
    private void Start()
    {
        mainCharacter = gameObject.GetComponentInParent<Character>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<Monster>().GetMonsterType() == MonsterType.Aerial)
            {
                if ((int)mainCharacter.Strength * Random.Range(0, 2) == 1)
                    collision.gameObject.GetComponent<Monster>().Hit((int)mainCharacter.Strength, AttackType.STUN);
            }
            else
            {
                collision.gameObject.GetComponent<Monster>().Hit((int)mainCharacter.Strength, AttackType.STUN);
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
