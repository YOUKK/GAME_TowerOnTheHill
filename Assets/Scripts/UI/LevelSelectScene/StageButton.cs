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
	// stage ��ư�� ����Ǵ� OnClick �Լ�
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
