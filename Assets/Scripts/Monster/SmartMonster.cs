using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartMonster : Monster
{
    [SerializeField]
    private float   lineChangeTime;
    private bool    isMovingLine = false;

    private Transform startPosition;
    private Transform endPosition;
    [SerializeField]
    private float moveDuration = 120.0f; // 2 minutes in seconds

    private float lineMoveStartTime;
    private float startTime;

    protected override void Start()
    {
        base.Start();

        
    }

    protected override void Update()
    {
        base.Update();
    }

    IEnumerator MovePlayerOverTime()
    {
        while (Time.time - startTime < moveDuration)
        {
            float normalizedTime = (Time.time - startTime) / moveDuration;
            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, normalizedTime);
            yield return null;
        }

        // Ensure the player reaches the exact end position
        transform.position = endPosition.position;
    }

    protected override void Move(float speed)
    {
        transform.position = new Vector3(transform.position.x + currentSpeed * (-1) * Time.deltaTime,
            transform.position.y, transform.position.z);
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override IEnumerator AttackCoolCoroutine()
    {
        Attack();
        yield return new WaitForSeconds(status.hitSpeed);
        isAttacking = false;
    }

    protected override void Dead()
    {
        base.Dead();
    }

    private void DecideWhereToMove()
    {
        Line way1, way2;
        if (currentLine == 4)
        {
            way1 = Map.GetInstance().GetLineInfo(currentLine - 1);
            way2 = Map.GetInstance().GetLineInfo(currentLine - 2);
        }
        else if(currentLine == 0)
        {
            way1 = Map.GetInstance().GetLineInfo(currentLine + 1);
            way2 = Map.GetInstance().GetLineInfo(currentLine + 2);
        }
        else
        {
            way1 = Map.GetInstance().GetLineInfo(currentLine - 1);
            way2 = Map.GetInstance().GetLineInfo(currentLine + 1);
        }


    }
}
