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

    // 함수 오버로딩하자. 각 아이템 버튼을 누르면 실행시킬 함수.
    public void SaveShopInfo(bool _buyHammer, bool _buySeatExpansion)
    {
        ShopData shopData = new ShopData();

        // shopData 초기화

        DataManager.SaveShopData(shopData);
    }

    public void ActivateHammer(GameObject hammerUI)
    {
        hammerUI.SetActive(true);
        // 코인 감소
    }
}
