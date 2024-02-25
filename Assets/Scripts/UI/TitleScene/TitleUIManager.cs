using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleUIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button bookButton;
    [SerializeField] private Button closeBookButton;

    [Header("Texts")]
    [SerializeField] private TMP_Text coinText;

    [Header("Book Popup")]
    [SerializeField] private GameObject bookPopup;
    [SerializeField] private Image targetImage;
    [SerializeField] private TMP_Text targetName;
    [SerializeField] private TMP_Text targetDescription;

    [Header("Figure Description")]
    [SerializeField] private FigureDescription[] characterDescriptions;
    [SerializeField] private FigureDescription[] monsterDescriptions;

    // ScriptableObject명, ScriptableObject 데이터 쌍의 딕셔너리 정보 저장
    private Dictionary<string, FigureDescription> figureDic = new Dictionary<string, FigureDescription>();

    void Start()
    {
        // coinText.text = PlayerPrefs.GetInt("coin").ToString();
        coinText.text = GameManager.GetInstance.GetPlayerData(PlayerDataKind.Coin).ToString();
        // 버튼에 리스너 함수 추가
        playButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("Title", "LevelSelectScene"));
        tutorialButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("Title", "TutorialScene"));
        shopButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("Title", "Shop"));
        bookButton.onClick.AddListener(() => ActiveBookPopup(true));
        closeBookButton.onClick.AddListener(() => ActiveBookPopup(false));

        // 캐릭터 설명 정보를 딕셔너리에 저장
        foreach(var element in characterDescriptions)
        {
            figureDic.Add(element.name, element);
        }
        // 몬스터 설명 정보를 딕셔너리에 저장
        foreach(var element in monsterDescriptions)
        {
            figureDic.Add(element.name, element);
        }

        ChangeBookPopup("Normal_Shooter");
    }

    private void SetFigureResolution(string figure)
    {
        if(figure == "Aerial_Monster")
        {
            targetImage.rectTransform.sizeDelta = new Vector2(144.0f, 91.2f);
        }
        else if(figure == "Bomber")
        {
            targetImage.rectTransform.sizeDelta = new Vector2(90.0f, 108.0f);
        }
        else if(figure == "Fairy")
        {
            targetImage.rectTransform.sizeDelta = new Vector2(133.0f, 112.0f);
        }
        // 위의 세 몬스터/캐릭터가 아닌 경우
        else
        {
            targetImage.rectTransform.sizeDelta = new Vector2(150.0f, 150.0f);
        }
    }

    private void ActiveBookPopup(bool flag)
    {
        if(bookPopup != null)
        {
            bookPopup.SetActive(flag);
            SoundManager.Instance.PlayEffect("Button1");
        }
    }

    // 클릭한 캐릭터/몬스터의 정보를 표시할 팝업 창 갱신
    public void ChangeBookPopup(string key)
    {
        if(figureDic.ContainsKey(key))
        {
            targetImage.sprite = figureDic[key].figureSprite;
            targetName.text = figureDic[key].figureName;
            targetDescription.text = figureDic[key].figureDescription;

            SetFigureResolution(key);
        }
    }

    public void ExitGame()
    {
        SoundManager.Instance.PlayEffect("Button1");
        Application.Quit();
    }
}
