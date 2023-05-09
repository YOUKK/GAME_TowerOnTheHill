using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterWaveTimer : MonoBehaviour
{
    private float firstAppearTime = 5f; // ù ���Ͱ� ��Ÿ���� �������� �ð�
    private float timeCheckInterval = 2f; // �ð� üũ ����
    private float firstWaveTime = 30f; // ù��° ���̺� = 2��
    private float secondWaveTime = 60f; // �ι�° ���̺� = 4��
    private bool firstAppear = false;
    private bool firstWave = false;

    private float startTime; // ���� ���۽ð�
    private float currentTime; // ���� �ð�

    private float moveInterval = 0; // �ڵ��� �������� �ϴ� ��
    private float moveNum = 0; // ������ Ƚ��

    [SerializeField]
    private GameObject waveBar;
    private Slider waveSlider;

    void Start()
    {
        waveSlider = waveBar.GetComponent<Slider>();

        SetTime();
        StartCoroutine(Call2f());

    }

    void Update()
    {

    }

    IEnumerator Call2f()
	{
        while (true)
        {
            CheckTime();
            Debug.Log(Time.time);
            yield return new WaitForSeconds(2.0f);

            if(waveSlider.value >= 1)
			{
                break;
			}
        }
    }

    private void SetTime()
	{
        startTime = Time.time;
        currentTime = Time.time;
	}

    private void CheckTime()
	{
        currentTime = Time.time - startTime;
        if(!firstAppear && currentTime >= firstAppearTime)
		{
            firstAppear = true;
            waveBar.SetActive(true);
            moveNum = (firstWaveTime - firstAppearTime) / timeCheckInterval;
            moveInterval = 0.5f / moveNum;
        }
        else if(firstAppear && !firstWave && currentTime < firstWaveTime)
		{
            HandleMove();
        }
        else if(firstAppear && !firstWave && currentTime >= firstWaveTime)
		{
            firstWave = true;
            moveNum = (secondWaveTime - firstWaveTime) / timeCheckInterval;
            moveInterval = 0.5f / moveNum;
        }
		else
		{
            HandleMove();
		}
	}

    private void HandleMove()
	{
        waveSlider.value += moveInterval;
	}
}
