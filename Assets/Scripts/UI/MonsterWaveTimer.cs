using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ���̺� UI ���� ��ũ��Ʈ
public class MonsterWaveTimer : MonoBehaviour
{
    private int firstAppearTime = 5; // ù ���Ͱ� ��Ÿ���� �������� �ð�
    private int timeCheckInterval = 2; // �ð� üũ ����
    private int firstWaveTime = 120; // ù��° ���̺� = 2��
    private int secondWaveTime = 240; // �ι�° ���̺� = 4��
    private bool firstAppear = false;
    private bool firstWave = false;

    //private float startTime; // ���� ���۽ð�
    //private float currentTime; // ���� �ð�

    private float moveInterval = 0; // �ڵ��� �������� �ϴ� ��
    private int moveNum = 0; // ������ Ƚ��

    [SerializeField]
    private GameObject waveBar;
    private Slider waveSlider;

    void Start()
    {
        waveSlider = waveBar.GetComponent<Slider>();

        //SetTime();
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
			//Debug.Log(Time.time);
			yield return new WaitForSeconds(2.0f);

			if (waveSlider.value >= 1)
			{
				break;
			}
		}
	}

	//   private void SetTime()
	//{
	//       startTime = Time.time;
	//       currentTime = Time.time;
	//}

	private void CheckTime()
	{
        if(!firstAppear && Managers.TimeM.Sec >= firstAppearTime)
		{
            firstAppear = true;
            waveBar.SetActive(true);
            moveNum = (firstWaveTime - firstAppearTime) / timeCheckInterval;
            moveInterval = 0.5f / moveNum;
        }
        else if(firstAppear && !firstWave && Managers.TimeM.Sec < firstWaveTime)
		{
            HandleMove();
        }
        else if(firstAppear && !firstWave && Managers.TimeM.Sec >= firstWaveTime)
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
