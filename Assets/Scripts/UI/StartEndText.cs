using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartEndText : MonoBehaviour
{
    [SerializeField]
    private GameObject ready;
    [SerializeField]
    private GameObject start;

    void Start()
    {
        StartCoroutine(StartText());
    }

    void Update()
    {
    }

    IEnumerator StartText()
	{
        ready.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        ready.SetActive(false);
        start.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        start.SetActive(false);

        // ���� �ؽ�Ʈ UI ��� �� ������ �����ϹǷ� �̶����� Ÿ�̸� ����
        Managers.TimeM.StartTimer();
	}

    
}
