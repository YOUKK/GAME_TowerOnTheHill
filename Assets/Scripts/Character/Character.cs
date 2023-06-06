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
