using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePopup : ShopBase
{
    [SerializeField]
    Button closeButton;

    GameObject[] characterUpgradeUI;

    struct UpgradeInfoUI
    {
        Button upgradeButton;
        Slider slider;
        TextMeshProUGUI costText;
        TextMeshProUGUI increaseValueText;  
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
