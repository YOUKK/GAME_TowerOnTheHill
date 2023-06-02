using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public bool         monsterCreate = false;
    public int          phase = 1;
    public int          stage = 1;
    public GameObject[] lines = new GameObject[5];
    
    private int           count = 0;
    private int           idx = 0;
    private MonsterWave[] currentWave = null;

    void Start()
    {
        if (phase - 1 < 0 || stage - 1 < 0) Debug.LogError("Wrong Phase or Stage number input");
        currentWave = DataManager.monsterWave[phase-1][stage-1].waveArray;
        count = currentWave.Length;

        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (idx >= count) return;

        if (currentWave[idx].time < Managers.TimeM.Sec)
        {
            Instantiate(currentWave[idx].monsterInfo, 
                lines[currentWave[idx].line].transform.position, transform.rotation);

            ++idx;
        }
    }
}
