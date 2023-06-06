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

    /*
    [SerializeField]
    protected int coolTime = 1;                //소환 쿨타임
    [SerializeField]
    protected int projectileSpeed = 1;         //투사체 속도
    [SerializeField]
    protected int attackDelay = 1;             //공격 속도
    [SerializeField]
    protected int pAttackDelay = 0;            //공격 속도 초기화 변수
    [SerializeField]
    protected int projectileNum = 1;           //투사체 개수
    [SerializeField]
    protected int range = 1;                   //투사체 거리
    [SerializeField]
    protected int strength = 1;                //공격력
    [SerializeField]
    protected int healthPoint = 1;             //체력
    */
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
            yield return new WaitForSeconds(status.attackDelay);
            Attack();
        }
    }
}
