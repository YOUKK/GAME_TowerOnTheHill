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
        public TextMeshProUGUI increaseText;

        public UpgradeUIInfo(Button ub, Slider s, TextMeshProUGUI ct, TextMeshProUGUI ivt)
        {
            upgradeButton = ub;
            slider = s;
            costText = ct;
            increaseText = ivt;
        }
    }

    const int MAX_LEVEL = 4;
    readonly int[] trainingCost = { 100, 200, 400, 800 };

    [SerializeField]
    Button closeButton;
    [SerializeField]
    TextMeshProUGUI currentCoin;
    [SerializeField]
    GameObject[] characterUpgradeUI;

    Dictionary<string, UpgradeData> characterDic;
    Dictionary<string, UpgradeUIInfo> upgradeUIDic = new Dictionary<string, UpgradeUIInfo>();

    // 캐릭터 업글 관련 UI 모두 찾기, 
    void Start()
    {
        characterDic = DataManager.GetUpgradeDataDic();
        currentCoin.text = PlayerPrefs.GetInt("coin").ToString();

        // 캐릭터 각각에 대한 업그레이드 UI 정보들을 불러와 캐릭터 이름에 따라 딕셔너리에 저장.
        foreach (var item in characterUpgradeUI)
        {
            string chName = FindChild<TextMeshProUGUI>(item, "Text_Name", true).text;
            Button upgradeButton = FindChild<Button>(item, "Button_Upgrade");
            Slider slider = FindChild<Slider>(item, "Slider", true);
            TextMeshProUGUI costText = FindChild<TextMeshProUGUI>(item, "Text_Cost", true);
            TextMeshProUGUI increaseText = FindChild<TextMeshProUGUI>(item, "IncreaseValue", true);


            // UI initalization
            int currentLevel = characterDic[chName].currentLevel;

            upgradeButton.onClick.AddListener(() => UpgradeCharacter(chName));

            slider.value = (currentLevel + 1) / 5.0f;
            if (currentLevel < MAX_LEVEL)
            {
                if (PlayerPrefs.GetInt("coin") < trainingCost[currentLevel])
                    upgradeButton.interactable = false;
                else upgradeButton.interactable = true;

                costText.text = trainingCost[currentLevel].ToString();
                increaseText.text = $"({characterDic[chName].statIncrease[currentLevel]} -> {characterDic[chName].statIncrease[currentLevel + 1]})";
            }
            else
            {
                upgradeButton.interactable = false;
                increaseText.text = "(MAX)";
                costText.text = "---";
            }

            UpgradeUIInfo upgradeInfoUI = new UpgradeUIInfo(upgradeButton, slider, costText, increaseText);
            upgradeUIDic.Add(chName, upgradeInfoUI);
        }
    }

    void UpgradeCharacter(string name)
    {
        if (characterDic[name].currentLevel < MAX_LEVEL)
        {
            int cost = PlayerPrefs.GetInt("coin") - trainingCost[characterDic[name].currentLevel];
            PlayerPrefs.SetInt("coin", cost);
        }

        int newLevel = characterDic[name].currentLevel + 1;
        characterDic[name].currentLevel = newLevel;

        // UI update
        upgradeUIDic[name].slider.value = (newLevel + 1) / 5.0f;

        if (newLevel < MAX_LEVEL)
        {
            upgradeUIDic[name].costText.text = trainingCost[newLevel].ToString();
            upgradeUIDic[name].increaseText.text = $"({characterDic[name].statIncrease[newLevel]} -> {characterDic[name].statIncrease[newLevel + 1]})";
        }
        else
        {
            upgradeUIDic[name].upgradeButton.interactable = false;
            upgradeUIDic[name].increaseText.text = "(MAX)";
            upgradeUIDic[name].costText.text = "---";
        }

        currentCoin.text = PlayerPrefs.GetInt("coin").ToString();
        UpdateButtonActive();

        DataManager.SaveCharacterUpgradeData();
    }

    void UpdateButtonActive()
    {
        foreach(KeyValuePair<string, UpgradeUIInfo> item in upgradeUIDic)
        {
            if (characterDic[item.Key].currentLevel < MAX_LEVEL)
            {
                if (PlayerPrefs.GetInt("coin") < trainingCost[characterDic[item.Key].currentLevel])
                    item.Value.upgradeButton.interactable = false;
                else item.Value.upgradeButton.interactable = true;
            }
            else item.Value.upgradeButton.interactable = false;
        }
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
