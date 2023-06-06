using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //소환 쿨타임 2초
        CoolTime = 2f;
        //공격력 1 (변함 없음)
        //체력 3
        HealthPoint = 50f;
        //투사체 속도 3 
        ProjectileSpeed = 3f;
        //공격 속도  2 
        AttackDelay = 2f;
        //투사체 개수 1 (변함 없음)
        //사거리 10 (최대 사거리)
        Range = 10f;
        attackDuration = 2f;
        Strength = 10.0f;
    }

    public override void Attack()
    {
        projectile = projectiles.Dequeue();
        projectiles.Enqueue(projectile);
        activatedProj.Enqueue(projectile);
        projectile.SetActive(true);

<<<<<<< Updated upstream
        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(ProjectileSpeed, 0) * 100);
        Invoke("Duration", AttackDuration);
=======
        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(projectileSpeed, 0) * 100);
        Invoke("Duration", attackDuration);
>>>>>>> Stashed changes
    }
    void Duration()
    {
        activatedProj.Peek().transform.position = gameObject.transform.position;
        GameObject T = activatedProj.Dequeue();
        T.SetActive(false);
    }
}
