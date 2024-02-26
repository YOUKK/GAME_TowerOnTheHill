using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// ������ �ʿ� ���� ��׶��带 �����ϴ� ��ũ��Ʈ
// GamePlayScene���� Background ������Ʈ�� �ٴ´�.
// CharacterSeletScene���� Background ������Ʈ�� �ٴ´�.

public class BackgroundSetting : MonoBehaviour
{
    private SpriteRenderer background; // ��� ��������Ʈ
    [SerializeField]
    private Sprite map2Background;
    [SerializeField]
    private Sprite map3Background;

	private void Awake()
	{
        background = GetComponent<SpriteRenderer>();
        GamePlayManagers.Instance.LoadSelectPhaseStageFromJson();
        int phase = GamePlayManagers.Instance.selectPS.phase;

        // selectPS.phase == 1 �� ���� ���� �״��
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
