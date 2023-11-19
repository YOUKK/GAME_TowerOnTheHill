using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameVictory : MonoBehaviour
{
    [SerializeField]
    private GameObject victoryText;

    private PhaseStage winPS = new PhaseStage();
    private PhaseStage selectPS = new PhaseStage();

    public bool gameClear = false;

    void Start()
    {
        
    }

    void Update()
    {
		if (MonsterSpawner.GetInstance.IsAllMonsterDead)
        //if(gameClear)
		{
            victoryText.SetActive(true);
            Debug.Log("�¸�!!!!");

            // �������� clear ���� ������Ʈ
            LoadWinPhaseStageFromJson();
            LoadSelectPhaseStageFromJson();
            if(selectPS.phase >= winPS.stage && selectPS.stage > winPS.stage)
			{
                winPS.phase = selectPS.phase;
                winPS.stage = selectPS.stage;
                SavePhaseStageToJson();
            }
        }
    }

    //  json�� phaseStage�� �ε��ϴ� �Լ�
    private void LoadWinPhaseStageFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "winPhaseStage.json");
        string jsonData = File.ReadAllText(path);
        winPS = JsonUtility.FromJson<PhaseStage>(jsonData);
    }

    //  json�� phaseStage�� �ε��ϴ� �Լ�
    private void LoadSelectPhaseStageFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "selectPhaseStage.json");
        string jsonData = File.ReadAllText(path);
        selectPS = JsonUtility.FromJson<PhaseStage>(jsonData);
    }

    // phaseStage�� json���� �����ϴ� �Լ�
    private void SavePhaseStageToJson()
    {
        string jsonData = JsonUtility.ToJson(winPS, true);
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "winPhaseStage.json");
        File.WriteAllText(path, jsonData);
    }
}
