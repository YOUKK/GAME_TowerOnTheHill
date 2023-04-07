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
        //���ݷ� 1 (���� ����)
        //ü�� 3
        HealthPoint = 3f;
        //����ü �ӵ� 1 (���� ����)
        ProjectileSpeed = 3f;
        //���� �ӵ� 1 (���� ����)
        AttackDelay = 2f;
        //����ü ���� 1 (���� ����)
        //��Ÿ� 10 (�ִ� ��Ÿ�)
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
}
