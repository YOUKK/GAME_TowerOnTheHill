using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 몬스터 웨이브 UI 관련 스크립트
public class MonsterWaveTimer : MonoBehaviour
{
    private int firstAppearTime = 5; // 첫 몬스터가 나타나기 전까지의 시간
    public int FirstAppearTime { set { firstAppearTime = value; } get { return firstAppearTime; } }
    private int timeCheckInterval = 2; // 시간 체크 간격

    private int firstWaveStart = 0; // 첫번째 웨이브 시작 시간
    public int FirstWaveStart { set { firstWaveStart = value; } get { return firstWaveStart; } }
    private int secondWaveStart = 0; // 두번째 웨이브 시작 시간
    public int SecondWaveStart { set { secondWaveStart = value; } get { return secondWaveStart; } }

    private int firstWaveTime = 120; // 첫번째 웨이브 = 2분(슬라이드 딱 중간 지점)
    public int FirstwaveTime { set { firstWaveTime = value; } get { return firstWaveTime; } }
    private int secondWaveTime = 240; // 두번째 웨이브 = 4분(슬라이드 맨 끝)(firstwaveTime의 2배여야함)
    public int SecondWaveTime { set { secondWaveTime = value; } get { return secondWaveTime; } }
    private bool firstAppear = false;
    private bool firstWave = false;

    //private float startTime; // 게임 시작시간
    //private float currentTime; // 현재 시간

    private float moveInterval = 0; // 핸들이 움직여야 하는 값
    private int moveNum = 0; // 움직일 횟수

    [SerializeField]
    private GameObject waveBar;
    private Slider waveSlider;

    void Start()
    {
        waveSlider = waveBar.GetComponent<Slider>();

        //SetTime();
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
			//Debug.Log(Time.time);
			yield return new WaitForSeconds(2.0f);

			if (waveSlider.value >= 1)
			{
				break;
			}
		}
	}

	//   private void SetTime()
	//{
	//       startTime = Time.time;
	//       currentTime = Time.time;
	//}

	private void CheckTime()
	{
        if(!firstAppear && Managers.TimeM.Sec >= firstAppearTime)
		{
            firstAppear = true;
            waveBar.SetActive(true);
            moveNum = (firstWaveTime - firstAppearTime) / timeCheckInterval;
            moveInterval = 0.5f / moveNum;
        }
        else if(firstAppear && !firstWave && Managers.TimeM.Sec < firstWaveTime)
		{
            HandleMove();
        }
        else if(firstAppear && !firstWave && Managers.TimeM.Sec >= firstWaveTime)
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
