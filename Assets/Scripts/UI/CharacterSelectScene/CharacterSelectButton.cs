using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectButton : MonoBehaviour
{
    private bool isSelect = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void MoveCanvas()
	{
        if (!isSelect) // �κ��丮 ĵ���� -> ĳ���� ���� ĵ����
        {
            transform.SetParent(SelectedCharacter.characterFrames[0].transform);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
            StartCoroutine(Move());

            isSelect = true;
        }
        else // �κ��丮 ĵ���� -> ĳ���� ���� ĵ����
        {
            isSelect = false;
		}
	}

	IEnumerator Move()
	{
		Vector2 des = new Vector2(0, 0);
		RectTransform rectT = gameObject.GetComponent<RectTransform>();

		while(Vector2.Distance(rectT.anchoredPosition, des) > 0.1f)
		{
			rectT.anchoredPosition = Vector2.Lerp(rectT.anchoredPosition, des, 0.05f);
			yield return null;
		}

		rectT.anchoredPosition = des;
	}
}
