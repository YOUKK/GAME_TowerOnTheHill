using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public TextAsset data;
    private MonsterWaveDB waveDB;

    void Awake()
    {
        waveDB = JsonUtility.FromJson<MonsterWaveDB>(data.text);

        foreach (var item in waveDB.prototypeWave)
        {
            Debug.Log("Test time " + item.time);
        }
    }

    void Update()
    {
        
    }
}

[System.Serializable]
public class MonsterWaveDB
{
    public MonsterWave[] prototypeWave;
}

[System.Serializable]
public class MonsterWave
{
    public GameObject   monsterInfo;
    public string       stage;
    public float        time;
    public int          line;

    public MonsterWave(string _stage, float _time, int _monsterInfo, int _line)
    {
        stage = _stage;
        time = _time;
        line = _line;
        monsterInfo = Resources.Load<GameObject>($"Prefab/{(MonsterName)_monsterInfo}");
        if (monsterInfo == null) Debug.Log("No Monster Object");
    }
}