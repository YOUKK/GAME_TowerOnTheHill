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
        //��Ŭ�� ���� ��
        if(Input.GetMouseButtonDown(0))
        {
            //��Ŭ���� ����Ʈ�� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                       Input.mousePosition.y,
                                                                       -Input.mousePosition.z));
            if(Vector2.Distance(transform.position, point) < 0.5f)
            {
                resourceUI.GetResource(50); // resource�� ���� �ϴ� 50�̶�� ������

                //��ü �ڿ� �߰� �̱���
                gameObject.SetActive(false);
            }
        }
    }
}