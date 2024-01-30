using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


// MapButton Ŭ���� ���� ��� ����
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

    private GameObject currentStageButton; // ���� ������ִ� �������� ��ư

    void Start()
    {
        // Ŭ������ ���������� ���� ���� �ߵ��� ����
        GamePlayManagers.Instance.LoadWinPhaseStageFromJson();
        int curPhase = GamePlayManagers.Instance.winPS.phase - 1;
        currentStageButton = mapStagesButton[curPhase];
        MapButtonEffect(curPhase);
        currentStageButton.GetComponent<Animation>().Play("GoDown");

        backButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("LevelSelectScene", "Title"));
    }

    // �� ��ư Ŭ���� ��ư �� ȿ�� ó��
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

    // �� ��ư Ŭ���� ���� �ִϸ��̼� ó��
    private void Animation(int current)
	{
        if(currentStageButton != mapStagesButton[current])
		{
            currentStageButton.GetComponent<Animation>().Play("GoUp");
            currentStageButton = mapStagesButton[current];
            currentStageButton.GetComponent<Animation>().Play("GoDown");
        }
	}

    // �� ��ư�̶� ����� �Լ�
    public void ButtonClick()
	{
        SoundManager.Instance.PlayEffect("Button1");

        if(EventSystem.current.currentSelectedGameObject.name == "Map1Button") // Map1��ư Ŭ��
		{
            MapButtonEffect(0);
            Animation(0);
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "Map2Button") // Map2��ư Ŭ��
		{
            MapButtonEffect(1);
            Animation(1);
        }
		else // Map3��ư Ŭ��
		{
            MapButtonEffect(2);
            Animation(2);
        }
	}
}
