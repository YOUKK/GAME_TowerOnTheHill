using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
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
            collision.gameObject.GetComponent<Monster>().Hit((int)mainCharacter.Strength);
            collision.gameObject.GetComponent<Monster>().Slow(0.15f, mainCharacter);
            gameObject.SetActive(false);
        }
    }
}
