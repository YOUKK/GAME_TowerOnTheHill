using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { NORMAL, SLOW, STUN, CRAZY, DEAD}

public abstract class Monster : MonoBehaviour
{
    [SerializeField]
    protected MonsterStatus status;
    protected Animator      anim;
    protected Transform     target;
    protected int           currentLine;
    protected float         ignoreDistance;
    protected bool          isAttacking;
    protected bool          isDead;

    [SerializeField]
    protected int           currentHP;
    [SerializeField]
    protected float         currentSpeed;
    [SerializeField]
    protected int           currentForce; 
    [SerializeField]
    public    int           CurrentLine { set => currentLine = value; }

    [SerializeField]
    private ParticleSystem   monsterBuffEffect;

    // 코인 랜덤 생성 퍼센티지
    protected int            randomPercent = 100;
    protected bool           isGetCoin;
    protected GameObject     randomCoin;
    protected SpriteRenderer sprite;

    protected virtual void Start()
    {
        isAttacking = false;
        isDead = false;

        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInChildren<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null) sprite = GetComponentInChildren<SpriteRenderer>();
        randomCoin = Resources.Load<GameObject>("Prefabs/Projectile/Coin");
        if (randomCoin == null) Debug.LogError("Wrond Path Prefab Coin");

        monsterBuffEffect.Stop();

        // 스테이터스 세팅
        ignoreDistance = 0.5f;
        currentHP = status.hp;
        currentSpeed = status.speed;
        currentForce = status.force;
        int ran = Random.Range(0, 100);
        if (ran <= randomPercent) isGetCoin = true;
        else isGetCoin = false;
    }

    protected virtual void Update()
    {
        if (isDead) return;

        if (target == null)
        {
            Move(currentSpeed);
            anim.SetBool("isTargetIn", false);
            isAttacking = false;
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
            targetCharacter.Hit(currentForce, this);
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
        if (isGetCoin)
            Instantiate(randomCoin, transform.position, transform.rotation);

        MonsterSpawner.GetInstance.RemoveMonster(gameObject, currentLine);
        Destroy(gameObject);
    }

    public void Hit(int damage, AttackType type = AttackType.NORMAL)
    {
        if (currentHP - damage > 0)
        {
            StartCoroutine(HittedCoroutine(damage));

            switch (type)
            {
                case AttackType.NORMAL:
                    break;
                case AttackType.SLOW:
                    {
                        // Slow();
                        break;
                    }
                case AttackType.STUN:
                    break;
                case AttackType.CRAZY:
                    break;
                case AttackType.DEAD:
                    break;
                default:
                    break;
            }
        }
        else
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            if (anim != null) anim.SetBool("isDead", true);
            else Dead();
            isDead = true;
        }
    }

    public void Slow(float speed, Character AttackCharacter)
    {
        currentSpeed = status.speed / speed;    
    }

    public void SetLine(int line)
    {
        currentLine = line;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Character"))
        {
            if (transform.position.x - collision.transform.position.x > ignoreDistance)
            {
                target = collision.transform;
            }
        }
    }

    private IEnumerator HittedCoroutine(int damage)
    {
        currentHP -= damage;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.6f);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
    }

    public MonsterType GetMonsterType()
    {
        return status.type;
    }

    public void ChangeStatus(int hp, float speed, int force)
    {
        monsterBuffEffect.Play();
        currentHP += hp;
        currentSpeed += speed;
        currentForce += force;
    }

    public void ChangeStatus()
    {
        monsterBuffEffect.Stop();
        currentSpeed = status.speed;
        currentForce = status.force;
    }
}
