using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVictoryCharacter : GameVictory
{
	[SerializeField]
	private Button unlockButton;
	[SerializeField]
	private MenuCanvas menuCanvas;

	private void OnEnable()
	{
		unlockButton.onClick.AddListener(() => menuCanvas.ActiveCharacterShow());

        LoadWinPhaseStageFromJson();
        LoadSelectPhaseStageFromJson();
        if (selectPS.phase > winPS.phase || (selectPS.phase == winPS.phase && selectPS.stage > winPS.stage))
        {
            winPS.phase = selectPS.phase;
            winPS.stage = selectPS.stage;
            SavePhaseStageToJson();

            if (!((winPS.phase == 1 && winPS.stage == 4) || (winPS.phase == 2 && winPS.stage == 4) || (winPS.phase == 3 && winPS.stage == 3) || (winPS.phase == 3 && winPS.stage == 4) || (winPS.phase == 3 && winPS.stage == 5)))
                UnlockCharacter(); // 스테이지에 따른 캐릭터 해금

            if ((winPS.phase == 1 && winPS.stage == 1) || (winPS.phase == 1 && winPS.stage == 2) || (winPS.phase == 1 && winPS.stage == 3) || (winPS.phase == 1 && winPS.stage == 5))
                UnlockSlot();
        }

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayEffect("Win");
        Debug.Log("게임 클리어시 데이터 업데이트");
    }

}
