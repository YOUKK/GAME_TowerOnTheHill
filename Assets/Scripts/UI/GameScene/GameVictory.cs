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

    private PhaseStage winPS = new PhaseStage(); // 현재 클리어한 곳까지의 맵, 스테이지 정보
    private PhaseStage selectPS = new PhaseStage(); // 선택한 맵, 스테이지 정보

    void OnEnable()
    {
        // 버튼 클릭 시 호출될 함수 추가
        restartButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("CharacterSelectScene"));
        nextButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("LevelSelectScene"));
        // 플레이한 스테이지에서 얻은 코인 값을 표시
        coinText.text = GamePlayManagers.Instance.GetEarnedCoin.ToString();
        // TODO : 얻은 캐릭터 표시

        if (GamePlayManagers.Instance.IsGameClear || gameClear)
        {
            // 스테이지 clear 정보 업데이트
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

    //  json을 phaseStage로 로드하는 함수
    private void LoadWinPhaseStageFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "winPhaseStage.json");
        string jsonData = File.ReadAllText(path);
        winPS = JsonUtility.FromJson<PhaseStage>(jsonData);
    }

    //  json을 phaseStage로 로드하는 함수
    private void LoadSelectPhaseStageFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "selectPhaseStage.json");
        string jsonData = File.ReadAllText(path);
        selectPS = JsonUtility.FromJson<PhaseStage>(jsonData);
    }

    // phaseStage를 json으로 저장하는 함수
    private void SavePhaseStageToJson()
    {
        string jsonData = JsonUtility.ToJson(winPS, true);
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "winPhaseStage.json");
        File.WriteAllText(path, jsonData);
    }
}
