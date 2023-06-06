using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private SunFlower mainCharacter;

    private CollectResource resourceUI;

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = transform.GetComponentInParent<SunFlower>();
        resourceUI = GameObject.Find("MenuCanvas").GetComponent<CollectResource>();
    }

    // Update is called once per frame
    void Update()
    {
        //좌클릭 했을 때
        if(Input.GetMouseButtonDown(0))
        {
            //좌클릭한 포인트의 위치를 월드 좌표로 변환
            Vector2 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                       Input.mousePosition.y,
                                                                       -Input.mousePosition.z));
            if(Vector2.Distance(transform.position, point) < 0.5f)
            {
                resourceUI.GetResource(50); // resource의 양을 일단 50이라고 설정함

                //전체 자원 추가 미구현
                gameObject.SetActive(false);
            }
        }
    }
}