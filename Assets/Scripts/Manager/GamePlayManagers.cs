using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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

    //private MouseInputManager mouseInputM = new MouseInputManager();
    //public static MouseInputManager MouseInputM { get { return instance.mouseInputM; } }

    private GameObject menuCanvas;
    private PhaseStage currentPS;
    public PhaseStage GetCurrentPS { get => currentPS; }

    // �� �������� ������ ���� ���� ��
    private int earnedCoin = 0;
    public int GetEarnedCoin { get => earnedCoin; }

    // ���� ����
    //public int slotNum = 6;
    [SerializeField] private bool isGameClear = false;
    public bool IsGameClear { get => isGameClear; set => isGameClear = value; }


	void Start()
    {
        Init();
        timeM.InitTimer();
        //timeM.StartTimer();
        menuCanvas = GameObject.Find("MenuCanvas");
        // ���� ��������-������ ���� �ҷ�����
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "selectPhaseStage.json");
        string jsonData = File.ReadAllText(path);
        currentPS = JsonUtility.FromJson<PhaseStage>(jsonData);

        if (SceneManager.GetActiveScene().name == "GamePlayScene" || SceneManager.GetActiveScene().name == "BossWave")
            ApplyShopItem();
    }

    void Update()
    {
        // ���� �÷��� ���� ���۵Ǹ� ȣ���ϱ�
        timeM.OnUpdate();

        // �׽�Ʈ �ڵ�
		if (isGameClear)
            Victory();
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

    public void Victory()
	{
        menuCanvas.GetComponent<MenuCanvas>().ActivePopupVictory();
    }

    public void Defeat()
	{
        menuCanvas.GetComponent<MenuCanvas>().ActivePopupDefeat();
	}

    private void ApplyShopItem()
    {
        if (DataManager.GetData.GetShopData().hasHammer)
            menuCanvas.GetComponent<MenuCanvas>().ActiveHammer(true);
        else menuCanvas.GetComponent<MenuCanvas>().ActiveHammer(false);

        if(DataManager.GetData.GetShopData().hasSeatExpansion)
        {
            Map.GetInstance().AddLine(); // Map���� �� ������Ű�� �Լ� AddLine ����
        }
    }

    public void AddCoin(int amount)
    {
        earnedCoin += amount;
        int currentCoin = PlayerPrefs.GetInt("coin");
        PlayerPrefs.SetInt("coin", currentCoin + 50);
    }
}
