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
    private PhaseStage phaseStage = new PhaseStage();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // phaseStage�� json���� �����ϴ� �Լ�
    public void SavePhaseStageToJson()
	{
        string jsonData = JsonUtility.ToJson(phaseStage, true);
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "PhaseStage.json");
        File.WriteAllText(path, jsonData);
	}

    // stage ��ư�� ����Ǵ� OnClick �Լ�
    public void stageButtonClick(string level)
	{
        phaseStage.phase = level[0] - '0';
        phaseStage.stage = level[1] - '0';

        SavePhaseStageToJson();
        LoadCharacterSelectScene();
    }

    private void LoadCharacterSelectScene()
	{
        SceneManager.LoadScene("CharacterSelectScene");
	}
}
