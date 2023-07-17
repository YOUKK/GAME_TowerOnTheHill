using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField]
    protected int           currentForce; 
    public int CurrentLine { set => currentLine = value; }

    protected virtual void Start()
    {
        isAttacking = false;
        isDead = false;

        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInChildren<Animator>();

        currentHP = status.hp;
        currentSpeed = status.speed;
        currentForce = status.force;
    }

    protected virtual void Update()
    {
        if (target == null && !isDead)
        {
            Move(currentSpeed);
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

    protected virtual void Move(float speed)
    {
        transform.position = new Vector3(transform.position.x + speed * (-1) * Time.deltaTime,
            transform.position.y, transform.position.z);
    }

    protected virtual void Attack() // Animation의 Event에 의해 실행됨.
    {
        Character targetCharacter = target.gameObject.GetComponent<Character>();
        if (targetCharacter != null)
            targetCharacter.Hit(currentForce);
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
        MonsterSpawner.GetInstance.RemoveMonster(gameObject, currentLine);
        Destroy(gameObject);
    }

    public void Hit(int damage)
    {
        if (currentHP - damage > 0) currentHP -= damage;
        else { anim.SetBool("isDead", true); isDead = true; }
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

    public MonsterType GetMonsterType()
    {
        return status.type;
    }

    public void ChangeStatus(int hp, float speed, int force)
    {
        currentHP += hp;
        currentSpeed += speed;
        currentForce += force;
    }

    public void ChangeStatus()
    {
        currentSpeed = status.speed;
        currentForce = status.force;
    }
}
