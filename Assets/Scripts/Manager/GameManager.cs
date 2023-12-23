using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager GetInstance { get { Init(); return instance; } }
    void Awake()
    {
        Init();

        if (!PlayerPrefs.HasKey("coin"))
        {
            PlayerPrefs.SetInt("coin", 0);
            Debug.Log("Coin ���� ����");
        }

        // �Ʒ� ������ �׽�Ʈ�� ���� �ڵ�
        PlayerPrefs.DeleteKey("chaUnlockLevel");
        PlayerPrefs.DeleteKey("slotNum");
    }

    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");
            if (go == null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<GameManager>();
        }
    }

    public void MoveScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
