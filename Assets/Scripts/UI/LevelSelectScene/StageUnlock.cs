using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// levelSelectScene�� �ε��� ������ Ŭ������ �������� �������� ��ư�� unlock�ϴ� ��ũ��Ʈ
// �� �������� ��ư�� ���� DoUnlock��ũ��Ʈ�� �̿�
public class StageUnlock : MonoBehaviour
{
    private PhaseStage winPS = new PhaseStage();

    void Start()
    {
        LoadWinPhaseStageFromJson();

        // Ŭ������ ������������ ��ư�� unlock��Ų��
        for (int i = 0; i < winPS.phase; i++)
        {
            GameObject mapButton = gameObject.transform.GetChild(i).gameObject;
            if (i < winPS.phase - 1)
            {
                for(int j = 0; j < 5; j++)
                    mapButton.transform.GetChild(j).GetComponent<DoUnlock>().UnLock();
            }
            else
            {
                for (int j = 0; j < winPS.stage; j++)
                    mapButton.transform.GetChild(j).GetComponent<DoUnlock>().UnLock();
            }
        }
        
    }

    void Update()
    {
        
    }

    // Ŭ������ ���������� ���� ��������
    private void LoadWinPhaseStageFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Data/", "winPhaseStage.json");
        string jsonData = File.ReadAllText(path);
        winPS = JsonUtility.FromJson<PhaseStage>(jsonData);
    }
}
