using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //��ȯ ��Ÿ�� 2��
        CoolTime = 2f;
        //��ݷ� 1 (���� ���)
        //ü�� 3
        HealthPoint = 50f;
        //���ü �ӵ� 3 
        ProjectileSpeed = 3f;
        //��� �ӵ�  2 
        AttackDelay = 2f;
        //���ü ���� 1 (���� ���)
        //��Ÿ� 10 (�ִ� ��Ÿ�)
        Range = 10f;
        attackDuration = 2f;
        Strength = 10.0f;
    }

    public override void Attack()
    {
        if (!IsDragged && CheckMonster)
        {
            projectile = projectiles.Dequeue();
            projectiles.Enqueue(projectile);
            activatedProj.Enqueue(projectile);
            projectile.SetActive(true);

            projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(ProjectileSpeed, 0) * 100);
            Invoke("Duration", AttackDuration);
        }
    }
    void Duration()
    {
        activatedProj.Peek().transform.position = gameObject.transform.position;
        GameObject T = activatedProj.Dequeue();
        T.SetActive(false);
    }
}
