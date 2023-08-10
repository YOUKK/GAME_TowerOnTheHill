using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterSpawner : MonoBehaviour
{
    private static MonsterSpawner instance;
    public  static MonsterSpawner GetInstance { get { Init(); return instance; } }

    // 페이즈, 스테이지는 인덱스 상으로는 0번부터 시작
    public  int           phase = 1;
    public  int           stage = 1;
    public  GameObject[]  lines = new GameObject[5];

    private int           count = 0; // 한 스테이지의 길이(몬스터 생성 수)
    private int           idx = 0;
    private MonsterWave[] currentWave = null;
    public  float         monsterBuffTime = 0;

    private LinkedList<GameObject>[] monsterList = new LinkedList<GameObject>[5];

    [SerializeField]
    private bool isAllMonsterDead = false;
    public  bool IsAllMonsterDead { get => isAllMonsterDead; }
    [SerializeField]
    private int rewardCoin = 100;

    [SerializeField] GameObject textVictory;
    private CollectResource resourceUI;
    private GameObject coinBox;

    void Start()
    {
        Init();
        PlayerPrefs.SetInt("coin", 0);

        if (phase - 1 < 0 || stage - 1 < 0) Debug.LogError("Wrong Phase or Stage number input");
        currentWave = DataManager.monsterWave[phase-1][stage-1].waveArray;
        count = currentWave.Length;

        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = transform.GetChild(i).gameObject;
        }

        for(int i = 0; i < monsterList.Length; ++i)
        {
            monsterList[i] = new LinkedList<GameObject>();
        }

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
            textVictory.SetActive(true);
            return;
        }

        if (currentWave[idx].time < Managers.TimeM.Sec)
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
        if (monsterList[line].Remove(obj))
            Debug.Log("Removed Monster");
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
            Debug.Log(lerpCoin + "  ,  " + (float)i/8);
            textMeshPro.text = ((int)lerpCoin).ToString();
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.5f);
        coinBox.SetActive(false);
    }
}
