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
    protected float projectileSpeed = 1.0f;         //����ü �ӵ�
    protected float attackDelay = 1.0f;             //���� �ӵ�
    protected float pAttackDelay = 0f;              //���� �ӵ� �ʱ�ȭ ����
    protected float projectileNum = 1.0f;           //����ü ����
    protected float range = 1.0f;                   //����ü �Ÿ�
    protected float strength = 1.0f;                //���ݷ�
    [SerializeField]
    protected float healthPoint = 50f;             //ü��

    IEnumerator AttackCoroutine = null;
<<<<<<< Updated upstream
=======
<<<<<<< HEAD

    protected float attackDuration  = 10.0f;       // ���� ���� �ð� 100��

    public float CoolTime           { get => coolTime; set => coolTime = value; }
    public float Strength           { get => strength; set => strength = value; }
    public float HealthPoint        { get => healthPoint; set => healthPoint = value; }
    public float ProjectileSpeed    { get => projectileSpeed; set => projectileSpeed = value; }
    public float AttackDelay        { get => attackDelay; set => attackDelay = value; }
    public float ProjectileNum      { get => projectileNum; set => projectileNum = value; }
    public float Range              { get => range; set => range = value; }
    public float AttackDuration     { get => attackDuration; set => attackDuration = value; }
=======
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
>>>>>>> parent of f25130f (statusFix)
>>>>>>> Stashed changes

    // Start is called before the first frame update
    protected virtual void Start()
    {
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
            yield return new WaitForSeconds(attackDelay);
            Attack();
        }
    }
}
