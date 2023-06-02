using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ϰ� ���ҽ��� �������� ���
public class AppearResource : MonoBehaviour
{
    private GameObject resource;
    private GameObject instResource;
    private Vector3 leftTopPos;
    private Vector3 rightTopPos;
    private Vector3 leftBottomPos;
    private Vector3 rightBottomPos;

    float time = 0f;
    private bool isCreate = false;

    void Start()
    {
        resource = Resources.Load<GameObject>("Prefabs/Projectile/Resource");
        leftTopPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 10f)); // ���� �� ȭ���� ��ǥ
        rightTopPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 10f)); // ������ �� ȭ���� ��ǥ
        leftBottomPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 10f)); // ���� �Ʒ� ȭ���� ��ǥ
        rightBottomPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 10f)); // ������ �Ʒ� ȭ���� ��ǥ

        StartCoroutine(Create());
    }

    void Update()
    {
        if(isCreate)
            Move();
    }

	IEnumerator Create()
	{
        while (true)
        {
            yield return new WaitForSeconds(4f);

            Vector3 createPos = new Vector3(Random.Range(leftTopPos.x + 1, rightTopPos.x - 1), leftTopPos.y + 1, 0); // startPos�� endPos ������ ���� ��ǥ

            instResource = Instantiate(resource, createPos, Quaternion.identity);
            isCreate = true;

            yield return new WaitForSeconds(15f);
        }
	}


	private void Move()
	{
        if (instResource.transform.position.y > leftBottomPos.y + 0.5f) // ���ҽ��� �Ʒ� ȭ����� ���� ���� ���
            instResource.transform.Translate(Vector3.down * Time.deltaTime); // ��������
        else // ���ҽ��� ȭ�� �Ʒ����� ��������
        {
            Debug.Log(time);
            time += Time.deltaTime;
            if (time > 4f) // 3�� �Ŀ� destroy
            {
                Destroy(instResource);
                time = 0f;
                isCreate = false;
            }
        }
	}
}
