using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : ShopBase
{
    ShopData shopData;
    Dictionary<string, UpgradeData> characterDic;

    [SerializeField]
    GameObject characterUpgradePopup;
    [SerializeField]
    TextMeshProUGUI currentCoin;
    [SerializeField]
    GameObject[] buttons;

    private UpgradeData[] characterUpgradeDatas;

    enum Buttons { HammerButton, SeatButton, SlotButton, UpgradeButton }
    enum Texts { PointText, ScoreText }
    enum Images { ItemIcon, }
    enum GameObjects { TestObject }

    #region Costs
    const int hammerCost = 100;
    const int seatExpansionCost = 300;
    readonly int[] slotExpansionCost = { 100, 200, 300, 400 };
    readonly int[] trainingCost = { 100, 200, 400, 800 };
    #endregion

    void Start()
    {
        shopData = DataManager.GetShopData();

        characterDic = DataManager.GetUpgradeDataDic();
        UpgradeData currenCaracterData = characterDic["PeaShooter"];

        Debug.Log(currenCaracterData.chName + " and " + currenCaracterData.kind + " and " + 
            currenCaracterData.statIncrease[currenCaracterData.currentLevel]);

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.HammerButton).GetComponentInChildren<TextMeshProUGUI>().text = 
            hammerCost.ToString();
        GetButton((int)Buttons.SeatButton).GetComponentInChildren<TextMeshProUGUI>().text = 
            seatExpansionCost.ToString();
        GetButton((int)Buttons.SlotButton).GetComponentInChildren<TextMeshProUGUI>().text = 
            slotExpansionCost[shopData.slotLevel].ToString();

        characterUpgradePopup.SetActive(false);

        GetButton((int)Buttons.UpgradeButton).onClick.AddListener(ActivateUpgradePopup);

        if (PlayerPrefs.HasKey("coin"))
            currentCoin.text = PlayerPrefs.GetInt("coin").ToString();
        else Debug.LogError("No Coin Data!");
    }

    public void ActivateUpgradePopup()
    {
        characterUpgradePopup.SetActive(true);
    }

    public void SaveShopInfo(string itemName)
    {
        ShopData shopData = new ShopData();

        if (itemName == "Hammer")
        {
            shopData.hasHammer = true;
        }
        else if (itemName == "SeatExpansion")
        {
            shopData.hasSeatExpansion = true;
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
