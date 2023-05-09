using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //��ȯ ��Ÿ�� 2��
        CoolTime = 2f;
        //���ݷ� 1 (���� ����)
        //ü�� 2
        HealthPoint = 2f;
        //����ü �ӵ� 1 (���� ����)
        //���� �ӵ� 5
        AttackDelay = 5f;
        //����ü ���� 1 (���� ����)
        //��Ÿ� 1 (���� ����)
        //����(�ڿ�)���� �ð� 7��
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
