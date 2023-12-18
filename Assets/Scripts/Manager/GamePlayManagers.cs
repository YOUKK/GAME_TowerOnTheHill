using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ��� �Ŵ��� Ŭ������ ���� Ŭ����. ��ǥ �Ŵ��� Ŭ�����̴�.
// �� Ŭ������ ���� �ٸ� �Ŵ��� Ŭ������ �����Ѵ�.
// ���� ������ �����ϴ� ������ ���⿡ static ������ �����Ѵ�.

// Manager ������Ʈ�� �ٴ´�

public class GamePlayManagers : MonoBehaviour
{
    private static GamePlayManagers instance;
    public static GamePlayManagers Instance // get ������Ƽ
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

    [SerializeField]
    private GameObject hammerItem;

    // �� �������� ������ ���� ���� ��
    private int earnedCoin = 0;
    public int GetEarnedCoin { get => earnedCoin; }

    // ���� ����
    public int slotNum = 6;

	void Start()
    {
        Init();
        timeM.InitTimer();
        //timeM.StartTimer();
        if(SceneManager.GetActiveScene().name == "GamePlayScene")
            ApplyShopItem();
    }

    void Update()
    {
        // ���� �÷��� ���� ���۵Ǹ� ȣ���ϱ�
        timeM.OnUpdate();
    }

    private static void Init()
	{
        if(instance == null)
		{
            GameObject go = GameObject.Find("Manager");
            if(go == null)
			{
                go = new GameObject { name = "Manager" };
                go.AddComponent<GamePlayManagers>();
			}

            instance = go.GetComponent<GamePlayManagers>();
		}
	}

    private void ApplyShopItem()
    {
        if (DataManager.GetData.GetShopData().hasHammer)
            hammerItem.SetActive(true);
        else hammerItem.SetActive(false);
    }

    public void AddCoin(int amount)
    {
        earnedCoin += amount;
        int currentCoin = PlayerPrefs.GetInt("coin");
        PlayerPrefs.SetInt("coin", currentCoin + 50);
    }
}
