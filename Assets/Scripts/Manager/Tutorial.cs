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

    [SerializeField]
    GameObject NextText;

    private Map MapBase;

    string[] dataset;

    private void Start()
    {
        MapBase = GameObject.Find("Map_Base").GetComponent<Map>();
        TextAsset csvData = Resources.Load<TextAsset>($"Data/tutorial");
        data = csvData.text.Split(new char[] { '\n' });
    }
    private void Update()
    {
        try
        {
            dataset = data[step].Split(new char[] { ',' });
            dataset[2] = dataset[2].Substring(0, dataset[2].Length - 1);
            dataset[1] = dataset[1].Replace(".", ",");
        }
        catch (System.Exception)
        {
            return;
        }
        if ( int.Parse(dataset[0]) <= GamePlayManagers.TimeM.Sec )
        {
            Time.timeScale = 0;

            GamePlayManagers.TimeM.StopTimer();

            Pause.SetActive(true);
            tutorialText.text = dataset[1];

            switch(dataset[2])
            {
                case "Gem":
                    NextText.SetActive(false);
                    if (GameObject.Find("FallingGem(Clone)"))
                    {
                        return;
                    }
                    break;

                default:
                    NextText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        break;
                    }
                    return;
            }
            PassTutorial();
        }
    }
    void PassTutorial()
    {
        GamePlayManagers.TimeM.StartTimer();

        Pause.SetActive(false);

        Time.timeScale = 1;

        step++;
    }
}