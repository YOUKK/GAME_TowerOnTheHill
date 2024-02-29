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

    protected PhaseStage winPS; // 현재 클리어한 곳까지의 맵, 스테이지 정보
    protected PhaseStage selectPS; // 선택한 맵, 스테이지 정보



    private void OnEnable()
    { 
        // 버튼 클릭 시 호출될 함수 추가
        restartButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("GamePlayScene", "CharacterSelectScene"));
        nextButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("GamePlayScene", "LevelSelectScene"));
        // 플레이한 스테이지에서 얻은 코인 값을 표시
        coinText.text = GamePlayManagers.Instance.GetEarnedCoin.ToString();

        // 스테이지 clear 정보 업데이트
        if (!(MonsterSpawner.GetInstance.phase == 9)) // 튜토리얼씬이 아닐 때만
        {
            GamePlayManagers.Instance.LoadWinPhaseStageFromJson();
            GamePlayManagers.Instance.LoadSelectPhaseStageFromJson();
            winPS = GamePlayManagers.Instance.winPS; // 얕은 복사
            selectPS = GamePlayManagers.Instance.selectPS; // 얕은 복사
            if (selectPS.phase > winPS.phase || (selectPS.phase == winPS.phase && selectPS.stage > winPS.stage))
            {
                winPS.phase = selectPS.phase;
                winPS.stage = selectPS.stage;
                GamePlayManagers.Instance.SaveWinPhaseStageToJson();

                if (!((winPS.phase == 1 && winPS.stage == 4) || (winPS.phase == 2 && winPS.stage == 4) || (winPS.phase == 3 && winPS.stage == 3) || (winPS.phase == 3 && winPS.stage == 4) || (winPS.phase == 3 && winPS.stage == 5)))
                    UnlockCharacter(); // 스테이지에 따른 캐릭터 해금

                if ((winPS.phase == 1 && winPS.stage == 1) || (winPS.phase == 1 && winPS.stage == 2) || (winPS.phase == 1 && winPS.stage == 3) || (winPS.phase == 1 && winPS.stage == 5))
                    UnlockSlot();
            }
        }

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayEffect("Win");
        Debug.Log("게임 클리어시 데이터 업데이트");
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
