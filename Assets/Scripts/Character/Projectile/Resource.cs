using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Resource는 움직이기만.
// text ui가 뜨고 사라지는 건 다른 클래스가 하도록.
// text ui가 뜨고 사라지게 만드는 클래스를 새로 생성하자.
// 생각 1 : Menu Canvas 가 하나의 클래스로 자기 자식 오브젝트들을 관리하도록 하는 건 어떤가?
// 해머 ui 활성화, coin text ui 활성화도 하나의 클래스에서 관리하도록 하자.
// 그렇게 menu canvas만의 클래스를 만들더라도 리소스 생성 시, menu canvas를 찾아야 하는데,
// 이것이 좀 비효율적이지 않을까 생각이 듬.
// 게임 씬의 Manager나 UIManager처럼 static클래스에서 menu canvas에 접근할 수 있도록 한다면?
// 리소스를 클릭했을 때의 기능 - 리소스 UI로 이동하고 사라짐
public class Resource : MonoBehaviour
{
    private enum ResourceType { Gem, Coin }
    private ResourceType type;
    private CollectResource resourceUI;
    private GameObject iconGem;
    private GameObject coinBox;
    private Transform iconCoin;

    private bool isClick = false;
    public bool GetIsClick() { return isClick; }

	void OnEnable()
	{
        GamePlayManagers.Instance.finishProcess += EndProcess;
    }

	void Start()
    {
        if (transform.CompareTag("Gem")) type = ResourceType.Gem;
        else if (transform.CompareTag("Coin")) type = ResourceType.Coin;
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
                    case ResourceType.Gem:
                        {
                            isClick = true;
                            resourceUI.EarnResource(50);
                            StartCoroutine(MovetoUI(iconGem.transform.position));
                            SoundManager.Instance.PlayEffect("GetResource");
                            break;
                        }
                    case ResourceType.Coin:
                        {
                            isClick = true;
                            resourceUI.EarnCoin();
                            StartCoroutine(MovetoUI(iconCoin.position));
                            SoundManager.Instance.PlayEffect("Coin");
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }

    // 리소스Gem을 클릭하면 리소스UI로 이동하는 기능
    IEnumerator MovetoUI(Vector2 destination)
	{
        while (Vector2.Distance(transform.position, destination) > 0.1f)
		{
			transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime * 7f);
			yield return null;
		}

        MinusDelegate();

        if (type == ResourceType.Gem)
			gameObject.SetActive(false);
		if (type == ResourceType.Coin)
			Destroy(gameObject);
    }

	private void EndProcess()
	{
        if(type == ResourceType.Gem)
		{
            gameObject.SetActive(false);
		}
        else
        {
            resourceUI.EarnCoin();
            StartCoroutine(MovetoUI(iconCoin.position));
        }
    }

    public void MinusDelegate()
	{
        GamePlayManagers.Instance.finishProcess -= EndProcess;
    }
}