using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{ 
    [SerializeField]
    protected GameObject projectile;
    protected Queue<GameObject> projectiles = new Queue<GameObject>();
    protected Queue<GameObject> activatedProj = new Queue<GameObject>();

    protected float coolTime = 1.0f;                //¼ÒÈ¯ ÄðÅ¸ÀÓ
    protected float projectileSpeed = 1.0f;         //Åõ»çÃ¼ ¼Óµµ
    protected float attackDelay = 1.0f;             //°ø°Ý ¼Óµµ
    protected float pAttackDelay = 0f;              //°ø°Ý ¼Óµµ ÃÊ±âÈ­ º¯¼ö
    protected float projectileNum = 1.0f;           //Åõ»çÃ¼ °³¼ö
    protected float range = 1.0f;                   //Åõ»çÃ¼ °Å¸®
    protected float strength = 1.0f;                //°ø°Ý·Â
    [SerializeField]
    protected float healthPoint = 50f;             //Ã¼·Â

    IEnumerator AttackCoroutine = null;
<<<<<<< Updated upstream
=======
<<<<<<< HEAD

    protected float attackDuration  = 10.0f;       // °ø°Ý À¯Áö ½Ã°£ 100ÃÊ

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
    protected int attackDuration  = 10;       // ê³µê²© ìœ ì§€ ì‹œê°„ 100ì´ˆ
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
