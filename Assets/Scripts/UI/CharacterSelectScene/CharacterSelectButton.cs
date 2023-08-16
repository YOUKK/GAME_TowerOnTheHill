using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ĳ���� ��ư�� �ٴ� ��ũ��Ʈ
// ��ư�� ���������� ������ ����� ����

public class CharacterSelectButton : MonoBehaviour
{
    private SelectedCharacter selectedCharacter;
    public bool isSelect = false;
    [SerializeField]
    private CharacterButtonList character;
    private Transform motherObject; // ���� �θ� ������Ʈ
    Vector2 temp = new Vector2(0, 0); // ���� anchordPosition

    void Start()
    {
        selectedCharacter = GameObject.Find("SelectedCanvas").GetComponent<SelectedCharacter>();
        motherObject = transform.parent;
    }

    void Update()
    {
        
    }

    // ��ư Ŭ���ϸ� �κ��丮 ĵ���� <-> ĳ���� ���� ĵ���� �Դٰ��� �ϴ� ���
    public void MoveCanvas()
	{
        if (!isSelect) // �κ��丮 ĵ���� -> ĳ���� ���� ĵ����
        {
            transform.SetParent(selectedCharacter.GetNextTurn().transform);
            temp.x = gameObject.GetComponent<RectTransform>().anchoredPosition.x;
            temp.y = gameObject.GetComponent<RectTransform>().anchoredPosition.y;
            Debug.Log("Temp" + temp);
            StartCoroutine(MoveGo(Vector2.zero));

            selectedCharacter.Turn++;
            isSelect = true;
        }
        else // �κ��丮 ĵ���� -> ĳ���� ���� ĵ����
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
