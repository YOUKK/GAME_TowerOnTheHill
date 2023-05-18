using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 플레이씬 시작 이후 사용할 타이머
public class TimeManager
{
    private bool On = false; // true면 타이머 시작
    private float deltatime = 0f; // 시간 경과
    private int sec = 0; // 정수 타입의 초
    public int Sec { get { return sec; } }

    public void OnUpdate()
    {
        if (On)
        {
            deltatime += Time.deltaTime;
            sec = Mathf.FloorToInt(deltatime);
        }
    }

    public void InitTimer() // 매 게임 플레이 씬 시작할 때마다 호출
	{
        deltatime = 0f;
        sec = 0;
	}

    public void StartTimer() // 타이머 시작
	{
        On = true;
	}

    public void StopTimer() // 타이머 멈춤(일시정지를 누른 경우)
	{
        On = false;
	}
}
