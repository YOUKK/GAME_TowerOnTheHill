using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCheck : MonoBehaviour
{
    [SerializeField]
    private Character mainCharacter;
    private bool changeFlag = true;
    [SerializeField]
    private GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = transform.GetComponentInParent<Character>();
    }
    private void Update()
    {
        if (changeFlag && mainCharacter.Range != 0)
        {
            transform.position = new Vector2(transform.position.x + ((1 + mainCharacter.Range) / 2),
                                             transform.position.y);
            transform.localScale = new Vector2(mainCharacter.Range, transform.localScale.y / 2);
            changeFlag = false;
        }
    }
    public GameObject Monster { get => monster; set => monster = value; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        try
        {
            Debug.Log(gameObject.GetComponentInParent<Eater>().CanAttack);

            if (gameObject.GetComponentInParent<Eater>().CanAttack && collision.tag == "Enemy")
            {
                monster = collision.gameObject;
            }
        }
        catch
        {
        }
        if (collision.tag == "Enemy")
        {
            mainCharacter.CheckMonster = true;
        }
        else
        {
            mainCharacter.CheckMonster = false;
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            mainCharacter.CheckMonster = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            mainCharacter.CheckMonster = false;
        }
    }*/
}
