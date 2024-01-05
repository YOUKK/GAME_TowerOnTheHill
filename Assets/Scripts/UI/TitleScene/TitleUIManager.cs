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

    void Start()
    {
        coinText.text = PlayerPrefs.GetInt("coin").ToString();
        playButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("Title", "LevelSelectScene"));
        tutorialButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("Title", "TutorialScene"));
        shopButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("Title", "Shop"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
