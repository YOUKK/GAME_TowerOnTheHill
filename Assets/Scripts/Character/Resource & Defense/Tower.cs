using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Character
{
    [SerializeField]
    private Tower_Attacker attacker;

    [SerializeField]
    private int     createResourceTime = 1;
    private bool    isCreating = false;
    private bool    isAttacking = false;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (!isCreating)
        {
            isCreating = true;
            StartCoroutine(CreateResourceCoroutine());
        }
        if (attacker.monsterCount > 0 && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator CreateResourceCoroutine()
    {
        CreateResource();
        yield return new WaitForSeconds(createResourceTime);
        isCreating = false;
    }

    void CreateResource()
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

    IEnumerator AttackCoroutine()
    {
        Debug.Log("Tower Attack animation called"); // Attack Animation
        yield return new WaitForSeconds(status.attackDelay);
        Attack();
        isAttacking = false;
    }

    // ���� ��� : Tower_Attack Ŭ������ �ݶ��̴��� ���Ͱ� ī��Ʈ �Ǹ� ���� ����, ������ ���� �ߴ�.
    public override void Attack()
    {
        List<GameObject> currentMonsterList = attacker.MonsterList;
        foreach (var item in currentMonsterList)
        {
            Debug.Log($"Tower --> {item.name} HIT");
            item.GetComponent<Monster>().Hit(status.strength);
        }
    }
    //attacker�� tirgger���� ���͸� ����(list), triggerExit���� list�� �����ϸ� ���� �ָ� ����
    // attacker�� list�� tower���� �޾ƿͼ� attack�� �� ���.

    // ���ο� ��� : OnTriggerExit�� Ž���� ��ü�� ���������� �� �Ӹ� �ƴ϶� Destroy�Ǿ ȣ��ȴ�.
    

    public override void Hit(int damage)
    {
        if (healthPoint - damage <= 0)
        {
            // �ش� ���� ���� ����
            Destroy(gameObject);
        }
        else
        {
            healthPoint -= damage;
        }
    }
}
