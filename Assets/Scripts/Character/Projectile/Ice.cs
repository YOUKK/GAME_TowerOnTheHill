using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    private Character mainCharacter;
    Collider2D collision;
    private void Start()
    {
        mainCharacter = gameObject.GetComponentInParent<Character>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" && col.gameObject.GetComponent<Monster>().GetMonsterType() != MonsterType.Aerial)
        {
            col.gameObject.GetComponent<Monster>().Hit((int)mainCharacter.Strength, AttackType.SLOW);
            gameObject.transform.position = mainCharacter.transform.position;
            gameObject.SetActive(false);
        }
    }

}
