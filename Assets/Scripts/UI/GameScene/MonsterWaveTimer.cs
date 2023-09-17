using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ���̺� UI ���� ��ũ��Ʈ
// menuCanavas�� �����Ǿ� ����
public class MonsterWaveTimer : MonoBehaviour
{
    private int firstAppearTime = 5; // ù ���Ͱ� ��Ÿ���� �������� �ð�
    public int FirstAppearTime { set { firstAppearTime = value; } get { return firstAppearTime; } }
    private int timeCheckInterval = 2; // �ð� üũ ����

    private int firstWaveStart = 0; // ù��° ���̺� ���� �ð�
    //public int FirstWaveStart { set { firstWaveStart = value; } get { return firstWaveStart; } }
    private int secondWaveStart = 0; // �ι�° ���̺� ���� �ð�
    //public int SecondWaveStart { set { secondWaveStart = value; } get { return secondWaveStart; } }

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
    private bool firstPopUp = false;
    private bool secondPopUp = false;

    [SerializeField]
    private GameObject waveBar;
    private Slider waveSlider;
    [SerializeField]
    private GameObject monsterWaveText;

    void Start()
    {
        waveSlider = waveBar.GetComponent<Slider>();
        firstWaveStart = firstWaveTime - 5;
        secondWaveStart = secondWaveTime - 5;

        //SetTime();
        StartCoroutine(Call2f());

    }

    void Update()
    {
        if (!firstPopUp)
        {
            if (Managers.TimeM.Sec > firstWaveStart)
            {
                firstPopUp = true;
                Debug.Log("ù��° ���̺� ����! " + Managers.TimeM.Sec);
                Debug.Log("firstwavetime" + firstWaveTime);
                Debug.Log("firstwavestart " + firstWaveStart);
                StartCoroutine(MonsterWaveTextPopUp());
            }
        }

		if (!secondPopUp)
		{
            if(Managers.TimeM.Sec > secondWaveStart)
			{
                secondPopUp = true;
                Debug.Log("�ι�° ���̺� ����!");
                StartCoroutine(MonsterWaveTextPopUp());
            }
		}
    }

    IEnumerator MonsterWaveTextPopUp()
	{
        monsterWaveText.SetActive(true);

        yield return new WaitForSeconds(2f);

        monsterWaveText.SetActive(false);
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
