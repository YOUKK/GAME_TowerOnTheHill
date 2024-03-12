using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MonsterSpawner : MonoBehaviour
{
    private static MonsterSpawner instance;
    public  static MonsterSpawner GetInstance { get { Init(); return instance; } }

    public int           phase = 1;
    public int           stage = 1;
    public  GameObject[]  lines = new GameObject[5];

    private int           count = 0; // 한 스테이지의 길이(몬스터 생성 수)
    private int           idx = 0;
    private MonsterSpawnData[] currentWave = null;
    public  float         monsterBuffTime = 0;

    private LinkedList<GameObject>[] monsterList = new LinkedList<GameObject>[5];

    [SerializeField]
    private int rewardCoin = 100;

    private CollectResource resourceUI;
    private GameObject coinBox;

    // 선택한 스테이지 정보
    private PhaseStage selectPS = new PhaseStage();

    private GameObject menuCanvas;

    void Start()
    {
        Init();
        // stage 정보에 따라 몬스터스포너 설정
        SetPhaseStage();
        if (SceneManager.GetActiveScene().name == "TutorialScene") { phase = 9; stage = 1; }
        
        // Debug.Log("phase: " + phase + " stage: " + stage);

        currentWave = DataManager.GetData.TryParse(phase, stage).waveArray;
        count = currentWave.Length;
        // Line 오브젝트 데이터 수집
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = transform.GetChild(i).gameObject;
        }
        // 생성된 몬스터를 관리할 리스트 초기화
        for(int i = 0; i < monsterList.Length; ++i)
        {
            monsterList[i] = new LinkedList<GameObject>();
        }

        menuCanvas = GameObject.Find("MenuCanvas");
        menuCanvas.GetComponent<MenuCanvas>().ActiveMonsterWaveTimer();
        resourceUI = menuCanvas.GetComponent<CollectResource>();
        coinBox = resourceUI.transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (idx >= count)
        {
            for (int i = 0; i < monsterList.Length; ++i)
            {
                if (monsterList[i].Count != 0) return;
            }
            // GamePlayManagers.Instance.IsGameClear = true; // 테스트 코드
            if (GamePlayManagers.Instance.IsGameClear == false)
            {
                GamePlayManagers.Instance.Victory();
                GamePlayManagers.Instance.IsGameClear = true;
            }
            return;
        }

        if (currentWave[idx].time < GamePlayManagers.TimeM.Sec)
        {
            GameObject obj = Instantiate(currentWave[idx].monsterInfo, 
                lines[currentWave[idx].line].transform.position, transform.rotation);

            obj.GetComponent<Monster>().CurrentLine = currentWave[idx].line; // 라인 정보 할당
            obj.GetComponent<SpriteRenderer>().sortingLayerName = $"Line{currentWave[idx].line}";
            obj.GetComponent<SpriteRenderer>().sortingOrder = idx + 1; // sorting order 0 is for characters

            if (obj != null) { monsterList[currentWave[idx].line].AddLast(obj); } // 삽입
            ++idx;
        }
    }

    private static void Init()
    {
        if(instance == null)
        {
            GameObject go = GameObject.Find("MonsterSpawner");
            if(go == null)
            {
                go = new GameObject("MonsterSpawner");
                go.AddComponent<MonsterSpawner>();
            }

            instance = go.GetComponent<MonsterSpawner>();
        }
    }

    // json을 phaseStage로 로드하는 함수
    // 이 함수를 씬 로드할 때마다 호출하기
    private void SetPhaseStage()
    {
        //string path = Path.Combine(Application.dataPath + "/Resources/Data/", "selectPhaseStage.json");
        //string jsonData = Resources.Load<TextAsset>("Data/selectPhaseStage").ToString();
        string jsonData = File.ReadAllText(Application.dataPath + "/selectPhaseStage.json");
        selectPS = JsonUtility.FromJson<PhaseStage>(jsonData);

        phase = selectPS.phase;
        stage = selectPS.stage;

        if (phase < 1 || stage < 1) 
        { 
            phase = 1; 
            stage = 1; 
        }
    }

    public GameObject[] GetLineMonstersInfo(int line)
    {
        if (monsterList[line].Count == 0) return null;

        List<GameObject> tempMonsterList = new List<GameObject>();
        foreach (var item in monsterList[line])
        {
            tempMonsterList.Add(item);
        }
        return tempMonsterList.ToArray();
    }

    public void RemoveMonster(GameObject obj, int line)
    {
        monsterList[line].Remove(obj);

        /*if (monsterList[line].Remove(obj))
            Debug.Log("Monster is removed");
        else
        {
            Debug.LogError("Monster doesn't removed");
        }*/
            //for(int i = 0; i < 5; ++i)
            //    Debug.Log("Line : " + i + " " + monsterList[i].Count);
    }

    public void InsertMonster(GameObject obj, int line)
    {
        monsterList[line].AddLast(obj);
        //for (int i = 0; i < 5; ++i)
        //    Debug.Log("Line : " + i + " " + monsterList[i].Count);
    }

    public void BuffMonsters()
    {
        for(int i = 0; i < monsterList.Length; ++i)
        {
            foreach (var item in monsterList[i])
            {
                if (item != null)
                {
                    item.GetComponent<Monster>().ChangeStatus(20, 0.2f, 50);
                }
            }
        }
        Invoke("NerfMonsters", monsterBuffTime);
    }

    private void NerfMonsters()
    {
        for (int i = 0; i < monsterList.Length; ++i)
        {
            foreach (var item in monsterList[i])
            {
                if (item != null)
                {
                    item.GetComponent<Monster>().ChangeStatus();
                }
            }
        }

        StartCoroutine(CoinRewardCoroutine());
    }

    IEnumerator CoinRewardCoroutine()
    {
        coinBox.SetActive(true);
        int initialCoin = GameManager.GetInstance.GetPlayerData(PlayerDataKind.Coin);
        GameManager.GetInstance.SetPlayerData(PlayerDataKind.Coin, initialCoin);

        TextMeshProUGUI textMeshPro = coinBox.GetComponentInChildren<TextMeshProUGUI>();
        for (int i = 0; i <= 8; ++i)
        {
            int afterCoin = GameManager.GetInstance.GetPlayerData(PlayerDataKind.Coin);
            float lerpCoin = Mathf.Lerp(initialCoin, afterCoin, (float)i/8);
            textMeshPro.text = ((int)lerpCoin).ToString();
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.5f);
        coinBox.SetActive(false);
    }
}
