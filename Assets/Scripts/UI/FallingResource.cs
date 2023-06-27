using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingResource : MonoBehaviour
{
    private Vector3 leftBottomPos;
    float time = 0f;

    void Start()
    {
        leftBottomPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 10f)); // 왼쪽 아래 화면의 좌표
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position.y > leftBottomPos.y + 0.5f) // 리소스가 아래 화면까지 오지 않은 경우
            transform.Translate(Vector3.down * Time.deltaTime); // 내려가기
        else // 리소스가 화면 아래까지 내려오면
        {
            //Debug.Log(time);
            time += Time.deltaTime;
            if (time > 8f) // 7초 후에 destroy
            {
                Destroy(gameObject);
                //time = 0f;
            }
        }
    }
}
