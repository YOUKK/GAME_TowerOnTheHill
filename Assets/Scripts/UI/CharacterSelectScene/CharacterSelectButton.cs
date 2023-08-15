using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 캐릭터 버튼에 붙는 스크립트
// 버튼이 공통적으로 가지는 기능이 적힘

public class CharacterSelectButton : MonoBehaviour
{
    private SelectedCharacter selectedCharacter;
    public bool isSelect = false;
    [SerializeField]
    private CharacterButtonList character;
    private Transform motherObject; // 원래 부모 오브젝트
    Vector2 temp = new Vector2(0, 0); // 원래 anchordPosition

    void Start()
    {
        selectedCharacter = GameObject.Find("SelectedCanvas").GetComponent<SelectedCharacter>();
        motherObject = transform.parent;
    }

    void Update()
    {
        
    }

    // 버튼 클릭하면 인벤토리 캔버스 <-> 캐릭터 선택 캔버스 왔다갔다 하는 기능
    public void MoveCanvas()
	{
        if (!isSelect) // 인벤토리 캔버스 -> 캐릭터 선택 캔버스
        {
            transform.SetParent(selectedCharacter.GetNextTurn().transform);
            temp.x = gameObject.GetComponent<RectTransform>().anchoredPosition.x;
            temp.y = gameObject.GetComponent<RectTransform>().anchoredPosition.y;
            Debug.Log("Temp" + temp);
            StartCoroutine(MoveGo(Vector2.zero));

            selectedCharacter.Turn++;
            isSelect = true;
        }
        else // 인벤토리 캔버스 -> 캐릭터 선택 캔버스
        {
            //transform.SetParent(motherObject);
            StartCoroutine(MoveBack(temp));

            selectedCharacter.Turn--;
            isSelect = false;
		}
	}

	IEnumerator MoveGo(Vector2 des)
	{
        float time = 0f;

		RectTransform rectT = gameObject.GetComponent<RectTransform>();

        while(time < 0.5f)
		{
            rectT.anchoredPosition = Vector2.Lerp(rectT.anchoredPosition, des, time+=Time.deltaTime);
            yield return null;
        }

        rectT.anchoredPosition = des;
	}

    IEnumerator MoveBack(Vector2 des)
	{
        float time = 0f;

        RectTransform rectT = gameObject.GetComponent<RectTransform>();

        while(time < 0.5f)
		{
            rectT.anchoredPosition = Vector2.Lerp(rectT.anchoredPosition, des, time+=Time.deltaTime);
            yield return null;
        }

        //while (Vector2.Distance(rectT.anchoredPosition, des) > 0.1f)
        //{
        //    rectT.anchoredPosition = Vector2.Lerp(rectT.anchoredPosition, des, 0.1f);
        //    yield return null;
        //}

        rectT.anchoredPosition = des;

        transform.SetParent(motherObject);
    }
}
