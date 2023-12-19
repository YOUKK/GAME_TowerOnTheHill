using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

// 상점 데이터, 상점 캐릭터 업데이트 데이터 => json
// 몬스터 웨이브 데이터 => csv

#region Shop Data
[System.Serializable]
public class ShopData
{
    public bool             hasHammer;
    public bool             hasSeatExpansion;
    public int              slotLevel;
    public ShopData()
    {
        hasHammer = false;
        hasSeatExpansion = false;
        slotLevel = 0;
    }
}

public enum UpgradeKind { Force, SkillSpeed, Health }

[System.Serializable]
public class UpgradeData
{
    public string chName;
    public float[] statIncrease;
    public int currentLevel;
    public UpgradeKind kind;

    public UpgradeData(string _name, int _currLevel, float[] _statArray, UpgradeKind _kind)
    {
        chName = _name;
        currentLevel = _currLevel;
        statIncrease = _statArray;
        kind = _kind;
    }
}
#endregion

// 한 스테이지의 Monster Wave 정보
public class StageWave
{
    public TutorialLine[] waveTuArray = null;
    public MonsterSpawnData[] waveArray = null;

    public StageWave(MonsterSpawnData[] waves)
    {
        waveArray = waves;
    }

    public StageWave(List<MonsterSpawnData> waves)
    {
        waveArray = waves.ToArray();
    }

    public StageWave(List<TutorialLine> waves)
    {
        waveTuArray = waves.ToArray();
    }
}

// 몬스터 하나의 스폰 Wave 정보
public struct MonsterSpawnData
{
    public int stage;
    public float time;
    public GameObject monsterInfo;
    public int line;
}

public struct TutorialLine
{
    public int stage;
    public float time;
    public int startPosX;
    public int startPosY;
    public int lineCount;
    public string lines;
}


public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager GetData { get { Init(); return instance; } }

    private static string shopDataPath;
    private static string characterDataPath;
    private static UpgradeData[] upgradeDatas;
    private float firstWaveTime;
    public float FirstWaveTime { get => firstWaveTime; }
    private float secondWaveTime;
    public float SecondWaveTime { get => secondWaveTime; }


    private string[] phaseFileNames = 
        {"", "MonsterWaveDB - Phase1", "MonsterWaveDB - Phase2", "MonsterWaveDB - Phase3",
        "MonsterWaveDB - Phase4", "MonsterWaveDB - Phase5","","","","MonsterWaveDB - Phase9"};

    public List<List<StageWave>> monsterWave = new List<List<StageWave>>();
    public List<List<StageWave>> tutorial = new List<List<StageWave>>();

    void Awake()
    {
        Init();

        shopDataPath = Application.dataPath + "/Resources/Data/shopData.json";
        characterDataPath = Application.dataPath + "/Resources/Data/CharacterUpgradeData.json";
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

    public ShopData GetShopData()
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

    // json save
    public void SaveShopData(ShopData shopData)
    {
        // json 형태로 된 문자열 생성
        string json = JsonUtility.ToJson(shopData);
        // 파일 생성 및 저장
        File.WriteAllText(shopDataPath, json);
    }
    
    // json load
    public Dictionary<string, UpgradeData> GetUpgradeDataDic()
    {
        if (upgradeDatas == null) LoadCharacterUpgradeData();
        
        Dictionary<string, UpgradeData> dic = new Dictionary<string, UpgradeData>();

        for(int i = 0; i < upgradeDatas.Length; ++i)
            dic.Add(upgradeDatas[i].chName, upgradeDatas[i]);
        
        return dic;
    }

    // json save
    public void SaveCharacterUpgradeData()
    {
        string json = JsonHelper.ToJson(upgradeDatas, true);
        File.WriteAllText(characterDataPath, json);
    }

    // json load
    private void LoadCharacterUpgradeData()
    {
        if (!File.Exists(characterDataPath))
        {
            upgradeDatas = new UpgradeData[7];

            upgradeDatas[0] = new UpgradeData("PeaShooter", 0, new float[] { 10, 12, 14, 17, 20 }, UpgradeKind.Force);
            upgradeDatas[1] = new UpgradeData("PeaShooter2", 0, new float[] { 7, 9, 11, 13, 15 }, UpgradeKind.Force);
            upgradeDatas[2] = new UpgradeData("SunFlower", 0, new float[] { 10, 9.5f, 9, 8, 7 }, UpgradeKind.SkillSpeed);
            upgradeDatas[3] = new UpgradeData("Walnut", 0, new float[] { 200, 210, 220, 230, 240 }, UpgradeKind.Health);
            upgradeDatas[4] = new UpgradeData("Eater", 0, new float[] { 5, 4.6f, 4.2f, 3.8f, 3.2f }, UpgradeKind.SkillSpeed);
            upgradeDatas[5] = new UpgradeData("GasMushroom", 0, new float[] { 5, 6, 7, 8, 10 }, UpgradeKind.Force);
            upgradeDatas[6] = new UpgradeData("IceShooter", 0, new float[] { 10, 11, 12, 13, 14 }, UpgradeKind.Force);


            string json = JsonHelper.ToJson(upgradeDatas, true);
            Debug.Log(json);
            File.WriteAllText(characterDataPath, json);
        }
        else
        {
            string json = File.ReadAllText(characterDataPath);
            upgradeDatas = JsonHelper.FromJson<UpgradeData>(json);
        }
    }

    public StageWave TryParse(int phase, int stage)
    {
        TextAsset csvData = Resources.Load<TextAsset>($"Data/{phaseFileNames[phase]}");
        print(csvData);
        string[] data = csvData.text.Split(new char[] { '\n' });
        StageWave stageWave = WaveParse(data, stage);

        if (stageWave == null) Debug.LogError("STAGE WAVE IS NULL");
        return stageWave;
    }

    // _CSVFileName에서 파라미터로 받은 stage에 해당하는 Wave를 StageWave로 만들어 반환.
    private StageWave WaveParse(string[] data, int stage)
    {
        int count = data.Length;
        for(int i = 1; i < count;)
        {
            string[] elements = data[i].Split(new char[] { ',' });
            int currentStage = int.Parse(elements[0]);
            if (currentStage != stage) { ++i; continue; }

            List<MonsterSpawnData> waveList = new List<MonsterSpawnData>();

            while (int.Parse(elements[0]) == currentStage)
            {
                MonsterSpawnData wave;

                wave.stage = int.Parse(elements[0]); // Stage
                wave.time = float.Parse(elements[1]); // time
                
                if (elements[2] == "first")
                {
                    ++i;
                    firstWaveTime = (int)wave.time;
                    elements = data[i].Split(new char[] { ',' });
                    continue;
                }
                else if (elements[2] == "second")
                {
                    ++i;
                    secondWaveTime = (int)wave.time;
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

            StageWave waveSet = new StageWave(waveList);
            return waveSet;
        }

        return null;
    }
}

