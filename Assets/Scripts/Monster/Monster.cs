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
    private   bool          isCrazy = false; 

    [SerializeField]
    protected int           currentHP;
    [SerializeField]
    protected float         currentSpeed;
    [SerializeField]
    protected int           currentForce; 
    [SerializeField]
    public    int           CurrentLine { set => currentLine = value; }

    [SerializeField]
    private ParticleSystem  monsterBuffEffect;
    private SpriteRenderer  sprite;

    // 코인 랜덤 생성 퍼센티지
    protected int           randomPercent = 35;
    protected bool          isGetCoin;
    protected GameObject    randomCoin;

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
        if (isCrazy)
        {
            Monster targetMonster = target.gameObject.GetComponent<Monster>();

            if (targetMonster != null)
                targetMonster.Hit(currentForce);
            else Debug.Log("target doesn't have Character");

            StartCoroutine(AttackCoolCoroutine());
        }
        else
        {
            Character targetCharacter = target.gameObject.GetComponent<Character>();

            Monster targetMonster = target.gameObject.GetComponent<Monster>();

            if (targetMonster != null)
            {
                targetMonster.Hit(currentForce);
            }

            if (targetCharacter != null)
                targetCharacter.Hit(currentForce, this);
            else Debug.Log("target doesn't have Character");

            StartCoroutine(AttackCoolCoroutine());
        }
    }

    protected virtual IEnumerator AttackCoolCoroutine()
    {
        yield return new WaitForSeconds(status.hitSpeed);
        isAttacking = false;
    }

    protected virtual void Dead() // Animation의 Event에 의해 실행됨.
    {
        SoundManager.Instance.PlayEffect("MonsterDeath");
        if (isGetCoin)
            Instantiate(randomCoin, transform.position, transform.rotation);

        // 보스전(3-5)에서는 MonsterSpawner을 사용하지 않는다.
        if (GamePlayManagers.Instance.GetCurrentPS.stage != 5 &&
            GamePlayManagers.Instance.GetCurrentPS.phase != 3)
        {
            MonsterSpawner.GetInstance.RemoveMonster(gameObject, currentLine);
        }
        Destroy(gameObject);
    }

    public void Hit(int damage, AttackType type = AttackType.NORMAL)
    {
        SoundManager.Instance.PlayEffect("Hit");

        if (currentHP - damage > 0)
        {
            StartCoroutine(HittedCoroutine(damage));

            switch (type)
            {
                case AttackType.NORMAL:
                    break;
                case AttackType.SLOW: // 얼음 캐릭터
                    {
                        Slow();
                        break;
                    }
                case AttackType.STUN: // 스턴 캐릭터
                    {
                        Stun();
                        break;
                    }
                case AttackType.CRAZY: // 최면 버섯 캐릭터
                    {
                        Crazy();
                        break;
                    }
                case AttackType.DEAD: // 즉사 공격
                    {
                        Dead();
                        break;
                    }
                default:
                    break;
            }
        }
        else
        {
            Destroy(gameObject.GetComponent<Collider2D>());
            if (anim != null)
            {
                anim.SetBool("isDead", true);
            }
            isDead = true;
        }
    }

    public void Crazy()
    {
        currentSpeed = status.speed * (-1);

        gameObject.tag = "Character";
        gameObject.layer = 6;

        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);

        isCrazy = true;
    }

    public void Stun(float delay = 2.0f)
    {
        currentSpeed = 0;
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y);
        Invoke("SDelay", delay);
    }

    public void Slow(float speed = 1.6f)
    {
        currentSpeed = status.speed / speed;
        Invoke("SDelay", 5f);
    }
    void SDelay()
    {
        currentSpeed = status.speed;
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
        if (isCrazy && collision.transform.CompareTag("Enemy"))
        {
            if (collision.transform.position.x - transform.position.x > ignoreDistance)
            {
                target = collision.transform;
            }
        }
    }

    private IEnumerator HittedCoroutine(int damage)
    {
        currentHP -= damage;
        sprite.color = new Color(255, 255, 255, 0.6f);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(255, 255, 255, 1);
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
