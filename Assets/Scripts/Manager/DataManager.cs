using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;


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

// �� ���������� Monster Wave ����
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

// ���� �ϳ��� ���� Wave ����
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
            // ĳ������ ������ ���ϴ� ���� : ���� ���� �ð� ������ �� ����.
            // ���� : ������ �̿��Ͽ� �ٲ� �� �ִ� �����͵��� �ҷ����� ������ �� ���� ���۸��� �ҷ����ִ� ���� ȿ�����̴�.
            // � ������ �ҷ��;� �ұ�?
            // �ش� ĳ������ ������ ��ġ ������ �����;� ��. ��, UpgradeData�� �ʿ���.
            // Idea : UpgradeData�� �÷��̾��� �̸��� Key�� ���� ���� status ���� value�� ����...
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
        // json ���·� �� ���ڿ� ����
        string json = JsonUtility.ToJson(shopData);
        // ���� ���� �� ����
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
            File.WriteAllText(path, json);
        }
        else
        {
            string json = File.ReadAllText(path);
            upgradeDatas = JsonHelper.FromJson<UpgradeData>(json);
        }
    }

    // �� �������� Wave �����͸� �Ľ��Ͽ� stage ���� ���� ����Ʈ�� ��ȯ.
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

