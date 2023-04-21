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
    // ���� �Ӵ� ���� ���� �߰�

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void Move()
    {
        transform.position = new Vector3(transform.position.x + status.speed * (-1) * Time.deltaTime,
            transform.position.y, transform.position.z);
    }

    protected virtual void Attack()
    {

    }

    protected virtual void Dead()
    {
        Destroy(gameObject);
    }
}
