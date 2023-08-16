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

    public void OnEnable()
    {
        if (PlayerPrefs.HasKey("coin"))
            currentCoin.text = PlayerPrefs.GetInt("coin").ToString();
        else Debug.LogError("No Coin Data!");
    }

    public void SaveShopInfo(string itemName)
    {
        ShopData shopData = new ShopData();

        if (itemName == "Hammer")
        {
            shopData.buyHammer = true;
        }
        else if (itemName == "SeatExpansion")
        {
            shopData.buySeatExpansion = true;
        }
        else if (itemName == "SlotExpansion")
        {
            shopData.slotLevel++;
        }
        else if (itemName == "CharacterTraining")
        {
            Debug.Log("Character Training Button is On");
        }
        else return;



        DataManager.SaveShopData(shopData);
    }
}
