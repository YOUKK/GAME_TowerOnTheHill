using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// levelSelectScene을 로딩할 때마다 클리어한 레벨까지 스테이지 버튼을 unlock하는 스크립트
// 각 스테이지 버튼에 대한 DoUnlock스크립트를 이용
public class StageUnlock : MonoBehaviour
{
    private PhaseStage winPS = new PhaseStage();

    //private List<GameObject> stageButtons;

    void Start()
    {
        //stageButtons = new List<GameObject>();
        for(int i = 0; i < winPS.phase; i++)
		{
            GameObject mapButton = gameObject.transform.GetChild(i).gameObject;
            for(int j = 0; j < winPS.stage; j++)
			{
                //stageButtons.Add(mapButton.transform.GetChild(j).gameObject);

                //mapButton.transform.GetChild(j).GetComponent<DoUnlock>().UnLock();
			}
		}

        LoadWinPhaseStageFromJson();
    }

    void Update()
    {
        
    }

    private void LoadWinPhaseStageFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "winPhaseStage.json");
        string jsonData = File.ReadAllText(path);
        winPS = JsonUtility.FromJson<PhaseStage>(jsonData);
    }
}
