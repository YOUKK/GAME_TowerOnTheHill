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
        //���� �ӵ� 1 (���� ����)
        //����ü ���� 1 (���� ����)
        //��Ÿ� 10 (�ִ� ��Ÿ�)
        Range = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
