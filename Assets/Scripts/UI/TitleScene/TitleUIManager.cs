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
        // Ʃ�丮��� ���� ���� ���� �ϼ��� �� if�� �����ϱ�
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
