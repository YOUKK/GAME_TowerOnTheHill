using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialArrow : MonoBehaviour
{
    [SerializeField]
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
            gameObject.transform.position = mainCharacter.transform.position;
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        gameObject.transform.position = mainCharacter.transform.position;
    }
}
