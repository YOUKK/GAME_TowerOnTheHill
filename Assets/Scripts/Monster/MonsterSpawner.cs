using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public int          phase = 1;
    public int          stage = 1;
    public GameObject[] lines = new GameObject[5];

    private int           count = 0; // 한 스테이지의 길이(몬스터 생성 수)
    private int           idx = 0;
    private MonsterWave[] currentWave = null;

    [SerializeField]
    private LinkedList<GameObject>[] monsterList = new LinkedList<GameObject>[5];

    void Start()
    {
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
    }

    void Update()
    {
        if (idx >= count) return;

        if (currentWave[idx].time < Managers.TimeM.Sec)
        {
            GameObject obj = Instantiate(currentWave[idx].monsterInfo, 
                lines[currentWave[idx].line].transform.position, transform.rotation);

            obj.GetComponent<Monster>().SetLine(currentWave[idx].line); // 라인 정보 할당

            if (obj != null) { monsterList[currentWave[idx].line].AddLast(obj); } // 삽입
            ++idx;
        }
    }

    public void RemoveMonster(GameObject obj, int line)
    {
        if (monsterList[line].Remove(obj))
            Debug.Log("Removed Monster");
    }
}
