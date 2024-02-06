using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class StartBackButton : MonoBehaviour
{
    private TextMeshProUGUI text;
    private SelectedCharacter selectedCharacter;
    private PhaseStage selectPhaseStage;

    void Start()
    {
        text = transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
        selectedCharacter = GameObject.Find("SelectedCanvas").GetComponent<SelectedCharacter>();

        GamePlayManagers.Instance.LoadSelectPhaseStageFromJson();
        selectPhaseStage = GamePlayManagers.Instance.selectPS;
    }

    void Update()
    {
        
    }

    // StartButton Ŭ���� 1. ĳ���� ���� ���� json���� ����� 2. �����÷��̾����� ��ȯ
    public void ClickButton()
	{
        // json ���� ����
        selectedCharacter.SaveButtonListToJson();

        // �� �̵�
        if (selectPhaseStage.phase == 3 && selectPhaseStage.stage == 5)
        {
            GameManager.GetInstance.MoveScene("CharacterSelectScene", "BossWave");
        }
		else
		{
            GameManager.GetInstance.MoveScene("CharacterSelectScene", "GamePlayScene");
        }
	}

    public void CanPressButton()
	{
        transform.GetComponent<Button>().interactable = true;
        text.color = new Color(1, 196/255f, 175/255f, 1);
	}

    public void CannotPressButton()
	{
        transform.GetComponent<Button>().interactable = false;
        text.color = new Color(1, 196 / 255f, 175 / 255f, 0.5f);
    }

    public void Back()
	{
        GameManager.GetInstance.MoveScene("CharacterSelectScene" ,"LevelSelectScene");
	}
}
