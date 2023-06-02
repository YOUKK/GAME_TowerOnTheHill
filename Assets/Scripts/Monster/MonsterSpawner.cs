using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public  bool         monsterCreate = false;
    public  int          lineNum;
    public  GameObject   monster;
    public  GameObject[] lines = new GameObject[5];
    private List<List<StageWave>> waveList = null;

    void Start()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        
    }
}
