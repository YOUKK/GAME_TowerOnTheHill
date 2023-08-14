using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI currentCoin;
    [SerializeField]
    GameObject[] buttons;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnEnable()
    {
        if (PlayerPrefs.HasKey("coin"))
            currentCoin.text = PlayerPrefs.GetInt("coin").ToString();
        else Debug.LogError("No Coin Data!");
    }

    // �Լ� �����ε�����. �� ������ ��ư�� ������ �����ų �Լ�.
    public void SaveShopInfo(bool _buyHammer, bool _buySeatExpansion)
    {
        ShopData shopData = new ShopData();

        // shopData �ʱ�ȭ

        DataManager.SaveShopData(shopData);
    }

    public void ActivateHammer(GameObject hammerUI)
    {
        hammerUI.SetActive(true);
        // ���� ����
    }
}
