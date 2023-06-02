using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walnut : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //소환 쿨타임 7초
        CoolTime = 7f;
        //공격력 1 (변함 없음)
        //체력 10
        HealthPoint = 200f;
        //투사체 속도 1 (변함 없음)
        //공격 속도 1 (변함 없음)
        //투사체 개수 1 (변함 없음)
        //사거리 1 (변함 없음)
    }

    public override void Attack()
    {
        if (projectile == null)
            return;
    }
}
