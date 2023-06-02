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
