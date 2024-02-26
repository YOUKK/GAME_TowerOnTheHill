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
	// stage 버튼에 연결되는 OnClick 함수
	public void stageButtonClick(string level)
	{
		GamePlayManagers.Instance.selectPS.phase = level[0] - '0';
		GamePlayManagers.Instance.selectPS.stage = level[1] - '0';
		GamePlayManagers.Instance.SaveSelectPhaseStageToJson();

		LoadCharacterSelectScene();
    }

    private void LoadCharacterSelectScene()
	{        
        GameManager.GetInstance.MoveScene("LevelSelectScene", "CharacterSelectScene");
    }
}
