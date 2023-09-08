using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartMonster : Monster
{
    [SerializeField]
    private float   lineChangeTime;
    private int     destinationLine;
    private bool    isFinishedLineMoving = false;
    private bool    isLineMoving = false;

    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField]
    private float moveDuration = 3.0f; // 라인 이동 시간

    [SerializeField]
    private float lineMoveStartTime = 3.5f; // 라인 이동을 시작하는 시간
    private float monsterCreatedTime;       // 몬스터가 생성된 시간

    [SerializeField]
    private DetectAreaScript detectObj;

    protected override void Start()
    {
        base.Start();
        monsterCreatedTime = Time.time;
    }

    protected override void Update()
    {
        if (isLineMoving == false && isFinishedLineMoving == false)
        {
            if (detectObj.IsDetectedCharacter == true || Time.time - monsterCreatedTime > lineMoveStartTime)
            {
                isLineMoving = true;
                MoveLine();
                return;
            }
        }

        if(isLineMoving == false) base.Update();
    }

    private void MoveLine()
    {
        lineMoveStartTime = Time.time;
        startPosition = transform.position;
        endPosition = DecideWhereToMove();

        StartCoroutine(MoveMonsterOverTime());
    }

    IEnumerator MoveMonsterOverTime()
    {
        while (Time.time - lineMoveStartTime < moveDuration)
        {
            float normalizedTime = (Time.time - lineMoveStartTime) / moveDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, normalizedTime);
            yield return null;
        }

        // Ensure the player reaches the exact end position
        transform.position = endPosition;

        MonsterSpawner.GetInstance.RemoveMonster(gameObject, currentLine);
        MonsterSpawner.GetInstance.InsertMonster(gameObject, destinationLine);
        isLineMoving = false;
        isFinishedLineMoving = true;
    }

    private Vector3 DecideWhereToMove()
    {
        Line way1, way2;
        if (currentLine == 4)
        {
            way1 = Map.GetInstance().GetLineInfo(currentLine - 1);
            way2 = Map.GetInstance().GetLineInfo(currentLine - 2);
        }
        else if (currentLine == 0)
        {
            way1 = Map.GetInstance().GetLineInfo(currentLine + 1);
            way2 = Map.GetInstance().GetLineInfo(currentLine + 2);
        }
        else
        {
            way1 = Map.GetInstance().GetLineInfo(currentLine - 1);
            way2 = Map.GetInstance().GetLineInfo(currentLine + 1);
        }

        if (way1.hpSum < way2.hpSum)
        {
            destinationLine = way1.lineNumber;
            return new Vector3(transform.position.x, way1.location.y, transform.position.z);
        }
        else
        {
            destinationLine = way2.lineNumber;
            return new Vector3(transform.position.x, way2.location.y, transform.position.z);
        }
    }
}
