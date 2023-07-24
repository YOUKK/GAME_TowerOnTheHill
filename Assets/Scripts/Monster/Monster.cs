using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    IDLE, WALK, ATTACK, DEAD,
}

public abstract class Monster : MonoBehaviour
{
    [SerializeField]
    protected MonsterStatus status;
    protected Animator      anim;
    protected Transform     target;
    protected int           currentLine;
    protected bool          isAttacking;
    protected bool          isDead;
    // 랜덤 머니 관련 변수 추가

    [SerializeField]
    protected int           currentHP;
    [SerializeField]
    protected float         currentSpeed;

    protected virtual void Start()
    {
        isAttacking = false;
        isDead = false;

        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInChildren<Animator>();

        currentHP = status.hp;
        currentSpeed = status.speed;
    }

    void Update()
    {
        if (target == null && !isDead)
        {
            Move();
            anim.SetBool("isTargetIn", false);
        }
        else
        {
            anim.SetBool("isTargetIn", true);
            if (!isAttacking)
            {
                anim.SetTrigger("attackTrigger");
                isAttacking = true;
            }
        }
    }

    protected virtual void Move()
    {
        transform.position = new Vector3(transform.position.x + currentSpeed * (-1) * Time.deltaTime,
            transform.position.y, transform.position.z);
    }

    protected virtual void Attack() // Animation의 Event에 의해 실행됨.
    {
        Character targetCharacter = target.gameObject.GetComponent<Character>();
        if (targetCharacter != null)
            targetCharacter.Hit(status.force);
        else Debug.Log("target doesn't have Character");

        StartCoroutine(AttackCoolCoroutine());
    }

    protected virtual IEnumerator AttackCoolCoroutine()
    {
        yield return new WaitForSeconds(status.hitSpeed);
        isAttacking = false;
    }

    protected virtual void Dead() // Animation의 Event에 의해 실행됨.
    {
        GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>().RemoveMonster(gameObject, currentLine);
        Destroy(gameObject);
    }

    public void Hit(int damage)
    {
        if (currentHP - damage > 0) currentHP -= damage;
        else { anim.SetBool("isDead", true); isDead = true; }
    }

    public void Slow(float speed, Character AttackCharacter)
    {
        currentSpeed = status.speed - speed;
    }

    public void SetLine(int line)
    {
        currentLine = line;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Character"))
        {
            if (transform.position.x - collision.transform.position.x > status.attackDistance)
            {
                target = collision.transform;
            }
        }
    }
}
