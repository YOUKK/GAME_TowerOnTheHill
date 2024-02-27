using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Resource�� �����̱⸸.
// text ui�� �߰� ������� �� �ٸ� Ŭ������ �ϵ���.
// text ui�� �߰� ������� ����� Ŭ������ ���� ��������.
// ���� 1 : Menu Canvas �� �ϳ��� Ŭ������ �ڱ� �ڽ� ������Ʈ���� �����ϵ��� �ϴ� �� ���?
// �ظ� ui Ȱ��ȭ, coin text ui Ȱ��ȭ�� �ϳ��� Ŭ�������� �����ϵ��� ����.
// �׷��� menu canvas���� Ŭ������ ������� ���ҽ� ���� ��, menu canvas�� ã�ƾ� �ϴµ�,
// �̰��� �� ��ȿ�������� ������ ������ ��.
// ���� ���� Manager�� UIManageró�� staticŬ�������� menu canvas�� ������ �� �ֵ��� �Ѵٸ�?
// ���ҽ��� Ŭ������ ���� ��� - ���ҽ� UI�� �̵��ϰ� �����
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
        //��Ŭ�� ���� ��
        if(Input.GetMouseButtonDown(0))
        {
            //��Ŭ���� ����Ʈ�� ��ġ�� ���� ��ǥ�� ��ȯ
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

    // ���ҽ�Gem�� Ŭ���ϸ� ���ҽ�UI�� �̵��ϴ� ���
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