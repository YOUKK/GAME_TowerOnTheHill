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

    public delegate void Finish();
    public Finish finishProcess; // ���� ���� �� ��, ���� ó��

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

    // ���� ����
    public PhaseStage winPS = new PhaseStage(); // ���� Ŭ������ �������� ��, �������� ����
    public PhaseStage selectPS = new PhaseStage(); // ������ ��, �������� ����

    // json ���� ���
    private string winPSPath;
    private string selectPSPath;

	private void Awake()
	{
        Init();

        // ���� ��������-������ ���� �ҷ�����
        LoadWinPhaseStageFromJson();
        LoadSelectPhaseStageFromJson();
    }

	void Start()
    {
        timeM.InitTimer();
        //timeM.StartTimer();
        menuCanvas = GameObject.Find("MenuCanvas");

        if (SceneManager.GetActiveScene().name == "GamePlayScene" || SceneManager.GetActiveScene().name == "BossWave")
            ApplyShopItem();
    }

    void Update()
    {
        // ���� �÷��� ���� ���۵Ǹ� ȣ���ϱ�
        timeM.OnUpdate();

        // �׽�Ʈ �ڵ�
		if (isGameClear)
		{
            Victory();
            IsGameClear = false;
        }
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

    //  json�� WinphaseStage�� �ε��ϴ� �Լ�
    public void LoadWinPhaseStageFromJson()
    {
		winPSPath = Path.Combine(Application.dataPath, "winPhaseStage.json");
		if (!File.Exists(winPSPath))
		{
            winPS.phase = 1; winPS.stage = 0;
            SaveWinPhaseStageToJson();
		}

		string jsonData = File.ReadAllText(winPSPath);
        winPS = JsonUtility.FromJson<PhaseStage>(jsonData);
    }

    //  json�� SelectphaseStage�� �ε��ϴ� �Լ�
    public void LoadSelectPhaseStageFromJson()
    {
		selectPSPath = Path.Combine(Application.dataPath, "selectPhaseStage.json");
		if (!File.Exists(selectPSPath))
		{
            selectPS.phase = 1; selectPS.stage = 1;
            SaveSelectPhaseStageToJson();
		}

		string jsonData = File.ReadAllText(selectPSPath);
        selectPS = JsonUtility.FromJson<PhaseStage>(jsonData);
    }


    // WinphaseStage�� json���� �����ϴ� �Լ�
    public void SaveWinPhaseStageToJson()
    {
        winPSPath = Path.Combine(Application.dataPath, "winPhaseStage.json");
        string jsonData = JsonUtility.ToJson(winPS, true);
        File.WriteAllText(winPSPath, jsonData);
    }

    // SelectphaseStage�� json���� �����ϴ� �Լ�
    public void SaveSelectPhaseStageToJson()
    {
        selectPSPath = Path.Combine(Application.dataPath, "selectPhaseStage.json");
        string jsonData = JsonUtility.ToJson(selectPS, true);
        File.WriteAllText(selectPSPath, jsonData);
    }

    public void Victory()
	{
        // �� ��Ȱ�� & ���� �ڵ� ����
        //finishProcess();

        // Ʃ�丮�� ���� ���
        if (MonsterSpawner.GetInstance.phase == 9 && MonsterSpawner.GetInstance.stage == 1)
        {
            menuCanvas.GetComponent<MenuCanvas>().ActivePopupVictory();
            return;
        }

        // ���ǿ� ���� �˾�
        LoadWinPhaseStageFromJson();
        LoadSelectPhaseStageFromJson();

        // ���ο� ���������� ���µ�
        if(selectPS.phase > winPS.phase || (selectPS.phase == winPS.phase && selectPS.stage > winPS.stage))
		{
            // ���ο� ĳ���Ͱ� �رݵǴ� �����������
            if (!((selectPS.phase == 1 && selectPS.stage == 4) || (selectPS.phase == 2 && selectPS.stage == 4) || (selectPS.phase == 3 && (selectPS.stage == 3 || selectPS.stage == 4 || selectPS.stage == 5)))){
                menuCanvas.GetComponent<MenuCanvas>().ActiveVictoryCharacter();
			}
		}

        menuCanvas.GetComponent<MenuCanvas>().ActivePopupVictory();
    }

    public void Defeat()
	{
        // �� ��Ȱ�� & ���� �ڵ� ����
        //finishProcess();
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
        int currentCoin = GameManager.GetInstance.GetPlayerData(PlayerDataKind.Coin);
        GameManager.GetInstance.SetPlayerData(PlayerDataKind.Coin, currentCoin + 50);
    }
}
