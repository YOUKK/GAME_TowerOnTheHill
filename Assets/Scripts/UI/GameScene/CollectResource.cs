using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 리소스 획득, 사용과 리소스 수 UI로 보여주는 거 관련 스크립트
public class CollectResource : MonoBehaviour
{
    private int resource = 0;
    public int GetResource() { return resource; }

    [SerializeField]
    //private Text resourceText;
    private TMP_Text resourceText;
    [SerializeField]
    private GameObject coinBox;
    [SerializeField]
    private TMP_Text coinText;
    private float coinBoxActiveTimer = 1.5f;
    private bool isCoinItemPressed = false;

    void Start()
    {
        resource = 0;
        resourceText.text = "0";
    }

    void Update()
    {
        if(isCoinItemPressed)
        {
            CloseCoinBox();
        }
    }

    // 게임 중 자원 얻기
    public void EarnResource(int get) // get은 얻은 자원의 양
	{
        resource += get;
        ChangeText();
    }

    // 캐릭터 구매하기
    public void UseResource(int use) // use는 캐릭터의 자원소모량
	{
        resource -= use;
        ChangeText();
    }

    private void ChangeText()
	{
        resourceText.text = resource.ToString();
	}

    private void CloseCoinBox()
    {
        coinBoxActiveTimer -= Time.deltaTime;
        if (coinBoxActiveTimer < 0)
        {
            isCoinItemPressed = false;
            coinBoxActiveTimer = 1.5f;
            coinBox.SetActive(false);
        }
    }

    public void EarnCoin()
    {
        isCoinItemPressed = true;
        coinBoxActiveTimer = 1.5f;
        coinBox.SetActive(true);

        GamePlayManagers.Instance.AddCoin(50);
        coinText.text = PlayerPrefs.GetInt("coin").ToString();
    }
}
