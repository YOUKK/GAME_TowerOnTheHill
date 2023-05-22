using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �÷��̾� ���� ���� ����� Ÿ�̸�
public class TimeManager
{
    private bool On = false; // true�� Ÿ�̸� ����
    private float deltatime = 0f; // �ð� ���
    private int sec = 0; // ���� Ÿ���� ��
    public int Sec { get { return sec; } }

    public void OnUpdate()
    {
        if (On)
        {
            deltatime += Time.deltaTime;
            sec = Mathf.FloorToInt(deltatime);
        }
    }

    public void InitTimer() // �� ���� �÷��� �� ������ ������ ȣ��
	{
        deltatime = 0f;
        sec = 0;
	}

    public void StartTimer() // Ÿ�̸� ����
	{
        On = true;
	}

    public void StopTimer() // Ÿ�̸� ����(�Ͻ������� ���� ���)
	{
        On = false;
	}
}
