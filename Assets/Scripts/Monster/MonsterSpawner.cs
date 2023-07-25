using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private static MonsterSpawner instance;
    public static MonsterSpawner GetInstance { get { Init(); return instance; } }

    // ������, ���������� �ε��� �����δ� 0������ ����
    public int          phase = 1;
    public int          stage = 1;
    public GameObject[] lines = new GameObject[5];

    private int           count = 0; // �� ���������� ����(���� ���� ��)
    private int           idx = 0;
    private MonsterWave[] currentWave = null;
    public  float         monsterBuffTime = 0;

    [SerializeField]
    private LinkedList<GameObject>[] monsterList = new LinkedList<GameObject>[5];

    void Start()
    {
        Init();

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

            obj.GetComponent<Monster>().CurrentLine = currentWave[idx].line; // ���� ���� �Ҵ�
            obj.GetComponent<SpriteRenderer>().sortingLayerName = $"Line{currentWave[idx].line}";
            obj.GetComponent<SpriteRenderer>().sortingOrder = idx + 1; // sorting order 0 is for characters

            if (obj != null) { monsterList[currentWave[idx].line].AddLast(obj); } // ����
            ++idx;
        }
    }

    private static void Init()
    {
        if(instance == null)
        {
            GameObject go = GameObject.Find("MonsterSpawner");
            if(go == null)
            {
                go = new GameObject("MonsterSpawner");
                go.AddComponent<MonsterSpawner>();
            }

            instance = go.GetComponent<MonsterSpawner>();
        }
    }

    public void RemoveMonster(GameObject obj, int line)
    {
        if (monsterList[line].Remove(obj))
            Debug.Log("Removed Monster");
    }

    public void BuffMonsters()
    {
        for(int i = 0; i < monsterList.Length; ++i)
        {
            foreach (var item in monsterList[i])
            {
                item.GetComponent<Monster>().ChangeStatus(10, 0.2f, 500);
            }
        }
        Invoke("NerfMonsters", monsterBuffTime);
    }

    private void NerfMonsters()
    {
        for (int i = 0; i < monsterList.Length; ++i)
        {
            foreach (var item in monsterList[i])
            {
                item.GetComponent<Monster>().ChangeStatus();
            }
        }
    }
}
