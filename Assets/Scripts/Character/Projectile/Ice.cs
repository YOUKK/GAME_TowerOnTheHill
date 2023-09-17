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
        if (col.tag == "Enemy")
        {
            collision = col;
            col.gameObject.GetComponent<Monster>().Hit((int)mainCharacter.Strength);
            col.gameObject.GetComponent<Monster>().Slow(2.0f, mainCharacter);
            gameObject.SetActive(false);
        }
    }

}
