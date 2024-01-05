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
    private PhaseStage selectPhaseStage = new PhaseStage();

    void Start()
    {
        text = transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
        selectedCharacter = GameObject.Find("SelectedCanvas").GetComponent<SelectedCharacter>();

        LoadSelectPhaseStageFromJson();
    }

    void Update()
    {
        
    }

    private void LoadSelectPhaseStageFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "selectPhaseStage.json");
        string jsonData = File.ReadAllText(path);
        selectPhaseStage = JsonUtility.FromJson<PhaseStage>(jsonData);
    }

    // StartButton 클릭시 1. 캐릭터 선택 정보 json으로 저장됨 2. 게임플레이씬으로 전환
    public void ClickButton()
	{
        // json 파일 저장
        selectedCharacter.SaveButtonListToJson();

        if (selectPhaseStage.phase == 3 && selectPhaseStage.stage == 5)
        {
            GameManager.GetInstance.MoveScene("CharacterSelectScene", "BossWave");
        }

        // 씬 이동
        GameManager.GetInstance.MoveScene("CharacterSelectScene", "GamePlayScene");
        //SoundManager.Instance.PlayBGM("Battle");
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
