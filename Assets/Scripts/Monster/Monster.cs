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
    // 랜덤 머니 관련 변수 추가

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
                    // 몬스터 동작 상태 코드 수정함.
                    // 이걸 반영해서 에니메이션의 파라미터 새로 바꾸고 적용해야 함.

                    break;
                }
            case MovementState.WALK:
                {
                    Move();
                    break;
                }
            case MovementState.ATTACK:
                {
                    break;
                }
            case MovementState.DEAD:
                {
                    break;
                }
            default:
                break;
        }

        if (target == null)
        {
            
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
        movementState = MovementState.IDLE;
        yield return new WaitForSeconds(status.hitSpeed);
        if (target == null) movementState = MovementState.WALK;
        else movementState = MovementState.ATTACK;
    }

    protected virtual void Dead()
    {
        Destroy(gameObject);
    }

    public void Hit(int damage)
    {
        if (currentHP - damage > 0) currentHP -= damage;
        else Dead(); // 제안 : Dead 함수는 에니메이션 이벤트로 호출하고, state를 DEAD로 바꾸고 에니메이션을 실행시키자.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"{collision.transform.name}");
        if (collision.transform.CompareTag("Character"))
        {
            if (transform.position.x - collision.transform.position.x > status.attackDistance)
            {
                target = collision.transform;
                movementState = MovementState.ATTACK;
                isTargetIn = true;
            }
        }
    }
}
