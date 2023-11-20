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

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
