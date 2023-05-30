using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // monsterWave[Phase number][Stage number]
    public List<List<MonsterWaveSet>> monsterWave = new List<List<MonsterWaveSet>>();

    void Start()
    {
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

    void Update()
    {
        
    }

    List<MonsterWaveSet> WaveParse(string _CSVFileName)
    {
        List<MonsterWaveSet> res = new List<MonsterWaveSet>();

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

            MonsterWaveSet waveSet = new MonsterWaveSet(waveList);
            res.Add(waveSet);
        }

        return res;
    }
}

public class MonsterWaveSet
{
    public MonsterWave[] waveArray = null;

    public MonsterWaveSet(MonsterWave[] waves)
    {
        waveArray = waves;
    }

    public MonsterWaveSet(List<MonsterWave> waves)
    {
        waveArray = waves.ToArray();
    }
}

// 몬스터 하나의 스폰 웨이브 정보
public struct MonsterWave
{
    public int          stage;
    public float        time;
    public GameObject   monsterInfo;
    public int          line;
}
