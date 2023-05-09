using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterWaveTimer : MonoBehaviour
{
    private float firstAppearTime = 5f; // 첫 몬스터가 나타나기 전까지의 시간
    private float timeCheckInterval = 2f; // 시간 체크 간격
    private float firstWaveTime = 30f; // 첫번째 웨이브 = 2분
    private float secondWaveTime = 60f; // 두번째 웨이브 = 4분
    private bool firstAppear = false;
    private bool firstWave = false;

    private float startTime; // 게임 시작시간
    private float currentTime; // 현재 시간

    private float moveInterval = 0; // 핸들이 움직여야 하는 값
    private float moveNum = 0; // 움직일 횟수

    [SerializeField]
    private GameObject waveBar;
    private Slider waveSlider;

    void Start()
    {
        waveSlider = waveBar.GetComponent<Slider>();

        SetTime();
        StartCoroutine(Call2f());

    }

    void Update()
    {

    }

    IEnumerator Call2f()
	{
        while (true)
        {
            CheckTime();
            Debug.Log(Time.time);
            yield return new WaitForSeconds(2.0f);

            if(waveSlider.value >= 1)
			{
                break;
			}
        }
    }

    private void SetTime()
	{
        startTime = Time.time;
        currentTime = Time.time;
	}

    private void CheckTime()
	{
        currentTime = Time.time - startTime;
        if(!firstAppear && currentTime >= firstAppearTime)
		{
            firstAppear = true;
            waveBar.SetActive(true);
            moveNum = (firstWaveTime - firstAppearTime) / timeCheckInterval;
            moveInterval = 0.5f / moveNum;
        }
        else if(firstAppear && !firstWave && currentTime < firstWaveTime)
		{
            HandleMove();
        }
        else if(firstAppear && !firstWave && currentTime >= firstWaveTime)
		{
            firstWave = true;
            moveNum = (secondWaveTime - firstWaveTime) / timeCheckInterval;
            moveInterval = 0.5f / moveNum;
        }
		else
		{
            HandleMove();
		}
	}

    private void HandleMove()
	{
        waveSlider.value += moveInterval;
	}
}
