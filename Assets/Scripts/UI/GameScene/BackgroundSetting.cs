using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BackgroundSetting : MonoBehaviour
{
    private SpriteRenderer background; // ��� ��������Ʈ
    [SerializeField]
    private Sprite map2Background;
    [SerializeField]
    private Sprite map3Background;

    private PhaseStage selectPS = new PhaseStage();

	private void Awake()
	{
        background = GetComponent<SpriteRenderer>();
        LoadSelectPhaseStageFromJson();

        // selectPS.phase == 1 �� ���� ���� �״��
        if(selectPS.phase == 2)
		{
            background.sprite = map2Background;
		}
		else if(selectPS.phase == 3)
		{
            background.sprite = map3Background;
		}
    }

	//  json�� phaseStage�� �ε��ϴ� �Լ�
	private void LoadSelectPhaseStageFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "selectPhaseStage.json");
        string jsonData = File.ReadAllText(path);
        selectPS = JsonUtility.FromJson<PhaseStage>(jsonData);
    }

}
