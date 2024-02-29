using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CreateResource.cs에서 랜덤하게 만들어진 리소스가 내려오는 기능
public class FallingResource : MonoBehaviour
{
    private Vector3 leftBottomPos;
    float time = 0f;
    private Resource resource;

    void Start()
    {
        leftBottomPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 10f)); // 왼쪽 아래 화면의 좌표
        resource = GetComponent<Resource>();
    }

    void Update()
    {
        if (!resource.GetIsClick())
        {
            Move();
        }
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
                //gameObject.GetComponent<Resource>().MinusDelegate();
                Destroy(gameObject);
                //time = 0f;
            }
        }
    }
}
