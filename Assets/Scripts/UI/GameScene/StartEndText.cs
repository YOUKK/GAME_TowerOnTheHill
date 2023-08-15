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

        // 시작 텍스트 UI 띄운 후 게임이 시작하므로 이때부터 타이머 시작
        Managers.TimeM.StartTimer();
	}

    
}
