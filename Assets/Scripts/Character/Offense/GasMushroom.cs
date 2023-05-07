using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMushroom : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //��ȯ ��Ÿ�� 5��
        CoolTime = 5f;
        //���ݷ� 1 (���� ����)
        //ü�� 4
        HealthPoint = 4f;
        //����ü �ӵ� 1 (���� ����)
        //���� �ӵ�  4
        AttackDelay = 4f;
        //����ü ���� 1 (���� ����)
        //��Ÿ� 3
        Range = 3f;
        //���� ���� �ð� 1.0��
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
