using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 리소스를 클릭했을 때의 기능 - 리소스 UI로 이동하고 사라짐
public class Resource : MonoBehaviour
{
    private SunFlower mainCharacter;
    private float pAttackDuration = 0;

    private CollectResource resourceUI;
    private GameObject iconGem;

    private bool isClick = false;
    public bool GetIsClick() { return isClick; }

    void Start()
    {
        mainCharacter = transform.GetComponentInParent<SunFlower>();
        resourceUI = GameObject.Find("MenuCanvas").GetComponent<CollectResource>();
        iconGem = resourceUI.transform.GetChild(0).transform.GetChild(0).gameObject;
    }

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
                isClick = true;

                resourceUI.GetResource(50); // resource의 양을 일단 50이라고 설정함

                StartCoroutine(MovetoUI());
            }
        }
    }

    // 리소스Gem을 클릭하면 리소스UI로 이동하는 기능
    IEnumerator MovetoUI()
	{
		while (Vector2.Distance(transform.position, iconGem.transform.position) > 0.1f)
		{
			transform.position = Vector2.Lerp(transform.position, iconGem.transform.position, 0.05f);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}