using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartEndText : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private GameObject textOb;

    private Vector3 startPos; // 처음위치
    private Vector3 middlePos; // 화면 정가운데 좌표
    private Vector3 middleBotPos; // 화면 가운에 아래 좌표

    void Start()
    {
        startPos = textOb.transform.position;
        middlePos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
        middleBotPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, 10f));
    }

    void Update()
    {
        DownMove();
    }

	private void DownMove() // 아래로 움직이는 기능
	{
        if (textOb.transform.position.y - middlePos.y < 0.01f)
            textOb.transform.position = Vector3.Lerp(textOb.transform.position, middlePos, 0.05f);
        else
            textOb.transform.position = Vector3.Lerp(textOb.transform.position, middleBotPos, 0.05f);
	}
}
