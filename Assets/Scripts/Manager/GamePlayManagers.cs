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


	void Start()
    {
        Init();
        timeM.InitTimer();
        //timeM.StartTimer();
        menuCanvas = GameObject.Find("MenuCanvas");
        // 현재 스테이지-페이즈 정보 불러오기
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "selectPhaseStage.json");
        string jsonData = File.ReadAllText(path);
        currentPS = JsonUtility.FromJson<PhaseStage>(jsonData);

        if (SceneManager.GetActiveScene().name == "GamePlayScene" || SceneManager.GetActiveScene().name == "BossWave")
            ApplyShopItem();
    }

    void Update()
    {
        // 게임 플레이 씬이 시작되면 호출하기
        timeM.OnUpdate();

        // 테스트 코드
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
            Map.GetInstance().AddLine(); // Map에서 방어선 증가시키는 함수 AddLine 실행
        }
    }

    public void AddCoin(int amount)
    {
        earnedCoin += amount;
        int currentCoin = PlayerPrefs.GetInt("coin");
        PlayerPrefs.SetInt("coin", currentCoin + 50);
    }
}
