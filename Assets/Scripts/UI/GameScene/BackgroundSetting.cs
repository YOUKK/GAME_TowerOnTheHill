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

	private void Awake()
	{
        background = GetComponent<SpriteRenderer>();
        GamePlayManagers.Instance.LoadSelectPhaseStageFromJson();
        int phase = GamePlayManagers.Instance.selectPS.phase;

        // selectPS.phase == 1 인 경우는 원래 그대로
        if(phase == 2)
		{
            background.sprite = map2Background;
		}
		else if(phase == 3)
		{
            background.sprite = map3Background;
		}
    }
}
