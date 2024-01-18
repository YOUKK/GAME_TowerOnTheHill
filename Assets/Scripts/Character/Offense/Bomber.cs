using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Character
{
    [SerializeField]
    GameObject deadEffect;

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

    protected override void Dead()
    {
        if(deadEffect)
        {
            Instantiate(deadEffect, transform.position, transform.rotation);
        }
        base.Dead();
    }
}
