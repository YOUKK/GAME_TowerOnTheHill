using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameVictory : MonoBehaviour
{
    private PhaseStage winPS = new PhaseStage();
    private PhaseStage selectPS = new PhaseStage();

    public bool gameClear = false;

    void OnEnable()
    {
        // TODO : ���� �� ĳ���� UI ǥ�� and ��ư�� GameManager�� �� �̵� �Լ� ����

        if (MonsterSpawner.GetInstance.IsAllMonsterDead || gameClear)
        {
            // �������� clear ���� ������Ʈ
            LoadWinPhaseStageFromJson();
            LoadSelectPhaseStageFromJson();
            if (selectPS.phase > winPS.phase || (selectPS.phase == winPS.phase && selectPS.stage > winPS.stage))
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
