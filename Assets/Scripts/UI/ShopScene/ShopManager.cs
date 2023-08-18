using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : ShopBase
{
    ShopData shopData;

    [SerializeField]
    TextMeshProUGUI currentCoin;
    [SerializeField]
    GameObject[] buttons;

    enum Buttons { HammerButton, SeatButton, SlotButton, TrainingButton }
    enum Texts { PointText, ScoreText }
    enum Images { ItemIcon, }
    enum GameObjects { TestObject }

    const int hammerCost = 100;
    const int seatExpansionCost = 300;
    readonly int[] slotExpansionCost = { 100, 200, 300, 400 };
    readonly int[] trainingCost = { 100, 200, 400, 800 };
    
    void Start()
    {
        shopData = DataManager.GetShopData();

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.HammerButton).GetComponentInChildren<TextMeshProUGUI>().text = 
            hammerCost.ToString();
        GetButton((int)Buttons.SeatButton).GetComponentInChildren<TextMeshProUGUI>().text = 
            seatExpansionCost.ToString();
        GetButton((int)Buttons.SlotButton).GetComponentInChildren<TextMeshProUGUI>().text = 
            slotExpansionCost[shopData.slotLevel].ToString();


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
