using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public string[] data;
    int step = 1;
    [SerializeField]
    private Text tutorialText;

    [SerializeField]
    GameObject Pause;

    private void Start()
    {
        TextAsset csvData = Resources.Load<TextAsset>($"Data/tutorial");
        //print(csvData);
        data = csvData.text.Split(new char[] { '\n' });
    }
    private void Update()
    {
        string[] dataset = data[step].Split(new char[] { ',' });

        print(dataset[1]);
        if( int.Parse(dataset[0]) <= GamePlayManagers.TimeM.Sec )
        {
            Time.timeScale = 0;

            GamePlayManagers.TimeM.StopTimer();

            Pause.SetActive(true);

            tutorialText.text = dataset[1];
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;

            GamePlayManagers.TimeM.StartTimer();

            Pause.SetActive(false);

            step++;
        }
    }
}