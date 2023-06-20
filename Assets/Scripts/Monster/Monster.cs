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
    protected bool          isTargetIn;
    protected bool          attackable;
    protected bool          isDead;
    protected MovementState movementState;
    // ���� �Ӵ� ���� ���� �߰�

    [SerializeField]
    protected int           currentHP;
    [SerializeField]
    protected float         currentSpeed;

    protected virtual void Start()
    {
        isTargetIn = false;
        attackable = false;
        isDead = false;
        movementState = MovementState.WALK;

        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInChildren<Animator>();

        currentHP = status.hp;
        currentSpeed = status.speed;
    }

    void Update()
    {
        switch (movementState)
        {
            case MovementState.IDLE:
                {
                    anim.SetBool("isTargetIn", true);
                    break;
                }
            case MovementState.WALK:
                {
                    Move();
                    break;
                }
            case MovementState.ATTACK:
                {
                    anim.SetTrigger("attackTrigger");
                    break;
                }
            case MovementState.DEAD:
                {
                    Dead();
                    break;
                }
            default:
                break;
        }
    }

    protected virtual void Move()
    {
        transform.position = new Vector3(transform.position.x + currentSpeed * (-1) * Time.deltaTime,
            transform.position.y, transform.position.z);
    }

    protected virtual void Attack()
    {
        Character targetCharacter = target.gameObject.GetComponent<Character>();
        if (targetCharacter != null)
            targetCharacter.Hit(status.force);
        else Debug.Log("target doesn't have Character");

        StartCoroutine(AttackCoolCoroutine());
    }

    protected virtual IEnumerator AttackCoolCoroutine()
    {
        // ���� : �����δ� idle ���¿� �����ϸ� cool time���� ��� �� if���� ���� ���� ���°� ����Ǵµ�, �װ� �ȉ�.
        // idle ������ �ٷ� ���� ������.
        movementState = MovementState.IDLE;
        yield return new WaitForSeconds(status.hitSpeed);
        if (target == null) {
            anim.SetBool("isTargetIn", false); 
            movementState = MovementState.WALK;
        }
        else movementState = MovementState.ATTACK;
    }

    protected virtual void Dead()
    {
        anim.SetBool("isDead", true);
        Destroy(gameObject);
    }

    public void Hit(int damage)
    {
        if (currentHP - damage > 0) currentHP -= damage;
        else Dead(); // ���� : Dead �Լ��� ���ϸ��̼� �̺�Ʈ�� ȣ���ϰ�, state�� DEAD�� �ٲٰ� ���ϸ��̼��� �����Ű��.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"{collision.transform.name}");
        if (collision.transform.CompareTag("Character"))
        {
            if (transform.position.x - collision.transform.position.x > status.attackDistance)
            {
                target = collision.transform;
                anim.SetBool("isTargetIn", true);
                movementState = MovementState.ATTACK;
            }
        }
    }
}
