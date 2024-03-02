using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

// 모든 매니저 클래스의 집합 클래스. 대표 매니저 클래스이다.
// 이 클래스를 통해 다른 매니저 클래스에 접근한다.
// 여러 씬에서 공유하는 변수는 여기에 static 변수로 선언한다.

// Manager 오브젝트에 붙는다

public class GamePlayManagers : MonoBehaviour
{
    private static GamePlayManagers instance;
    public static GamePlayManagers Instance // get 프로퍼티
	{
		get
		{
            Init();
            return instance;
		}
	}

    public delegate void Finish();
    public Finish finishProcess; // 게임 끝날 때 젬, 코인 처리

    private TimeManager timeM = new TimeManager();
    public static TimeManager TimeM { get { return Instance.timeM; } }

    //private MouseInputManager mouseInputM = new MouseInputManager();
    //public static MouseInputManager MouseInputM { get { return instance.mouseInputM; } }

    private GameObject menuCanvas;
    private PhaseStage currentPS;
    public PhaseStage GetCurrentPS { get => currentPS; }

    // 한 스테이지 내에서 얻은 코인 값
    private int earnedCoin = 0;
    public int GetEarnedCoin { get => earnedCoin; }

    // 공유 변수
    //public int slotNum = 6;
    [SerializeField] private bool isGameClear = false;
    public bool IsGameClear { get => isGameClear; set => isGameClear = value; }

    // 공유 변수
    public PhaseStage winPS = new PhaseStage(); // 현재 클리어한 곳까지의 맵, 스테이지 정보
    public PhaseStage selectPS = new PhaseStage(); // 선택한 맵, 스테이지 정보

    // json 파일 경로
    private string winPSPath;
    private string selectPSPath;

	private void Awake()
	{
        Init();

        // 현재 스테이지-페이즈 정보 불러오기
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
        // 게임 플레이 씬이 시작되면 호출하기
        timeM.OnUpdate();

        // 테스트 코드
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

    //  json을 WinphaseStage로 로드하는 함수
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

    //  json을 SelectphaseStage로 로드하는 함수
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


    // WinphaseStage를 json으로 저장하는 함수
    public void SaveWinPhaseStageToJson()
    {
        winPSPath = Path.Combine(Application.dataPath, "winPhaseStage.json");
        string jsonData = JsonUtility.ToJson(winPS, true);
        File.WriteAllText(winPSPath, jsonData);
    }

    // SelectphaseStage를 json으로 저장하는 함수
    public void SaveSelectPhaseStageToJson()
    {
        selectPSPath = Path.Combine(Application.dataPath, "selectPhaseStage.json");
        string jsonData = JsonUtility.ToJson(selectPS, true);
        File.WriteAllText(selectPSPath, jsonData);
    }

    public void Victory()
	{
        // 젬 비활성 & 코인 자동 수집
        //finishProcess();

        // 튜토리얼 씬의 경우
        if (MonsterSpawner.GetInstance.phase == 9 && MonsterSpawner.GetInstance.stage == 1)
        {
            menuCanvas.GetComponent<MenuCanvas>().ActivePopupVictory();
            return;
        }

        // 조건에 따라 팝업
        LoadWinPhaseStageFromJson();
        LoadSelectPhaseStageFromJson();

        // 새로운 스테이지를 깼는데
        if(selectPS.phase > winPS.phase || (selectPS.phase == winPS.phase && selectPS.stage > winPS.stage))
		{
            // 새로운 캐릭터가 해금되는 스테이지라면
            if (!((selectPS.phase == 1 && selectPS.stage == 4) || (selectPS.phase == 2 && selectPS.stage == 4) || (selectPS.phase == 3 && (selectPS.stage == 3 || selectPS.stage == 4 || selectPS.stage == 5)))){
                menuCanvas.GetComponent<MenuCanvas>().ActiveVictoryCharacter();
			}
		}

        menuCanvas.GetComponent<MenuCanvas>().ActivePopupVictory();
    }

    public void Defeat()
	{
        // 젬 비활성 & 코인 자동 수집
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
            Map.GetInstance().AddLine(); // Map에서 방어선 증가시키는 함수 AddLine 실행
        }
    }

    public void AddCoin(int amount)
    {
        earnedCoin += amount;
        int currentCoin = GameManager.GetInstance.GetPlayerData(PlayerDataKind.Coin);
        GameManager.GetInstance.SetPlayerData(PlayerDataKind.Coin, currentCoin + 50);
    }
}
