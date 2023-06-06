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
        AttackDuration = 15.0f;
    }

    public override void Attack()
    {
        projectile = projectiles.Dequeue();
        projectiles.Enqueue(projectile);
        activatedProj.Enqueue(projectile);
        //print(projectile.name+","+ projectiles.Count);
        projectile.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(-1f, 1f),
                                                    gameObject.transform.position.y + Random.Range(-1f, 1f));
        projectile.SetActive(true);
        Invoke("Duration", attackDuration);
    }
    void Duration()
    {
        activatedProj.Peek().transform.position = gameObject.transform.position;
        GameObject T = activatedProj.Dequeue();
        T.SetActive(false);
    }
}
