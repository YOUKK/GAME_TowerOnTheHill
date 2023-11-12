using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// levelSelectScene�� �ε��� ������ Ŭ������ �������� �������� ��ư�� unlock�ϴ� ��ũ��Ʈ
// �� �������� ��ư�� ���� DoUnlock��ũ��Ʈ�� �̿�
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
