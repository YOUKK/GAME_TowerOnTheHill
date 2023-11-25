using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ���ҽ� ȹ��, ���� ���ҽ� �� UI�� �����ִ� �� ���� ��ũ��Ʈ
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

    // ���� �� �ڿ� ���
    public void EarnResource(int get) // get�� ���� �ڿ��� ��
	{
        resource += get;
        ChangeText();
    }

    // ĳ���� �����ϱ�
    public void UseResource(int use) // use�� ĳ������ �ڿ��Ҹ�
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
