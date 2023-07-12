using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ҽ��� Ŭ������ ���� ��� - ���ҽ� UI�� �̵��ϰ� �����
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
        //��Ŭ�� ���� ��
        if(Input.GetMouseButtonDown(0))
        {
            //��Ŭ���� ����Ʈ�� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                       Input.mousePosition.y,
                                                                       -Input.mousePosition.z));
            if(Vector2.Distance(transform.position, point) < 0.5f)
            {
                isClick = true;

                resourceUI.GetResource(50); // resource�� ���� �ϴ� 50�̶�� ������

                StartCoroutine(MovetoUI());
            }
        }
    }

    // ���ҽ�Gem�� Ŭ���ϸ� ���ҽ�UI�� �̵��ϴ� ���
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