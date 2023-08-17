using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Character
{
    [SerializeField]
    private Tower_Attacker attacker;
    [SerializeField]
    private ParticleSystem attackEffect;
    [SerializeField]
    private ParticleSystem DeadEffect;
    [SerializeField]
    private Sprite halfHealthSprite;

    [SerializeField]
    private int     createResourceTime;
    private bool    isCreating = false;
    private bool    isAttacking = false;

    protected override void Start()
    {
        coolTime = status.coolTime;
        projectileSpeed = status.projectileSpeed;
        attackDelay = status.attackDelay;
        projectileNum = status.projectileNum;
        range = status.range * 1.3f;
        strength = status.strength;
        healthPoint = status.healthPoint;
        attackDuration = status.attackDuration;
        attackEffect.Stop();
        DeadEffect.Stop();

        if (projectile == null)
        {
            for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
            {
                projectiles.Enqueue(gameObject.transform.GetChild(0).GetChild(i).gameObject);
            }
        }
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
        yield return new WaitForSeconds(createResourceTime);
        CreateResource();
        isCreating = false;
    }

    void CreateResource()
    {
        projectile = projectiles.Dequeue();
        projectiles.Enqueue(projectile);
        activatedProj.Enqueue(projectile);
        //print(projectile.name+","+ projectiles.Count);
        projectile.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0, 1.5f),
                                                    gameObject.transform.position.y + Random.Range(0, 1.5f));
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
        //atteckEffect.SetActive(true); // Attack Animation
        yield return new WaitForSeconds(status.attackDelay);
        attackEffect.Play();
        yield return new WaitForSeconds(0.5f);
        Attack();
        isAttacking = false;
    }

    // 공격 방식 : Tower_Attack 클래스의 콜라이더에 몬스터가 카운트 되면 공격 시작, 없으면 공격 중단.
    public override void Attack()
    {
        Debug.Log("Tower : Attack() start");
        GameObject[] currentMonsterList = attacker.MonsterList.ToArray();
        foreach (var item in currentMonsterList)
        {
            Debug.Log($"Tower --> {item.name} HIT");
            item.GetComponent<Monster>().Hit(status.strength);
        }
    }
    //attacker의 tirgger에서 몬스터를 받음(list), triggerExit에서 list를 갱신하며 없는 애를 지움
    // attacker의 list를 tower에서 받아와서 attack할 때 사용.

    // 새로운 사실 : OnTriggerExit은 탐지된 물체가 빠져나가는 것 뿐만 아니라 Destroy되어도 호출된다.
    private IEnumerator DeadCoroutine()
    {
        DeadEffect.Play();
        yield return new WaitForSeconds(0.3f);
        DeadAttack();
        yield return new WaitForSeconds(0.7f);
        MonsterSpawner.GetInstance.BuffMonsters();
        Map.GetInstance().RemoveCharacter(location);
        Destroy(gameObject);
    }

    private void DeadAttack()
    {
        RaycastHit2D[] hittedMonster = Physics2D.RaycastAll(transform.position, Vector2.right, 100f, 1 << 8);
        foreach (var item in hittedMonster)
        {
            item.transform.gameObject.GetComponent<Monster>().Hit(1000);
        }
    }

    public override void Hit(int damage)
    {
        if (healthPoint - damage <= 0)
        {
            // 해당 라인 즉사기 공격
            StartCoroutine(DeadCoroutine());
        }
        else
        {
            healthPoint -= damage;

            if (healthPoint < status.healthPoint / 2)
                gameObject.GetComponent<SpriteRenderer>().sprite = halfHealthSprite;
        }
    }
}
