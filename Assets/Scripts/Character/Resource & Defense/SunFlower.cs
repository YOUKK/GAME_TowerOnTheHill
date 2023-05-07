using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //소환 쿨타임 2초
        CoolTime = 2f;
        //공격력 1 (변함 없음)
        //체력 2
        HealthPoint = 2f;
        //투사체 속도 1 (변함 없음)
        //공격 속도 5
        AttackDelay = 5f;
        //투사체 개수 1 (변함 없음)
        //사거리 1 (변함 없음)
        //공격(자원)유지 시간 7초
        AttackDuration = 7.0f;
    }

    public override void Attack()
    {
        if (projectile == null)
            return;

        float randomX = gameObject.transform.position.x + Random.Range(-1f, 1f);
        float randomY = gameObject.transform.position.y + Random.Range(-1f, 1f);

        GameObject Sun = Instantiate(projectile, gameObject.transform);
        Sun.transform.position = new Vector2(randomX, randomY);
    }
}
