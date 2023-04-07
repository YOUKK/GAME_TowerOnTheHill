using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected float strength = 1.0f;                //공격력
    protected float healthPoint = 1.0f;             //체력

    [SerializeField]
    protected float projectileSpeed = 1.0f;         //투사체 속도
    protected float cooltime = 1.0f;                //공격 속도
    protected float projectileNum = 1.0f;           //투사체 개수
    protected float range = 1.0f;                   //투사체 거리

    IEnumerator AttackCoroutine = null;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (AttackCoroutine == null)
        {
            AttackCoroutine = AttackCoolTime();
            StartCoroutine(AttackCoroutine);
        }
    }
    
    IEnumerator AttackCoolTime()
    {
        Debug.Log("Start Attack Coroutine");
        yield return new WaitForSeconds(0);
    }

    public float Strength           { get => strength;  set => strength = value; }
    public float HealthPoint        { get => healthPoint; set => healthPoint = value; }
    public float ProjectileSpeed    { get => projectileSpeed; set => projectileSpeed = value; }
    public float Cooltime           { get => cooltime; set => cooltime = value; }
    public float ProjectileNum      { get => projectileNum; set => projectileNum = value; }
    public float Range              { get => range; set => range = value; }
}
