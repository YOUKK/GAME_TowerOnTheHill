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

    public  int           phase = 1;
    public  int           stage = 1;
    public  GameObject[]  lines = new GameObject[5];

    private int           count = 0; // �� ���������� ����(���� ���� ��)
    private int           idx = 0;
    private MonsterSpawnData[] currentWave = null;
    public  float         monsterBuffTime = 0;

    private LinkedList<GameObject>[] monsterList = new LinkedList<GameObject>[5];

    [SerializeField]
    private bool isAllMonsterDead = false;
    public  bool IsAllMonsterDead { get => isAllMonsterDead; }
    [SerializeField]
    private int rewardCoin = 100;

    private CollectResource resourceUI;
    private GameObject coinBox;

    // ������ �������� ����
    private PhaseStage selectPS = new PhaseStage();

    [SerializeField]
    private GameObject victoryPopup;

    public GameObject bossMonster;

    void Start()
    {
        Init();
        // stage ������ ���� ���ͽ����� ����
        SetPhaseStage();
        if (SceneManager.GetActiveScene().name == "TutorialScene") { phase = 9; stage = 1; }
        
        Debug.Log("phase: " + phase + " stage: " + stage);

        currentWave = DataManager.GetData.TryParse(phase, stage).waveArray;
        count = currentWave.Length;
        // Line ������Ʈ ������ ����
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = transform.GetChild(i).gameObject;
        }
        // ������ ���͸� ������ ����Ʈ �ʱ�ȭ
        for(int i = 0; i < monsterList.Length; ++i)
        {
            monsterList[i] = new LinkedList<GameObject>();
        }

        gameObject.GetComponent<MonsterWaveTimer>().enabled = true;

        resourceUI = GameObject.Find("MenuCanvas").GetComponent<CollectResource>();
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
            isAllMonsterDead = true;
            victoryPopup.SetActive(true);
            return;
        }

        if (currentWave[idx].time < GamePlayManagers.TimeM.Sec)
        {
            GameObject obj = Instantiate(currentWave[idx].monsterInfo, 
                lines[currentWave[idx].line].transform.position, transform.rotation);

            obj.GetComponent<Monster>().CurrentLine = currentWave[idx].line; // ���� ���� �Ҵ�
            obj.GetComponent<SpriteRenderer>().sortingLayerName = $"Line{currentWave[idx].line}";
            obj.GetComponent<SpriteRenderer>().sortingOrder = idx + 1; // sorting order 0 is for characters

            if (obj != null) { monsterList[currentWave[idx].line].AddLast(obj); } // ����
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

    // json�� phaseStage�� �ε��ϴ� �Լ�
    // �� �Լ��� �� �ε��� ������ ȣ���ϱ�
    private void SetPhaseStage()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "selectPhaseStage.json");
        string jsonData = File.ReadAllText(path);
        selectPS = JsonUtility.FromJson<PhaseStage>(jsonData);

        phase = selectPS.phase;
        stage = selectPS.stage;

        if (phase < 1 || stage < 1) 
        { 
            phase = 1; 
            stage = 1; 
        }
        if(phase == 3 && stage == 5)
        {
            bossMonster = GameObject.Find("Boss");
            if(bossMonster)
            {
                InsertMonster(bossMonster, 1);
            }
        }
    }

    public GameObject[] GetLineMonstersInfo(int line)
    {
        if (monsterList[line] == null) return null;

        List<GameObject> tempMonsterList = new List<GameObject>();
        foreach (var item in monsterList[line])
        {
            tempMonsterList.Add(item);
        }
        return tempMonsterList.ToArray();
    }

    public void RemoveMonster(GameObject obj, int line)
    {
        if (monsterList[line].Remove(obj))
            Debug.Log("Monster is removed");
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
                item.GetComponent<Monster>().ChangeStatus(10, 0.2f, 500);
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
                item.GetComponent<Monster>().ChangeStatus();
            }
        }

        StartCoroutine(CoinRewardCoroutine());
    }

    IEnumerator CoinRewardCoroutine()
    {
        coinBox.SetActive(true);
        int initialCoin = PlayerPrefs.GetInt("coin");
        PlayerPrefs.SetInt("coin", initialCoin + rewardCoin);

        TextMeshProUGUI textMeshPro = coinBox.GetComponentInChildren<TextMeshProUGUI>();
        for (int i = 0; i <= 8; ++i)
        {
            int afterCoin = PlayerPrefs.GetInt("coin");
            float lerpCoin = Mathf.Lerp(initialCoin, afterCoin, (float)i/8);
            textMeshPro.text = ((int)lerpCoin).ToString();
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.5f);
        coinBox.SetActive(false);
    }
}
