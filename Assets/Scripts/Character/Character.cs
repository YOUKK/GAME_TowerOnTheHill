using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected GameObject projectile;
    [SerializeField]
    protected CharacterStatus status;
    protected Animator anim;

    protected Queue<GameObject> projectiles = new Queue<GameObject>();
    protected Queue<GameObject> activatedProj = new Queue<GameObject>();

    IEnumerator AttackCoroutine = null;

    protected int coolTime;             
    protected float projectileSpeed;
    protected float attackDelay;
    protected int projectileNum;
    protected float range;
    protected int strength;
    protected int healthPoint;
    protected int attackDuration;

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

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null) 
            anim = GetComponentInChildren<Animator>();



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
            for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
            {
                projectiles.Enqueue(gameObject.transform.GetChild(0).GetChild(i).gameObject);
            }
        }
    }

    private void Update()
    {
    }

    public virtual void Hit(int damage)
    {
        if(healthPoint <= 0)
        {
            anim.SetBool("isDead", true);
            Invoke("DeadDelay", 1.0f);
        }
        else
        {
            healthPoint -= damage;
        }
    }

    void DeadDelay()
    {
        Dead();
    }

    protected void Dead()
    {
        Debug.Log("Stop Attack Coroutine");
        StopCoroutine(AttackCoroutine);
        Destroy(gameObject);
    }

    public virtual void Attack(){ }

    IEnumerator AttackCoolTime()
    {
        Debug.Log("Start Attack Coroutine");
        while(true)
        {
            yield return new WaitForSeconds(attackDelay);
            Attack();
        }
    }
}
