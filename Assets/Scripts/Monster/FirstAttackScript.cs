using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAttackScript : Monster
{
    private float distance = 8.0f;
    private Vector3 startPosition;
    protected override void Start()
    {
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
