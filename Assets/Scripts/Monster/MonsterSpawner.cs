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

    public LinkedList<GameObject>[] monstersInLine = new LinkedList<GameObject>[5];

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
            GameObject obj = Instantiate(currentWave[idx].monsterInfo, 
                lines[currentWave[idx].line].transform.position, transform.rotation);

            // 몬스터 생성 시, 생성될 라인의 Linked List의 뒤에 삽입.
            // 몬스터 사망 시, 해당 라인의 Linked List에서 삭제할 필요 있음.
            // 삭제는 MonsterSpawner에서 삭제하는 것이 좋을 듯. delegate로 삭제 함수를 호출하든,
            // update에서 null된 몬스터를 찾아 삭제하든.
            if (obj != null) monstersInLine[currentWave[idx].line].AddLast(obj); // 삽입
            ++idx;
        }
    }
}
