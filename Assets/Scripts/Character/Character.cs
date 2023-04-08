using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{ 
    [SerializeField]
    protected GameObject projectile;

    protected float coolTime = 1.0f;                //소환 쿨타임
    protected float projectileSpeed = 1.0f;         //투사체 속도
    protected float attackDelay = 1.0f;             //공격 속도
    protected float pAttackDelay = 0f;              //공격 속도 초기화 변수
    protected float projectileNum = 1.0f;           //투사체 개수
    protected float range = 1.0f;                   //투사체 거리
    protected float strength = 1.0f;                //공격력
    protected float healthPoint = 1.0f;             //체력

    IEnumerator AttackCoroutine = null;
    private List<IEnumerator> HitCoroutine = new List<IEnumerator>() { };
    private List<Collider2D> destroyCheck = new List<Collider2D>() { };

    private bool collCheckTrigger = false;

    private float pHitDelay = 0f;
    private float hitDelay = 1.0f; // 타격 시간 1초

    public float CoolTime           { get => coolTime; set => coolTime = value; }
    public float Strength           { get => strength; set => strength = value; }
    public float HealthPoint        { get => healthPoint; set => healthPoint = value; }
    public float ProjectileSpeed    { get => projectileSpeed; set => projectileSpeed = value; }
    public float AttackDelay        { get => attackDelay; set => attackDelay = value; }
    public float ProjectileNum      { get => projectileNum; set => projectileNum = value; }
    public float Range              { get => range; set => range = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (AttackCoroutine == null)
        {
            AttackCoroutine = AttackCoolTime();
            StartCoroutine(AttackCoroutine);
        }
    }

    private void Update()
    {
        for(int i = 0; i < HitCoroutine.Count; i++)
        {
            if (collCheckTrigger && !destroyCheck[i] && HitCoroutine[i] != null)
            {
                StopCoroutine(HitCoroutine[i]);
                HitCoroutine[i] = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collCheckTrigger = true;
        destroyCheck.Add(collision);

        HitCoroutine.Add(HitCoolTime());
        StartCoroutine(HitCoroutine[HitCoroutine.Count - 1]);


        for(int i = 0; i < HitCoroutine.Count; i++)
        {
            Debug.Log(HitCoroutine[i]+","+collision.name);
        }
        Debug.Log("JJJJJJJJJJJ");
    }

    public virtual void Attack(){ }
    public virtual void Hit() { }

    IEnumerator AttackCoolTime()
    {
        Debug.Log("Start Attack Coroutine");
        while(true)
        {
            pAttackDelay += Time.deltaTime;
            if(pAttackDelay >= attackDelay)
            {
                Attack();
                pAttackDelay -= attackDelay;
            }
            yield return new WaitForSeconds(0);
        }
    }

    //1초마다 1번씩 타격당하도록 설정
    IEnumerator HitCoolTime()
    {
        Debug.Log("Start Hit Coroutine");
        while(true)
        {
            pHitDelay += Time.deltaTime;
            if (pHitDelay >= hitDelay)
            {
                Hit();
                pHitDelay -= hitDelay;
            }
            yield return new WaitForSeconds(0);
        }
    }
}
