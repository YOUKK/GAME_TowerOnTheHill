using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAttackScript : Monster
{
    private float distance = 8.0f;
    private Vector3 startPosition;
    protected override void Start()
    {
        // 스테이터스 세팅
        ignoreDistance = 0.5f;
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
}
