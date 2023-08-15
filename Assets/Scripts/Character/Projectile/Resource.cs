using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 리소스를 클릭했을 때의 기능 - 리소스 UI로 이동하고 사라짐
public class Resource : MonoBehaviour
{
    private enum RecourceType { Gem, Coin }
    private RecourceType type;
    private CollectResource resourceUI;
    private GameObject iconGem;
    private GameObject coinBox;
    private Transform iconCoin;

    private bool isClick = false;
    public bool GetIsClick() { return isClick; }

    void Start()
    {
        if (transform.CompareTag("Gem")) type = RecourceType.Gem;
        else if (transform.CompareTag("Coin")) type = RecourceType.Coin;
        else Debug.LogError("Wrong Resource Name!");
        resourceUI = GameObject.Find("MenuCanvas").GetComponent<CollectResource>();
        iconGem = resourceUI.transform.GetChild(0).transform.GetChild(0).gameObject;
        coinBox = resourceUI.transform.GetChild(1).gameObject;
        iconCoin = coinBox.transform.GetChild(0);
    }

    void Update()
    {
        //좌클릭 했을 때
        if(Input.GetMouseButtonDown(0))
        {
            //좌클릭한 포인트의 위치를 월드 좌표로 변환
            Vector2 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                       Input.mousePosition.y,
                                                                       -Input.mousePosition.z));
            if(Vector2.Distance(transform.position, point) < 0.5f)
            {
                switch (type)
                {
                    case RecourceType.Gem:
                        {
                            isClick = true;
                            resourceUI.EarnResource(50); // resource의 양을 일단 50이라고 설정함
                            StartCoroutine(MovetoUI(iconGem.transform.position));
                            break;
                        }
                    case RecourceType.Coin:
                        {
                            isClick = true;
                            coinBox.SetActive(true);
                            int currentCoin = PlayerPrefs.GetInt("coin");
                            PlayerPrefs.SetInt("coin", currentCoin + 50);
                            TextMeshProUGUI textMeshPro = coinBox.GetComponentInChildren<TextMeshProUGUI>();
                            textMeshPro.text = (currentCoin + 50).ToString();

                            StartCoroutine(MovetoUI(iconCoin.position));
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }

    // 리소스Gem을 클릭하면 리소스UI로 이동하는 기능
    IEnumerator MovetoUI(Vector3 destination)
	{
		while (Vector2.Distance(transform.position, destination) > 0.1f)
		{
			transform.position = Vector2.Lerp(transform.position, destination, 0.05f);
			yield return null;
		}

		if (type == RecourceType.Coin)
        {
            yield return new WaitForSeconds(1);
            coinBox.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}