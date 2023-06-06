using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMushroom : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        /*
        //��ȯ ��Ÿ�� 5��
        CoolTime = 5;
        //���ݷ� 1 (���� ����)
        //ü�� 4
        HealthPoint = 4;
        //����ü �ӵ� 1 (���� ����)
        //���� �ӵ�  4
        AttackDelay = 4;
        //����ü ���� 1 (���� ����)
        //��Ÿ� 3
        Range = 3;
        //���� ���� �ð� 1.0��
        AttackDuration = 1;
        */
    }

    public override void Attack()
    {
        if (projectile == null)
            return;
        
        projectile.SetActive(true);
        Invoke("Duration", AttackDuration);
    }
    void Duration()
    {
        projectile.SetActive(false);
    }
}
