using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// 선택한 맵에 따라 백그라운드를 설정하는 스크립트
// GamePlayScene에서 Background 오브젝트에 붙는다.
// CharacterSeletScene에서 Background 오브젝트에 붙는다.

public class BackgroundSetting : MonoBehaviour
{
    private SpriteRenderer background; // 배경 스프라이트
    [SerializeField]
    private Sprite map2Background;
    [SerializeField]
    private Sprite map3Background;

    private PhaseStage selectPS = new PhaseStage();

	private void Awake()
	{
        background = GetComponent<SpriteRenderer>();
        LoadSelectPhaseStageFromJson();

        // selectPS.phase == 1 인 경우는 원래 그대로
        if(selectPS.phase == 2)
		{
            background.sprite = map2Background;
		}
		else if(selectPS.phase == 3)
		{
            background.sprite = map3Background;
		}
    }

	//  json을 phaseStage로 로드하는 함수
	private void LoadSelectPhaseStageFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "selectPhaseStage.json");
        string jsonData = File.ReadAllText(path);
        selectPS = JsonUtility.FromJson<PhaseStage>(jsonData);
    }

}
