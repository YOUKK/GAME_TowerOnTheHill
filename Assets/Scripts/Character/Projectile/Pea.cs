using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pea : MonoBehaviour
{
    private Character mainCharacter;

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
                collision.gameObject.GetComponent<Monster>().Hit((int)mainCharacter.Strength * Random.Range(0, 2));
            }
            else
            {
                collision.gameObject.GetComponent<Monster>().Hit((int)mainCharacter.Strength);
            }
            gameObject.transform.position = mainCharacter.transform.position;
            gameObject.SetActive(false);
        }
    }
}
