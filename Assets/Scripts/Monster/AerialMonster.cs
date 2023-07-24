using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialMonster : Monster
{
    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f);
    }

    protected override void Move(float speed)
    {
        base.Move(speed);
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
