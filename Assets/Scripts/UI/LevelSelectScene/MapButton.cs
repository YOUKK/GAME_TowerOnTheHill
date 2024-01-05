using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


// MapButton 클릭에 따른 기능 구현
public class MapButton : MonoBehaviour
{
    [SerializeField]
    private Sprite selectSprite;
    [SerializeField]
    private Sprite unselectSprite;

    [SerializeField]
    private GameObject mapButton1;
    [SerializeField]
    private GameObject mapButton2;
    [SerializeField]
    private GameObject mapButton3;

    [SerializeField]
    private GameObject map1Stage;
    [SerializeField]
    private GameObject map2Stage;
    [SerializeField]
    private GameObject map3Stage;
    [SerializeField]
    private Button backButton;

    private Color selectColor = new Color(226 / 255f, 199 / 255f, 153 / 255f);
    private Color unselectColor = new Color(135 / 255f, 120 / 255f, 98 / 255f);

    private GameObject currentStageButton; // 현재 띄어져있는 스테이지 버튼

    private List<string> animList = new List<string>();

    void Start()
    {
        currentStageButton = map1Stage;
        currentStageButton.GetComponent<Animation>().Play("GoDown");
        backButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("LevelSelectScene", "Title"));
    }

    void Update()
    {
        
    }

    public void ButtonClick()
	{
        if(EventSystem.current.currentSelectedGameObject.name == "Map1Button") // Map1버튼 클릭
		{
            mapButton1.GetComponent<Image>().sprite = selectSprite;
            mapButton2.GetComponent<Image>().sprite = unselectSprite;
            mapButton3.GetComponent<Image>().sprite = unselectSprite;

            mapButton1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = selectColor;
            mapButton2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = unselectColor;
            mapButton3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = unselectColor;

            if (currentStageButton != map1Stage)
            {
                currentStageButton.GetComponent<Animation>().Play("GoUp");
                currentStageButton = map1Stage;
                currentStageButton.GetComponent<Animation>().Play("GoDown");
            }
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "Map2Button") // Map2버튼 클릭
		{
            mapButton1.GetComponent<Image>().sprite = unselectSprite;
            mapButton2.GetComponent<Image>().sprite = selectSprite;
            mapButton3.GetComponent<Image>().sprite = unselectSprite;

            mapButton1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = unselectColor;
            mapButton2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = selectColor;
            mapButton3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = unselectColor;

            if (currentStageButton != map2Stage)
            {
                currentStageButton.GetComponent<Animation>().Play("GoUp");
                currentStageButton = map2Stage;
                currentStageButton.GetComponent<Animation>().Play("GoDown");
            }
        }
		else // Map3버튼 클릭
		{
            mapButton1.GetComponent<Image>().sprite = unselectSprite;
            mapButton2.GetComponent<Image>().sprite = unselectSprite;
            mapButton3.GetComponent<Image>().sprite = selectSprite;

            mapButton1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = unselectColor;
            mapButton2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = unselectColor;
            mapButton3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = selectColor;

            if (currentStageButton != map3Stage)
            {
                currentStageButton.GetComponent<Animation>().Play("GoUp");
                currentStageButton = map3Stage;
                currentStageButton.GetComponent<Animation>().Play("GoDown");
            }
        }
	}
}
