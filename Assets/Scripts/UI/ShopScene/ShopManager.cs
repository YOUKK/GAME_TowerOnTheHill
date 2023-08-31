using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : ShopBase
{
    ShopData shopData;

    [SerializeField]
    GameObject characterUpgradePopup;
    [SerializeField]
    TextMeshProUGUI currentCoinText;
    [SerializeField]
    TextMeshProUGUI slotLevelText;

    enum Buttons { HammerButton, SeatButton, SlotButton, UpgradeButton }

    #region Costs
    const int hammerCost = 100;
    const int seatExpansionCost = 300;
    readonly int[] slotExpansionCost = { 100, 200, 300, 400 };
    #endregion

    void Start()
    {
        shopData = DataManager.GetData.GetShopData();

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.HammerButton).GetComponentInChildren<TextMeshProUGUI>().text = 
            hammerCost.ToString();
        GetButton((int)Buttons.SeatButton).GetComponentInChildren<TextMeshProUGUI>().text = 
            seatExpansionCost.ToString();
        GetButton((int)Buttons.SlotButton).GetComponentInChildren<TextMeshProUGUI>().text = 
            slotExpansionCost[shopData.slotLevel].ToString();
        slotLevelText.text = $"({shopData.slotLevel}/4)";

        characterUpgradePopup.SetActive(false);
        UpdateShopButtons();

        GetButton((int)Buttons.UpgradeButton).onClick.AddListener(ActivateUpgradePopup);

        if (PlayerPrefs.HasKey("coin"))
            currentCoinText.text = PlayerPrefs.GetInt("coin").ToString();
        else Debug.LogError("No Coin Data!");
    }

    private void UpdateShopButtons()
    {
        int currentCoin = PlayerPrefs.GetInt("coin");

        if(currentCoin < hammerCost || shopData.hasHammer == true)
        {
            GetButton((int)Buttons.HammerButton).interactable = false;
        }
        else GetButton((int)Buttons.HammerButton).interactable = true;

        if (currentCoin < seatExpansionCost || shopData.hasSeatExpansion == true)
        {
            GetButton((int)Buttons.SeatButton).interactable = false;
        }
        else GetButton((int)Buttons.SeatButton).interactable = true;

        if (currentCoin < slotExpansionCost[shopData.slotLevel] || shopData.slotLevel == slotExpansionCost.Length)
        {
            GetButton((int)Buttons.SlotButton).interactable = false;
        }
        else GetButton((int)Buttons.SlotButton).interactable = true;
    }
    
    private bool Buy(int cost)
    {
        int currentCoin = PlayerPrefs.GetInt("coin");
        if (currentCoin < hammerCost) return false;

        currentCoin -= cost;
        PlayerPrefs.SetInt("coin", currentCoin);
        currentCoinText.text = currentCoin.ToString();

        return true;
    }

    private void ActivateUpgradePopup()
    {
        characterUpgradePopup.SetActive(true);
    }

    public void BuyHammerItem()
    {
        if (Buy(hammerCost) == false) return;

        shopData.hasHammer = true;
        UpdateShopButtons();
    }

    public void BuySeatExpansionItem()
    {
        if (Buy(seatExpansionCost) == false) return;

        shopData.hasSeatExpansion = true;
        UpdateShopButtons();
    }

    public void BuySlotExpansionItem()
    {
        if (Buy(slotExpansionCost[shopData.slotLevel]) == false) return;

        shopData.slotLevel++;
        slotLevelText.text = $"{shopData.slotLevel}/4";

        UpdateShopButtons();
    }

    public void SaveShopInfo(string itemName)
    {
        ShopData shopData = new ShopData();

        DataManager.GetData.SaveShopData(shopData); 
    }
}
