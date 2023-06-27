using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingResource : MonoBehaviour
{
    private Vector3 leftBottomPos;
    float time = 0f;

    void Start()
    {
        leftBottomPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 10f)); // ���� �Ʒ� ȭ���� ��ǥ
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position.y > leftBottomPos.y + 0.5f) // ���ҽ��� �Ʒ� ȭ����� ���� ���� ���
            transform.Translate(Vector3.down * Time.deltaTime); // ��������
        else // ���ҽ��� ȭ�� �Ʒ����� ��������
        {
            //Debug.Log(time);
            time += Time.deltaTime;
            if (time > 8f) // 7�� �Ŀ� destroy
            {
                Destroy(gameObject);
                //time = 0f;
            }
        }
    }
}
