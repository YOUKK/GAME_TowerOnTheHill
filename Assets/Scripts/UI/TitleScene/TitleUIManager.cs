using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button bookButton;
    [SerializeField] private Button closeBookButton;
    [SerializeField] private GameObject bookPopup;

    void Start()
    {
        coinText.text = PlayerPrefs.GetInt("coin").ToString();
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

    public void ExitGame()
    {
        SoundManager.Instance.PlayEffect("Button1");
        Application.Quit();
    }
}
