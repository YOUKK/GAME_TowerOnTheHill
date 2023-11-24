using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameVictory : MonoBehaviour
{
    [SerializeField]
    private GameObject victoryBox;

    private PhaseStage winPS = new PhaseStage();
    private PhaseStage selectPS = new PhaseStage();

    public bool gameClear = false;

    void Start()
    {
        
    }

    void Update()
    {
		//if ()
        if(MonsterSpawner.GetInstance.IsAllMonsterDead || gameClear)
		{
            if (victoryBox.activeInHierarchy == false)
            {
                victoryBox.SetActive(true);
            }
            Debug.Log("승리!!!!");

            // 스테이지 clear 정보 업데이트
            LoadWinPhaseStageFromJson();
            LoadSelectPhaseStageFromJson();
            if(selectPS.phase > winPS.phase || (selectPS.phase == winPS.phase && selectPS.stage > winPS.stage))
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
