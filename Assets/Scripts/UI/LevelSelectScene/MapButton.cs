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
    private GameObject[] mapButton = new GameObject[3];
    [SerializeField]
    private GameObject[] mapStagesButton = new GameObject[3];

    [SerializeField]
    private Button backButton;

    private Color selectColor = new Color(226 / 255f, 199 / 255f, 153 / 255f);
    private Color unselectColor = new Color(135 / 255f, 120 / 255f, 98 / 255f);

    private GameObject currentStageButton; // 현재 띄어져있는 스테이지 버튼

    void Start()
    {
        // 클리어한 스테이지가 가장 먼저 뜨도록 설정
        GamePlayManagers.Instance.LoadWinPhaseStageFromJson();
        int curPhase = GamePlayManagers.Instance.winPS.phase - 1;
        currentStageButton = mapStagesButton[curPhase];
        MapButtonEffect(curPhase);
        currentStageButton.GetComponent<Animation>().Play("GoDown");

        backButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("LevelSelectScene", "Title"));
    }

    // 맵 버튼 클릭시 버튼 색 효과 처리
    private void MapButtonEffect(int current)
	{
        for(int i = 0; i < 3; i++)
		{
            if(i == current)
			{
                mapButton[i].GetComponent<Image>().sprite = selectSprite;
                mapButton[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = selectColor;
            }
			else
			{
                mapButton[i].GetComponent<Image>().sprite = unselectSprite;
                mapButton[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = unselectColor;
            }
		}
	}

    // 맵 버튼 클릭에 따른 애니메이션 처리
    private void Animation(int current)
	{
        if(currentStageButton != mapStagesButton[current])
		{
            currentStageButton.GetComponent<Animation>().Play("GoUp");
            currentStageButton = mapStagesButton[current];
            currentStageButton.GetComponent<Animation>().Play("GoDown");
        }
	}

    // 맵 버튼이랑 연결된 함수
    public void ButtonClick()
	{
        SoundManager.Instance.PlayEffect("Button1");

        if(EventSystem.current.currentSelectedGameObject.name == "Map1Button") // Map1버튼 클릭
		{
            MapButtonEffect(0);
            Animation(0);
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "Map2Button") // Map2버튼 클릭
		{
            MapButtonEffect(1);
            Animation(1);
        }
		else // Map3버튼 클릭
		{
            MapButtonEffect(2);
            Animation(2);
        }
	}
}
