using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walnut : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //��ȯ ��Ÿ�� 7��
        CoolTime = 7f;
        //���ݷ� 1 (���� ����)
        //ü�� 10
        HealthPoint = 200f;
        //����ü �ӵ� 1 (���� ����)
        //���� �ӵ� 1 (���� ����)
        //����ü ���� 1 (���� ����)
        //��Ÿ� 1 (���� ����)
    }

    public override void Attack()
    {
        if (projectile == null)
            return;
    }
}
