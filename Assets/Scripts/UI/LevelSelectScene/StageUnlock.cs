using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// levelSelectScene을 로딩할 때마다 클리어한 레벨까지 스테이지 버튼을 unlock하는 스크립트
// 각 스테이지 버튼에 대한 DoUnlock스크립트를 이용
public class StageUnlock : MonoBehaviour
{
    // 맨 처음 아무 스테이지도 안 깼을 때의 winPhaseStage는 Phase=1, Stage=0
    private PhaseStage winPS;

    void Start()
    {
        GamePlayManagers.Instance.LoadWinPhaseStageFromJson();
        winPS = GamePlayManagers.Instance.winPS;
        // 클리어한 스테이지의 다음 스테이지 구하기
        if (!(winPS.phase == 3 && winPS.stage == 5))
        {
            if (winPS.stage + 1 > 5)
            {
                winPS.phase++;
                winPS.stage = 1;
            }
            else
                winPS.stage++;
        }

        // 클리어한 스테이지까지 버튼을 unlock시킨다
        for (int i = 0; i < winPS.phase; i++)
        {
            GameObject mapButton = gameObject.transform.GetChild(i).gameObject;
            if (i < winPS.phase - 1)
            {
                for (int j = 0; j < 5; j++)
                    mapButton.transform.GetChild(j).GetComponent<DoUnlock>().UnLock();
            }
            else
            {
                for (int j = 0; j < winPS.stage; j++)
                    mapButton.transform.GetChild(j).GetComponent<DoUnlock>().UnLock();
            }
        }

    }
}
