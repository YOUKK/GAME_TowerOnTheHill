using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private TutorialLine[] currentTutorialLines;

    MonsterSpawner monsterSpawner;
    DataManager dataSet;

    public int phase;
    public int stage;

    // Start is called before the first frame update
    void Start()
    {
        dataSet = GameObject.Find("DataManager").GetComponent<DataManager>();
        monsterSpawner = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();
        phase = monsterSpawner.phase;
        stage = monsterSpawner.stage;

        dataSet.tutorial.Add(DataSet("TutorialWaveDB - Phase0"));
    }

    private void Update()
    {
        phase = monsterSpawner.phase;
        stage = monsterSpawner.stage;
    }

    public List<StageWave> DataSet(string _CSVFileName)
    {
        List<StageWave> res = new List<StageWave>();

        TextAsset csvData = Resources.Load<TextAsset>($"Data/{_CSVFileName}");

        string[] data = csvData.text.Split(new char[] { '\n' });
        int count = data.Length;

        for (int i = 1; i < data.Length; i++)
        {
            string[] elements = data[i].Split(new char[] { ',' });

            for (int j = 0; j < elements.Length; j++)
            {
                // stage 값이 null -> 5, 6번 라인에 값 존재
                if (int.Parse(elements[0]) == phase) continue;
                else PlayTutorial();
            }
        }
        return res;
    }
    public void PlayTutorial()
    {

    }
}
