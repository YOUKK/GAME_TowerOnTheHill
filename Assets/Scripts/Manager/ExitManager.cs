using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ExitManager : MonoBehaviour
{
    bool TimeState = true;
    [SerializeField]
    GameObject Pause;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // ½Ã°£ÀÌ Èå¸£°í ÀÖÀ» ¶§
        if (TimeState && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;

            GamePlayManagers.TimeM.StopTimer();

            Pause.SetActive(true);

            TimeState = false;
        }
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        TimeState = true;
        GamePlayManagers.TimeM.StartTimer();
        Pause.SetActive(false);
    }
    public void FinishButton()
    {
        Time.timeScale = 1;
        TimeState = true;
        Managers.TimeM.StartTimer();
        Managers.TimeM.InitTimer();
        SceneManager.LoadScene("Shop");
    }
    public void RestartButton()
    {
        Time.timeScale = 1;
        TimeState = true;
        GamePlayManagers.TimeM.StartTimer();
        GamePlayManagers.TimeM.InitTimer();
        SceneManager.LoadScene("GamePlayScene");
    }
}
