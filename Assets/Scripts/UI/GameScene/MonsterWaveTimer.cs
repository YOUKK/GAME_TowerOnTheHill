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

    [SerializeField]
    private int firstWaveStart = 0; // ù��° ���̺� ���� �ð�
    [SerializeField]
    private int secondWaveStart = 0; // �ι�° ���̺� ���� �ð�

    [SerializeField]
    private int firstWaveTime = 120; // ù��° ���̺� = 2��(�����̵� �� �߰� ����)
    public int FirstwaveTime { set { firstWaveTime = value; } get { return firstWaveTime; } }
    [SerializeField]
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
        firstWaveTime = (int)DataManager.GetData.FirstWaveTime;
        secondWaveTime = (int)DataManager.GetData.SecondWaveTime;
        firstWaveStart = firstWaveTime - 5;
        secondWaveStart = secondWaveTime - 5;

        //SetTime();
        StartCoroutine(Call2f());
    }

    void Update()
    {
        if (!firstPopUp)
        {
            if (GamePlayManagers.TimeM.Sec > firstWaveStart)
            {
                firstPopUp = true;
                //Debug.Log("ù��° ���̺� ����! " + GamePlayManagers.TimeM.Sec);
                //Debug.Log("firstwavetime" + firstWaveTime);
                //Debug.Log("firstwavestart " + firstWaveStart);
                StartCoroutine(MonsterWaveTextPopUp());
            }
        }

		if (!secondPopUp)
		{
            if(GamePlayManagers.TimeM.Sec > secondWaveStart)
			{
                secondPopUp = true;
                //Debug.Log("�ι�° ���̺� ����!");
                StartCoroutine(MonsterWaveTextPopUp());
            }
		}
    }

    IEnumerator MonsterWaveTextPopUp()
	{
        monsterWaveText.SetActive(true);
        SoundManager.Instance.PlayEffect("MonstersAreComming");

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
        if(!firstAppear && GamePlayManagers.TimeM.Sec >= firstAppearTime)
		{
            firstAppear = true;
            waveBar.SetActive(true);
            moveNum = (firstWaveTime - firstAppearTime) / timeCheckInterval;
            moveInterval = 0.5f / moveNum;
        }
        else if(firstAppear && !firstWave && GamePlayManagers.TimeM.Sec < firstWaveTime)
		{
            HandleMove();
        }
        else if(firstAppear && !firstWave && GamePlayManagers.TimeM.Sec >= firstWaveTime)
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
