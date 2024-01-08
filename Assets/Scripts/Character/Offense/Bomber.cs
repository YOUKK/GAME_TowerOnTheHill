using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Character
{
    public void Bombed()
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(gameObject.transform.position, new Vector2(3, 3), 0);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].tag == "Enemy")
            {
                hit[i].GetComponent<Monster>().Hit(Strength);
            }
        }

        Dead();
    }
}


/*
public class Bomber : Character
{
    Queue<GameObject> bombedEnemy = new Queue<GameObject>();
    private Character mainCharacter;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        //anim.SetTrigger("canAttack");
        Invoke("Duration", attackDuration);
    }
    void Duration()
    {
        //Bomb();
        //anim.SetBool("isDead", true);
        //Invoke("DeadDelay", 2.0f);
    }
    void Bomb()
    {
        for (int i = 0; i < bombedEnemy.Count; i++)
        {
            bombedEnemy.Dequeue().GetComponent<Monster>().Hit((int)mainCharacter.Strength);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            bombedEnemy.Enqueue(collision.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            bombedEnemy.Dequeue();
        }
    }
}
*/