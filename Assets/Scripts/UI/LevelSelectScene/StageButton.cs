using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


// json파일로 저장하기 위해 만든 class
// 선택한 스테이지 버튼에 따라 phase와 stage가 저장된다.
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

    // phaseStage를 json으로 저장하는 함수
    public void SavePhaseStageToJson()
	{
        string jsonData = JsonUtility.ToJson(phaseStage, true);
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "PhaseStage.json");
        File.WriteAllText(path, jsonData);
	}

    // stage 버튼에 연결되는 OnClick 함수
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
