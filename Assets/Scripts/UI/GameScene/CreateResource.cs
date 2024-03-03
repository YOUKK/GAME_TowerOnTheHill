using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 랜덤하게 리소스가 만들어지는 기능
public class CreateResource : MonoBehaviour
{
    private GameObject resource;
    private Vector3 leftTopPos;
    private Vector3 rightTopPos;

	private void OnEnable()
	{
        GameManager.GetInstance.finishProcess += StopCreate;
    }

	void Start()
    {
        resource = Resources.Load<GameObject>("Prefabs/Projectile/FallingGem");
        leftTopPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 10f)); // 왼쪽 위 화면의 좌표
        rightTopPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 10f)); // 오른쪽 위 화면의 좌표

        StartCoroutine("Create");
    }

	private void OnDisable()
	{
        GameManager.GetInstance.finishProcess -= StopCreate;
	}


	IEnumerator Create()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);

            Vector3 createPos = new Vector3(Random.Range(leftTopPos.x + 5f, rightTopPos.x - 1.5f), leftTopPos.y + 1, 0); // startPos와 endPos 사이의 랜덤 좌표

            Instantiate(resource, createPos, Quaternion.identity);

            yield return new WaitForSeconds(14f);
        }
    }

    private void StopCreate()
	{
        StopCoroutine("Create");
	}
}
