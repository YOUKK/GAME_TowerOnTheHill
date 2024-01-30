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
    [SerializeField] private Sprite targetImage;
    [SerializeField] private TMP_Text targetName;
    [SerializeField] private TMP_Text targetDescription;

    //private Dictionary<string, >

    void Start()
    {
        coinText.text = PlayerPrefs.GetInt("coin").ToString();
        // 버튼에 리스너 함수 추가
        playButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("Title", "LevelSelectScene"));
        tutorialButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("Title", "TutorialScene"));
        shopButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("Title", "Shop"));
        bookButton.onClick.AddListener(() => ActiveBookPopup(true));
        closeBookButton.onClick.AddListener(() => ActiveBookPopup(false));
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
    public void ChangeBookPopup(string targetName)
    {

    }

    public void ExitGame()
    {
        SoundManager.Instance.PlayEffect("Button1");
        Application.Quit();
    }
}
