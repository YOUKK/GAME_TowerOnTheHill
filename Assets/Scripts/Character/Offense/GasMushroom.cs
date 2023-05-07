using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMushroom : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //소환 쿨타임 5초
        CoolTime = 5f;
        //공격력 1 (변함 없음)
        //체력 4
        HealthPoint = 4f;
        //투사체 속도 1 (변함 없음)
        //공격 속도  4
        AttackDelay = 4f;
        //투사체 개수 1 (변함 없음)
        //사거리 3
        Range = 3f;
        //공격 유지 시간 1.0초
        AttackDuration = 1.0f;
    }

    public override void Attack()
    {
        if (projectile == null)
            return;

        GameObject Pea = Instantiate(projectile, gameObject.transform);
        Pea.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
    }
}
