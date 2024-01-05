using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


// json���Ϸ� �����ϱ� ���� ���� class
// ������ �������� ��ư�� ���� phase�� stage�� ����ȴ�.
[System.Serializable]
public class PhaseStage
{
    public int phase;
    public int stage;
}

public class StageButton : MonoBehaviour
{
    private PhaseStage selectPhaseStage = new PhaseStage();

    void Start()
    {

    }

    void Update()
    {
        
    }

    // phaseStage�� json���� �����ϴ� �Լ�
    private void SavePhaseStageToJson()
	{
        string jsonData = JsonUtility.ToJson(selectPhaseStage, true);
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "selectPhaseStage.json");
        File.WriteAllText(path, jsonData);
	}

    // stage ��ư�� ����Ǵ� OnClick �Լ�
    public void stageButtonClick(string level)
	{
        selectPhaseStage.phase = level[0] - '0';
        selectPhaseStage.stage = level[1] - '0';

        SavePhaseStageToJson();
        LoadCharacterSelectScene();
    }

    private void LoadCharacterSelectScene()
	{        
        GameManager.GetInstance.MoveScene("LevelSelectScene", "CharacterSelectScene");
    }
}
