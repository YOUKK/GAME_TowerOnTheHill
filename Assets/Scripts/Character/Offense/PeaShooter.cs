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
        HealthPoint = 3f;
        //투사체 속도 1 (변함 없음)
        ProjectileSpeed = 3f;
        //공격 속도  1 (변함 없음)
        AttackDelay = 2f;
        //투사체 개수 1 (변함 없음)
        //사거리 10 (최대 사거리)
        Range = 10f;
    }

    public override void Attack()
    {
        if (projectile == null)
            return;
        
        GameObject Pea = Instantiate(projectile, gameObject.transform);
        Pea.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        Pea.GetComponent<Rigidbody2D>().AddForce(new Vector2(ProjectileSpeed, 0) * 100);
    }
    /*
    public override void Hit()
    {
        // -1로 설정 했지만 타격받은 상대의 공격력에 맞춰서 감소
        HealthPoint -= 1;
        Debug.Log(HealthPoint);

        if(HealthPoint == 0)
        {   
            //체력이 0에 도달하면 0.5초 후에 파괴
            Destroy(gameObject, 0.5f);
        }
    }
    */
}
