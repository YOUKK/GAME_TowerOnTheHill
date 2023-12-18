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
    public bool gameClear = false;

    private PhaseStage winPS = new PhaseStage(); // ���� Ŭ������ �������� ��, �������� ����
    private PhaseStage selectPS = new PhaseStage(); // ������ ��, �������� ����

    void OnEnable()
    {
        // ��ư Ŭ�� �� ȣ��� �Լ� �߰�
        restartButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("CharacterSelectScene"));
        nextButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("LevelSelectScene"));
        // �÷����� ������������ ���� ���� ���� ǥ��
        coinText.text = GamePlayManagers.Instance.GetEarnedCoin.ToString();
        // TODO : ���� ĳ���� ǥ��

        if (GamePlayManagers.Instance.IsGameClear || gameClear)
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
