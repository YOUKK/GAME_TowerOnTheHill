using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ϰ� ���ҽ��� ��������� ���
public class CreateResource : MonoBehaviour
{
    private GameObject resource;
    private Vector3 leftTopPos;
    private Vector3 rightTopPos;

    void Start()
    {
        GamePlayManagers.Instance.finishProcess += StopCreate;

        resource = Resources.Load<GameObject>("Prefabs/Projectile/FallingGem");
        leftTopPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 10f)); // ���� �� ȭ���� ��ǥ
        rightTopPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 10f)); // ������ �� ȭ���� ��ǥ

        StartCoroutine("Create");
    }

    void Update()
    {
        //if(isCreate)
            //Move();
    }

    IEnumerator Create()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);

            Vector3 createPos = new Vector3(Random.Range(leftTopPos.x + 5f, rightTopPos.x - 1.5f), leftTopPos.y + 1, 0); // startPos�� endPos ������ ���� ��ǥ

            Instantiate(resource, createPos, Quaternion.identity);

            yield return new WaitForSeconds(14f);
        }
    }

    private void StopCreate()
	{
        StopCoroutine("Create");
	}
}
