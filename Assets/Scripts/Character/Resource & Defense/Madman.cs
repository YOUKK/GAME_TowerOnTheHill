using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madman : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        if (projectile == null)
            return;
    }

    public override void Hit(int damage, Monster attackMonster)
    {
        base.Hit(damage, attackMonster);

        if (healthPoint - damage <= 0)
        {
            attackMonster.Hit(0, AttackType.CRAZY);
        }
    }
}
