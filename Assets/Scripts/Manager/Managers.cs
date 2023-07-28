using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 매니저 클래스의 집합 클래스. 대표 매니저 클래스이다.
// 이 클래스를 통해 다른 매니저 클래스에 접근한다.
public class Managers : MonoBehaviour
{
    private static Managers instance;
    public static Managers Instance // get 프로퍼티
	{
		get
		{
            Init();
            return instance;
		}
	}

    private TimeManager timeM = new TimeManager();
    public static TimeManager TimeM { get { return Instance.timeM; } }

    private MouseInputManager mouseInputM = new MouseInputManager();
    public static MouseInputManager MouseInputM { get { return instance.mouseInputM; } }



	void Start()
    {
        Init();
        timeM.InitTimer();
        //timeM.StartTimer();

        if (!PlayerPrefs.HasKey("coin"))
        {
            PlayerPrefs.SetInt("coin", 0);
            Debug.Log("Coin 새로 생성!");
        }
    }

    void Update()
    {
        // 게임 플레이 씬이 시작되면 호출하기
        timeM.OnUpdate();
        //Debug.Log(TimeM.Sec);
    }

    private static void Init()
	{
        if(instance == null)
		{
            GameObject go = GameObject.Find("Manager");
            if(go == null)
			{
                go = new GameObject { name = "Manager" };
                go.AddComponent<Managers>();
			}

            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();
		}
	}
}
