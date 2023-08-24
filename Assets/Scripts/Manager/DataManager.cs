using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

// 스크립트 직렬화 가능한 클래스
#region
[System.Serializable]
public class ShopData
{
    public bool             hasHammer;
    public bool             hasSeatExpansion;
    public int              slotLevel;
    public CharacterLvls[]  characterLvls;
    public ShopData()
    {
        hasHammer = false;
        hasSeatExpansion = false;
        slotLevel = 0;
        characterLvls = null;
    }
}

[System.Serializable]
public struct CharacterLvls
{
    public string characterName;
    public int level;
}

public enum UpgradeKind { Force, SkillSpeed, Health }

[System.Serializable]
public class UpgradeData
{
    public string chName;
    public float[] statIncrease;
    public UpgradeKind kind;

    public UpgradeData(string _name, float[] _statArray, UpgradeKind _kind)
    {
        chName = _name;
        statIncrease = _statArray;
        kind = _kind;
    }
}
#endregion

// 한 스테이지의 Monster Wave 정보
public class StageWave
{
    public MonsterWave[] waveArray = null;

    public StageWave(MonsterWave[] waves)
    {
        waveArray = waves;
    }

    public StageWave(List<MonsterWave> waves)
    {
        waveArray = waves.ToArray();
    }
}

// 몬스터 하나의 스폰 Wave 정보
public struct MonsterWave
{
    public int stage;
    public float time;
    public GameObject monsterInfo;
    public int line;
}


public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager GetData { get { Init(); return instance; } }

    [SerializeField]
    private MonsterWaveTimer monsterWaveTimer;
    private static string shopDataPath;
    private static UpgradeData[] upgradeDatas;

    public static List<List<StageWave>> monsterWave = new List<List<StageWave>>();

    void Awake()
    {
        Init();

        Scene currScene = SceneManager.GetActiveScene();
        if(currScene.name == "Shop")
        {
            // 캐릭터의 스텟을 정하는 시점 : 게임 시작 시가 적당한 것 같다.
            // 이유 : 상점을 이용하여 바뀔 수 있는 데이터들을 불러오기 때문에 매 게임 시작마다 불러와주는 것이 효율적이다.
            // 어떤 정보를 불러와야 할까?
            // 해당 캐릭터의 레벨의 수치 정보를 가져와야 함. 즉, UpgradeData가 필요함.
            // Idea : UpgradeData를 플레이어의 이름을 Key로 갖고 현재 status 값을 value로 갖는...
        }
        else TryParse();

        shopDataPath = Application.dataPath + "/Resources/Data/shopData.json";
        LoadCharacterUpgradeData();
    }

    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("DataManager");
            if (go == null)
            {
                go = new GameObject { name = "DataManager" };
                go.AddComponent<DataManager>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<DataManager>();
        }
    }

    private void TryParse()
    {
        monsterWave.Add(WaveParse("MonsterWaveDB - Phase0"));

        for (int i = 0; i < monsterWave[0].Count; ++i)
        {
            Debug.Log(monsterWave[0][i].waveArray.Length);
            for (int j = 0; j < monsterWave[0][i].waveArray.Length; ++j)
            {
                MonsterWave wave = monsterWave[0][i].waveArray[j];
                Debug.Log($"stage {wave.stage} , time {wave.time} , monster {wave.monsterInfo.name} , line {wave.line}");
            }
        }
    }

    public static ShopData GetShopData()
    {
        ShopData shopData = new ShopData();

        if (File.Exists(shopDataPath))
        {
            string jsonString = File.ReadAllText(shopDataPath);
            shopData = JsonUtility.FromJson<ShopData>(jsonString);
        }
        else
        {
            SaveShopData(shopData);
        }
        
        return shopData;
    }

    public static void SaveShopData(ShopData shopData)
    {
        // json 형태로 된 문자열 생성
        string json = JsonUtility.ToJson(shopData);
        // 파일 생성 및 저장
        File.WriteAllText(shopDataPath, json);
    }
    
    public static Dictionary<string, UpgradeData> GetUpgradeDataDic()
    {
        if (upgradeDatas == null) LoadCharacterUpgradeData();
        
        Dictionary<string, UpgradeData> dic = new Dictionary<string, UpgradeData>();

        for(int i = 0; i < upgradeDatas.Length; ++i)
            dic.Add(upgradeDatas[i].chName, upgradeDatas[i]);
        
        return dic;
    }

    private static void LoadCharacterUpgradeData()
    {
        string path = Application.dataPath + "/Resources/Data/CharacterUpgradeData.json";

        if (!File.Exists(path))
        {
            upgradeDatas = new UpgradeData[3];

            upgradeDatas[0] = new UpgradeData("Pea", new float[] { 1.0f, 2.0f, 3.0f, 4.0f }, UpgradeKind.Force);
            upgradeDatas[1] = new UpgradeData("Gas", new float[] { 1.1f, 2.1f, 3.1f, 4.1f }, UpgradeKind.Health);
            upgradeDatas[2] = new UpgradeData("Sun", new float[] { 1.2f, 2.2f, 3.2f, 4.2f }, UpgradeKind.SkillSpeed);

            string json = JsonHelper.ToJson(upgradeDatas, true);
            Debug.Log(json);
            File.WriteAllText(path, json);
        }
        else
        {
            string json = File.ReadAllText(path);
            upgradeDatas = JsonHelper.FromJson<UpgradeData>(json);
        }
    }

    // 한 페이즈의 Wave 데이터를 파싱하여 stage 별로 나눈 리스트를 반환.
    List<StageWave> WaveParse(string _CSVFileName)
    {
        List<StageWave> res = new List<StageWave>();

        TextAsset csvData = Resources.Load<TextAsset>($"Data/{_CSVFileName}");

        string[] data = csvData.text.Split(new char[] { '\n' });

        int count = data.Length;
        for(int i = 1; i < count;)
        {
            string[] elements = data[i].Split(new char[] { ',' });
            int currentStage = int.Parse(elements[0]);

            List<MonsterWave> waveList = new List<MonsterWave>();
            do
            {
                MonsterWave wave;

                wave.stage = int.Parse(elements[0]); // Stage
                wave.time = float.Parse(elements[1]); // time
                if (elements[2] == "first")
                {
                    ++i;
                    monsterWaveTimer.FirstwaveTime = (int)wave.time;
                    elements = data[i].Split(new char[] { ',' });
                    continue;
                }
                else if (elements[2] == "second")
                {
                    ++i;
                    monsterWaveTimer.SecondWaveTime = (int)wave.time;
                    elements = data[i].Split(new char[] { ',' });
                    continue;
                }
                int monsterNum = int.Parse(elements[2]);
                wave.monsterInfo = Resources.Load<GameObject>($"Prefabs/Monsters/{(MonsterName)monsterNum}"); // monster
                wave.line = int.Parse(elements[3]); // line

                if (++i < count)
                {
                    waveList.Add(wave);
                    elements = data[i].Split(new char[] { ',' });
                }
                else
                {
                    waveList.Add(wave);
                    break;
                }
            }
            while (int.Parse(elements[0]) == currentStage);

            StageWave waveSet = new StageWave(waveList);
            res.Add(waveSet);
        }

        return res;
    }
}

