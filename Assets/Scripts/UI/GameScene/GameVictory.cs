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

        // �������� clear ���� ������Ʈ
        if (!(MonsterSpawner.GetInstance.phase == 9)) // Ʃ�丮����� �ƴ� ����
        {
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
        }

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayEffect("Win");
        Debug.Log("���� Ŭ����� ������ ������Ʈ");
    }

    protected void UnlockSlot()
	{
        int slotNum = GameManager.GetInstance.GetPlayerData(PlayerDataKind.SlotNum);
        GameManager.GetInstance.SetPlayerData(PlayerDataKind.SlotNum, slotNum + 1);
    }

	protected void UnlockCharacter()
	{
        int chaUnlockLevel = GameManager.GetInstance.GetPlayerData(PlayerDataKind.ChaUnlockLevel);
        GameManager.GetInstance.SetPlayerData(PlayerDataKind.ChaUnlockLevel, chaUnlockLevel + 1);
    }
}
