using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class GameVictory : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public Image characterImage;
    public Button nextButton;
    public Button restartButton;

    private PhaseStage winPS = new PhaseStage(); // ���� Ŭ������ �������� ��, �������� ����
    private PhaseStage selectPS = new PhaseStage(); // ������ ��, �������� ����



    void OnEnable()
    {
        // ��ư Ŭ�� �� ȣ��� �Լ� �߰�
        restartButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("GamePlayScene", "CharacterSelectScene"));
        nextButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("GamePlayScene", "LevelSelectScene"));
        // �÷����� ������������ ���� ���� ���� ǥ��
        coinText.text = GamePlayManagers.Instance.GetEarnedCoin.ToString();
        // TODO : ���� ĳ���� ǥ��


        //if (GamePlayManagers.Instance.IsGameClear)
        //{
            // �������� clear ���� ������Ʈ
            LoadWinPhaseStageFromJson();
            LoadSelectPhaseStageFromJson();
            if (selectPS.phase > winPS.phase || (selectPS.phase == winPS.phase && selectPS.stage > winPS.stage))
            {
                winPS.phase = selectPS.phase;
                winPS.stage = selectPS.stage;
                SavePhaseStageToJson();

                if (!((winPS.phase == 1 && winPS.stage == 4) || (winPS.phase == 2 && winPS.stage == 4) || (winPS.phase == 3 && winPS.stage == 3) || (winPS.phase == 3 && winPS.stage == 4) || (winPS.phase == 3 && winPS.stage == 5)))
                    UnlockCharacter(); // ���������� ���� ĳ���� �ر�

                if ((winPS.phase == 1 && winPS.stage == 1) || (winPS.phase == 1 && winPS.stage == 2) || (winPS.phase == 1 && winPS.stage == 3) || (winPS.phase == 1 && winPS.stage == 5))
                    UnlockSlot();
            }

            Debug.Log("���� Ŭ����� ������ ������Ʈ");
        //}
    }

    private void UnlockSlot()
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

	private void UnlockCharacter()
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
