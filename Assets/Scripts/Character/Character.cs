using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{ 
    [SerializeField]
    protected GameObject projectile;
    [SerializeField]
    protected CharacterStatus status;

    protected Queue<GameObject> projectiles = new Queue<GameObject>();
    protected Queue<GameObject> activatedProj = new Queue<GameObject>();

    IEnumerator AttackCoroutine = null;

    protected int coolTime;
    protected float projectileSpeed;
    protected float attackDelay;
    protected int pAttackDelay;
    protected int projectileNum;
    protected int range;
    protected int strength;
    protected int healthPoint;
    protected int attackDuration;
    
    public int CoolTime           { get => coolTime; set => coolTime = value; }
    public int Strength           { get => strength; set => strength = value; }
    public int HealthPoint        { get => healthPoint; set => healthPoint = value; }
    public float ProjectileSpeed    { get => projectileSpeed; set => projectileSpeed = value; }
    public float AttackDelay        { get => attackDelay; set => attackDelay = value; }
    public int ProjectileNum      { get => projectileNum; set => projectileNum = value; }
    public int Range              { get => range; set => range = value; }
    public int AttackDuration     { get => attackDuration; set => attackDuration = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        coolTime        = status.coolTime       ;
        projectileSpeed = status.projectileSpeed;
        attackDelay     = status.attackDelay    ;
        pAttackDelay    = status.pAttackDelay   ;
        projectileNum   = status.projectileNum  ;
        range           = status.range          ;
        strength        = status.strength       ;
        healthPoint     = status.healthPoint    ;
        attackDuration  = status.attackDuration ;
        if (AttackCoroutine == null)
        {
            AttackCoroutine = AttackCoolTime();
            StartCoroutine(AttackCoroutine);
        }
        if (projectile == null)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                projectiles.Enqueue(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }

    private void Update()
    {
        if(HealthPoint <= 0)
        {
            Debug.Log("Stop Attack Coroutine");
            StopCoroutine(AttackCoroutine);
            Destroy(gameObject);
        }
    }
    public virtual void Attack(){ }

    IEnumerator AttackCoolTime()
    {
        Debug.Log("Start Attack Coroutine");
        while(true)
        {
            yield return new WaitForSeconds(status.attackDelay);
            Attack();
        }
    }
}
