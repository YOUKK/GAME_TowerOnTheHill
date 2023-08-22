using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ShopData
{
    public bool buyHammer;
    public bool buySeatExpansion;
    public int slotLevel;
    public CharacterLv[] chLvs;
    public ShopData()
    {
        buyHammer = false;
        buySeatExpansion = false;
        slotLevel = 0;
        chLvs = null;
    }
}

[System.Serializable]
public struct CharacterLv
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

    public static List<List<StageWave>> monsterWave = new List<List<StageWave>>();

    void Awake()
    {
        Init();

        Scene currScene = SceneManager.GetActiveScene();
        if(currScene.name == "Shop")
        {
            
        }
        else TryParse();

        shopDataPath = Application.dataPath + "/Resources/Data/shopData.json";
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
    
    public static UpgradeData[] LoadCharacterUpgradeData()
    {
        string path = Application.dataPath + "/Resources/Data/CharacterUpgradeData.json";
        UpgradeData[] datas;

        if (!File.Exists(path))
        {
            Debug.Log("Character Upgrade Data File is Created");
            datas = new UpgradeData[3];

            datas[0] = new UpgradeData("Pea", new float[] { 1.0f, 2.0f, 3.0f, 4.0f }, UpgradeKind.Force);
            datas[1] = new UpgradeData("Gas", new float[] { 1.1f, 2.1f, 3.1f, 4.1f }, UpgradeKind.Health);
            datas[2] = new UpgradeData("Sun", new float[] { 1.2f, 2.2f, 3.2f, 4.2f }, UpgradeKind.SkillSpeed);

            string json = JsonHelper.ToJson(datas, true);
            Debug.Log(json);
            File.WriteAllText(path, json);
        }
        else
        {
            string json = File.ReadAllText(path);
            datas = JsonHelper.FromJson<UpgradeData>(json);
        }

        return datas;
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

