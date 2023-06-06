using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager GetData { get { Init(); return instance; } }

    public static List<List<StageWave>> monsterWave = new List<List<StageWave>>();

    void Awake()
    {
        Init();

        monsterWave.Add(WaveParse("MonsterWaveDB - Phase0"));

        for(int i = 0; i < monsterWave[0].Count; ++i)
        {
            Debug.Log(monsterWave[0][i].waveArray.Length);
            for(int j = 0; j < monsterWave[0][i].waveArray.Length; ++j)
            {
                MonsterWave wave = monsterWave[0][i].waveArray[j];
                Debug.Log($"stage {wave.stage} , time {wave.time} , monster {wave.monsterInfo.name} , line {wave.line}");
            }
        }
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

    // 한 페이즈의 Wave 데이터를 파싱하여 stage 별로 나눈 리스트를 반환.
    List<StageWave> WaveParse(string _CSVFileName)
    {
        List<StageWave> res = new List<StageWave>();

        TextAsset csvData = Resources.Load<TextAsset>($"Waves/{_CSVFileName}");

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
    public int          stage;
    public float        time;
    public GameObject   monsterInfo;
    public int          line;
}
