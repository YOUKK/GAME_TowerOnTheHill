using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected GameObject projectile;
    [SerializeField]
    protected GameObject projectileSecond;
    [SerializeField]
    public CharacterStatus status;
    protected Animator anim;

    protected Queue<GameObject> projectiles = new Queue<GameObject>();
    protected Queue<GameObject> activatedProj = new Queue<GameObject>();

    IEnumerator AttackCoroutine = null;
    [SerializeField]
    protected int coolTime;
    [SerializeField]
    protected float projectileSpeed;
    [SerializeField]
    protected float attackDelay;
    [SerializeField]
    protected int projectileNum;
    [SerializeField]
    protected float range;
    [SerializeField]
    protected int strength;
    [SerializeField]
    protected int healthPoint;
    [SerializeField]
    protected int attackDuration;
    [SerializeField]
    protected CharacterType type;

    private GameObject monster;
    private Monster AttackMonster;

    private bool isDragged = false;
    private bool checkMonster = false;
    public int CoolTime { get => coolTime; set => coolTime = value; }
    public int Strength { get => strength; set => strength = value; }
    public int HealthPoint { get => healthPoint; set => healthPoint = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public float AttackDelay { get => attackDelay; set => attackDelay = value; }
    public int ProjectileNum { get => projectileNum; set => projectileNum = value; }
    public float Range { get => range; set => range = value; }
    public int AttackDuration { get => attackDuration; set => attackDuration = value; }
    public bool IsDragged { get => isDragged; set => isDragged = value; }
    public bool CheckMonster { get => checkMonster; set => checkMonster = value; }

    public GameObject Monster { get => monster; set => monster = value; }

    // seat ����
    protected Vector2 location;
    public Vector2 Location { get => location; set => location = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
            anim = GetComponentInChildren<Animator>();

        type = status.type;
        coolTime = status.coolTime;
        projectileSpeed = status.projectileSpeed;
        attackDelay = status.attackDelay;
        projectileNum = status.projectileNum;
        range = status.range * 1.3f;
        strength = status.strength;
        healthPoint = status.healthPoint;
        attackDuration = status.attackDuration;

        if (AttackCoroutine == null)
        {
            AttackCoroutine = AttackCoolTime();
            StartCoroutine(AttackCoroutine);
        }
        if (projectile == null)
        {
            for (int i = 0; i < gameObject.transform.GetChild(1).childCount; i++)
            {
                projectiles.Enqueue(gameObject.transform.GetChild(1).GetChild(i).gameObject);
            }
        }

        monster = gameObject.GetComponentInChildren<MonsterCheck>().Monster;
    }

    public virtual void Hit(int damage, Monster attackMonster)
    {
        AttackMonster = attackMonster;
        if (healthPoint - damage <= 0)
        {
            anim.SetBool("isDead", true);
            Invoke("DeadDelay", 1.0f);

            Invoke("SlowDelay", AttackDuration);
        }
        else
        {
            healthPoint -= damage;
        }
    }
    public virtual void Hit(int damage)
    {
        if (healthPoint - damage <= 0)
        {
            anim.SetBool("isDead", true);
            Invoke("DeadDelay", 1.0f);

            Invoke("SlowDelay", AttackDuration);
        }
        else
        {
            healthPoint -= damage;
        }
    }
    void SlowDelay()
    {
        AttackMonster.Slow(1, this);
    }

    void DeadDelay()
    {
        Dead();
        // seat ���� ������Ʈ
        Map.GetInstance().RemoveCharacter(location);
    }

    protected void Dead()
    {
        Debug.Log("Stop Attack Coroutine");
        StopCoroutine(AttackCoroutine);
        Destroy(gameObject);
    }

    public virtual void Attack() { }
    private void Update()
    {
        monster = gameObject.GetComponentInChildren<MonsterCheck>().Monster;
    }
    IEnumerator AttackCoolTime()
    {
        Debug.Log("Start Attack Coroutine");

        while (true)
        {
            yield return new WaitForSeconds(attackDelay);
            if (type == CharacterType.Normal && checkMonster)
            {
                if (monster.GetComponent<Monster>().GetMonsterType() != MonsterType.Aerial)
                {
                    Attack();
                }
            }
            if (type == CharacterType.Aerial && checkMonster)
            {
                Attack();
            }
            if (type == CharacterType.UnTouch && checkMonster)
            {
                if (monster.GetComponent<Monster>().GetMonsterType() == MonsterType.Normal)
                    Attack();
            }
        }
    }
}
