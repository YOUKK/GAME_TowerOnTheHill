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
        // �÷����� ������������ ���� ���� ���� ǥ��
        coinText.text = GamePlayManagers.Instance.GetEarnedCoin.ToString();

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

}
