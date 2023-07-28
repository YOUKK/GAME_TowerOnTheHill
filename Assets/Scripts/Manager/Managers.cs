using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� �Ŵ��� Ŭ������ ���� Ŭ����. ��ǥ �Ŵ��� Ŭ�����̴�.
// �� Ŭ������ ���� �ٸ� �Ŵ��� Ŭ������ �����Ѵ�.
public class Managers : MonoBehaviour
{
    private static Managers instance;
    public static Managers Instance // get ������Ƽ
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
            Debug.Log("Coin ���� ����!");
        }
    }

    void Update()
    {
        // ���� �÷��� ���� ���۵Ǹ� ȣ���ϱ�
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
