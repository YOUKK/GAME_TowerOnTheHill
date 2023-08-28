using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePopup : ShopBase
{
    struct UpgradeUIInfo
    {
        public Button upgradeButton;
        public Slider slider;
        public TextMeshProUGUI costText;
        public TextMeshProUGUI increaseValueText;

        public UpgradeUIInfo(Button ub, Slider s, TextMeshProUGUI ct, TextMeshProUGUI ivt)
        {
            upgradeButton = ub;
            slider = s;
            costText = ct;
            increaseValueText = ivt;
        }
    }

    const int MAX_LEVEL = 4;
    readonly int[] trainingCost = { 100, 200, 400, 800 };

    [SerializeField]
    Button closeButton;

    [SerializeField]
    GameObject[] characterUpgradeUI;

    Dictionary<string, UpgradeData> characterDic;
    Dictionary<string, UpgradeUIInfo> upgradeUIDic = new Dictionary<string, UpgradeUIInfo>();

    // ĳ���� ���� ���� UI ��� ã��, 
    void Start()
    {
        characterDic = DataManager.GetUpgradeDataDic();

        // ĳ���� ������ ���� ���׷��̵� UI �������� �ҷ��� ĳ���� �̸��� ���� ��ųʸ��� ����.
        foreach (var item in characterUpgradeUI)
        {
            string chName = FindChild<TextMeshProUGUI>(item, "Text_Name", true).text;
            Button upgradeButton = FindChild<Button>(item, "Button_Upgrade");
            Slider slider = FindChild<Slider>(item, "Slider", true);
            TextMeshProUGUI costText = FindChild<TextMeshProUGUI>(item, "Text_Cost", true);

            int currLevel = characterDic[chName].currentLevel;
            upgradeButton.onClick.AddListener(() => UpgradeCharacter(chName));
            slider.value = characterDic[chName].currentLevel / 5.0f;
            if (characterDic[chName].currentLevel < trainingCost.Length)
            {
                costText.text = trainingCost[characterDic[chName].currentLevel].ToString();
            }
            else
            {
                costText.text = "MAX";
            }
            TextMeshProUGUI increaseText = FindChild<TextMeshProUGUI>(item, "IncreaseValue", true);
            //increaseText.text = $"({characterDic[chName].statIncrease[newLevel - 1]} + {characterDic[chName].statIncrease[newLevel]})";

            UpgradeUIInfo upgradeInfoUI = new UpgradeUIInfo(upgradeButton, slider, costText, increaseText);
            upgradeUIDic.Add(chName, upgradeInfoUI);
        }

        
    }

    void UpgradeCharacter(string name)
    {
        int newLevel = characterDic[name].currentLevel + 1;
        if (newLevel > MAX_LEVEL) return;


        
        // ���׷��̵� �� ������Ʈ �Ǵ� UI��
        upgradeUIDic[name].slider.value = newLevel / 5.0f;
        if (newLevel < trainingCost.Length)
            upgradeUIDic[name].costText.text = trainingCost[newLevel].ToString();
        else upgradeUIDic[name].costText.text = "MAX";
        upgradeUIDic[name].increaseValueText.text = 
            $"({characterDic[name].statIncrease[newLevel - 1]} + {characterDic[name].statIncrease[newLevel]})";
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
