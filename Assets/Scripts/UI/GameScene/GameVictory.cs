using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class GameVictory : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI coinText;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button restartButton;

    protected PhaseStage winPS; // ���� Ŭ������ �������� ��, �������� ����
    protected PhaseStage selectPS; // ������ ��, �������� ����



    private void OnEnable()
    { 
        // ��ư Ŭ�� �� ȣ��� �Լ� �߰�
        restartButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("GamePlayScene", "CharacterSelectScene"));
        nextButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("GamePlayScene", "LevelSelectScene"));
        // �÷����� ������������ ���� ���� ���� ǥ��
        coinText.text = GamePlayManagers.Instance.GetEarnedCoin.ToString();
        // TODO : ���� ĳ���� ǥ��

        // �������� clear ���� ������Ʈ
        GamePlayManagers.Instance.LoadWinPhaseStageFromJson();
        GamePlayManagers.Instance.LoadSelectPhaseStageFromJson();
        winPS = GamePlayManagers.Instance.winPS; // ���� ����
        selectPS = GamePlayManagers.Instance.selectPS; // ���� ����
        if (selectPS.phase > winPS.phase || (selectPS.phase == winPS.phase && selectPS.stage > winPS.stage))
        {
            winPS.phase = selectPS.phase;
            winPS.stage = selectPS.stage;
            GamePlayManagers.Instance.SaveWinPhaseStageToJson();

            if (!((winPS.phase == 1 && winPS.stage == 4) || (winPS.phase == 2 && winPS.stage == 4) || (winPS.phase == 3 && winPS.stage == 3) || (winPS.phase == 3 && winPS.stage == 4) || (winPS.phase == 3 && winPS.stage == 5)))
                UnlockCharacter(); // ���������� ���� ĳ���� �ر�

            if ((winPS.phase == 1 && winPS.stage == 1) || (winPS.phase == 1 && winPS.stage == 2) || (winPS.phase == 1 && winPS.stage == 3) || (winPS.phase == 1 && winPS.stage == 5))
                UnlockSlot();
        }

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayEffect("Win");
        Debug.Log("���� Ŭ����� ������ ������Ʈ");
    }

    protected void UnlockSlot()
	{
        if (PlayerPrefs.HasKey("slotNum"))
        {
            PlayerPrefs.SetInt("slotNum", PlayerPrefs.GetInt("slotNum") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("slotNum", 2);
        }
    }

	protected void UnlockCharacter()
	{
		if (PlayerPrefs.HasKey("chaUnlockLevel"))
		{
            PlayerPrefs.SetInt("chaUnlockLevel", PlayerPrefs.GetInt("chaUnlockLevel") + 1);
        }
		else
		{
            PlayerPrefs.SetInt("chaUnlockLevel", 3);
        }

        Debug.Log("chaUnlockLevel in gamevictory " + PlayerPrefs.GetInt("chaUnlockLevel"));
    }
}
