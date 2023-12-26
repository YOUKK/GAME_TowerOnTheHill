using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackObject : Monster
{
    [SerializeField]
    private float distance = 8.0f;
    private Vector3 startPosition;
    protected override void Start()
    {
        // 스테이터스 세팅
        ignoreDistance = 0.3f;
        currentHP = status.hp;
        currentSpeed = status.speed;
        currentForce = status.force;
        int ran = Random.Range(0, 100);
        if (ran <= randomPercent) isGetCoin = true;
        else isGetCoin = false;

        startPosition = transform.position;
    }

    protected override void Update()
    {
        if (startPosition.x - transform.position.x < distance)
            Move(currentSpeed);
        else
            Destroy(gameObject);
    }

    protected override void Move(float speed)
    {
        base.Move(speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Character"))
        {
            Debug.Log($"{name} attacked");
            collision.GetComponent<Character>().Hit(currentForce);
        }
    }
}
