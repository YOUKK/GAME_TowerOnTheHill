using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;

    void Start()
    {
        coinText.text = PlayerPrefs.GetInt("coin").ToString();
    }

    void Update()
    {
        
    }

    public void SwitchScene(string sceneName)
    {
        if (sceneName == "GamePlayScene")
            SceneManager.LoadScene("GamePlayScene");
        else if (sceneName == "Shop")
            SceneManager.LoadScene("Shop");
        // 튜토리얼과 레벨 선택 씬이 완성될 시 if문 수정하기
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
