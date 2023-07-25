using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ���̺� UI ���� ��ũ��Ʈ
public class MonsterWaveTimer : MonoBehaviour
{
    private int firstAppearTime = 5; // ù ���Ͱ� ��Ÿ���� �������� �ð�
    public int FirstAppearTime { set { firstAppearTime = value; } get { return firstAppearTime; } }
    private int timeCheckInterval = 2; // �ð� üũ ����

    private int firstWaveStart = 0; // ù��° ���̺� ���� �ð�
    public int FirstWaveStart { set { firstWaveStart = value; } get { return firstWaveStart; } }
    private int secondWaveStart = 0; // �ι�° ���̺� ���� �ð�
    public int SecondWaveStart { set { secondWaveStart = value; } get { return secondWaveStart; } }

    private int firstWaveTime = 120; // ù��° ���̺� = 2��(�����̵� �� �߰� ����)
    public int FirstwaveTime { set { firstWaveTime = value; } get { return firstWaveTime; } }
    private int secondWaveTime = 240; // �ι�° ���̺� = 4��(�����̵� �� ��)(firstwaveTime�� 2�迩����)
    public int SecondWaveTime { set { secondWaveTime = value; } get { return secondWaveTime; } }
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
