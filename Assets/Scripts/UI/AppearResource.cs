using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 랜덤하게 리소스가 내려오는 기능
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
        leftTopPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 10f)); // 왼쪽 위 화면의 좌표
        rightTopPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 10f)); // 오른쪽 위 화면의 좌표
        leftBottomPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 10f)); // 왼쪽 아래 화면의 좌표
        rightBottomPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 10f)); // 오른쪽 아래 화면의 좌표

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

            Vector3 createPos = new Vector3(Random.Range(leftTopPos.x + 1, rightTopPos.x - 1), leftTopPos.y + 1, 0); // startPos와 endPos 사이의 랜덤 좌표

            instResource = Instantiate(resource, createPos, Quaternion.identity);
            isCreate = true;

            yield return new WaitForSeconds(15f);
        }
	}


	private void Move()
	{
        if (instResource.transform.position.y > leftBottomPos.y + 0.5f) // 리소스가 아래 화면까지 오지 않은 경우
            instResource.transform.Translate(Vector3.down * Time.deltaTime); // 내려가기
        else // 리소스가 화면 아래까지 내려오면
        {
            Debug.Log(time);
            time += Time.deltaTime;
            if (time > 4f) // 3초 후에 destroy
            {
                Destroy(instResource);
                time = 0f;
                isCreate = false;
            }
        }
	}
}
