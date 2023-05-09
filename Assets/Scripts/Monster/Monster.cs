using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField]
    protected MonsterStatus status;
    protected Animator      anim;
    protected Transform     target;
    protected int           lineNumber;
    protected bool          isAttack;
    // 랜덤 머니 관련 변수 추가

    protected int           currentHP;
    protected float         currentSpeed;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInChildren<Animator>();

        currentHP = status.hp;
        currentSpeed = status.speed;
    }



    protected virtual void Move()
    {
        transform.position = new Vector3(transform.position.x + currentSpeed * (-1) * Time.deltaTime,
            transform.position.y, transform.position.z);
    }

    protected virtual void Attack()
    {

    }

    protected virtual void Dead()
    {
        Destroy(gameObject);
    }

    public void Hit(int damage)
    {
        if (currentHP - damage > 0) currentHP -= damage;
        else Dead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.transform.name}");
        if (collision.transform.CompareTag("Character"))
        {
            if (transform.position.x - collision.transform.position.x > status.attackDistance)
            {
                target = collision.transform;
            }
        }
    }
}
