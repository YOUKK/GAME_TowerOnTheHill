using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{ 
    [SerializeField]
    protected GameObject projectile;
    protected Queue<GameObject> projectiles = new Queue<GameObject>();
    protected Queue<GameObject> activatedProj = new Queue<GameObject>();

    protected float coolTime = 1.0f;                //��ȯ ��Ÿ��
    protected float projectileSpeed = 1.0f;         //���ü �ӵ�
    protected float attackDelay = 1.0f;             //��� �ӵ�
    protected float pAttackDelay = 0f;              //��� �ӵ� �ʱ�ȭ ����
    protected float projectileNum = 1.0f;           //���ü ����
    protected float range = 1.0f;                   //���ü �Ÿ�
    protected float strength = 1.0f;                //��ݷ�
    [SerializeField]
    protected float healthPoint = 50f;             //ü��

    IEnumerator AttackCoroutine = null;
    /*
    protected int attackDuration  = 10;       // 공격 유지 시간 100초
    */
    public int CoolTime           { get => status.coolTime; set => status.coolTime = value; }
    public int Strength           { get => status.strength; set => status.strength = value; }
    public int HealthPoint        { get => status.healthPoint; set => status.healthPoint = value; }
    public float ProjectileSpeed    { get => status.projectileSpeed; set => status.projectileSpeed = value; }
    public float AttackDelay        { get => status.attackDelay; set => status.attackDelay = value; }
    public int ProjectileNum      { get => status.projectileNum; set => status.projectileNum = value; }
    public int Range              { get => status.range; set => status.range = value; }
    public int AttackDuration     { get => status.attackDuration; set => status.attackDuration = value; }

    protected int coolTime;
    protected float projectileSpeed;
    protected float attackDelay;
    protected int pAttackDelay;
    protected int projectileNum;
    protected float range;
    protected int strength;
    protected int healthPoint;
    protected int attackDuration;

    private bool isDragged = false;
    private bool checkMonster = false;
    
    public int CoolTime             { get => coolTime; set => coolTime = value; }
    public int Strength             { get => strength; set => strength = value; }
    public int HealthPoint          { get => healthPoint; set => healthPoint = value; }
    public float ProjectileSpeed    { get => projectileSpeed; set => projectileSpeed = value; }
    public float AttackDelay        { get => attackDelay; set => attackDelay = value; }
    public int ProjectileNum        { get => projectileNum; set => projectileNum = value; }
    public float Range              { get => range; set => range = value; }
    public int AttackDuration       { get => attackDuration; set => attackDuration = value; }
    public bool IsDragged           { get => isDragged; set => isDragged = value; }
    public bool CheckMonster        { get => checkMonster; set => checkMonster = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        coolTime        = status.coolTime       ;
        projectileSpeed = status.projectileSpeed;
        attackDelay     = status.attackDelay    ;
        pAttackDelay    = status.pAttackDelay   ;
        projectileNum   = status.projectileNum  ;
        range           = status.range          * 1.3f;
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
            for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
            {
                projectiles.Enqueue(gameObject.transform.GetChild(0).GetChild(i).gameObject);
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
            yield return new WaitForSeconds(attackDelay);
            Attack();
        }
    }
}
