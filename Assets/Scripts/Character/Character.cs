using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{ 
    [SerializeField]
    protected GameObject projectile;

    protected float coolTime = 1.0f;                //��ȯ ��Ÿ��
    protected float projectileSpeed = 1.0f;         //����ü �ӵ�
    protected float attackDelay = 1.0f;             //���� �ӵ�
    protected float pAttackDelay = 0f;              //���� �ӵ� �ʱ�ȭ ����
    protected float projectileNum = 1.0f;           //����ü ����
    protected float range = 1.0f;                   //����ü �Ÿ�
    protected float strength = 1.0f;                //���ݷ�
    protected float healthPoint = 1.0f;             //ü��

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
        while(true)
        {
            pAttackDelay += Time.deltaTime;
            if(pAttackDelay >= attackDelay)
            {
                pAttackDelay -= attackDelay;
            }
            yield return new WaitForSeconds(0);
        }
    }

    public float CoolTime           { get => coolTime;          set => coolTime = value;        }
    public float Strength           { get => strength;          set => strength = value;        }
    public float HealthPoint        { get => healthPoint;       set => healthPoint = value;     }
    public float ProjectileSpeed    { get => projectileSpeed;   set => projectileSpeed = value; }
    public float AttackDelay        { get => attackDelay;       set => attackDelay = value;     }
    public float ProjectileNum      { get => projectileNum;     set => projectileNum = value;   }
    public float Range              { get => range;             set => range = value;           }
}
