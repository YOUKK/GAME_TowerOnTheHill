using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager GetInstance { get { Init(); return instance; } }

    private MouseInputManager mouseInputM = new MouseInputManager();
    public static MouseInputManager MouseInputM { get { return instance.mouseInputM; } }

    void Awake()
    {
        Init();

        if (!PlayerPrefs.HasKey("coin"))
        {
            PlayerPrefs.SetInt("coin", 0);
            Debug.Log("Coin ���� ����");
        }

        // �Ʒ��� �׽�Ʈ�� ���� �ڵ�
        //PlayerPrefs.DeleteKey("chaUnlockLevel");
        //PlayerPrefs.DeleteKey("slotNum");
        PlayerPrefs.SetInt("chaUnlockLevel", 12);
        PlayerPrefs.SetInt("slotNum", 6);
        //Debug.Log("PlayerPref ���� ���� - �׽�Ʈ��");
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

    // fromScene�� ���� ��
    // toScene�� �̵��Ϸ��� ��
    // ���� ���� BGM ����� �޶� ���ڷ� 2�� ���� �޴� �ɷ� ����
    public void MoveScene(string fromScene, string toScene)
    {
        // ��ư ����
        try
        {
            SoundManager.Instance.PlayEffect("Button1");

            // �� �̵�
            SceneManager.LoadScene(toScene);

            if (fromScene == "GamePlayScene" || fromScene == "BossWave" || fromScene == "TutorialScene")
            {
                SoundManager.Instance.PlayBGM("Title");
            }

            if (toScene == "GamePlayScene" || toScene == "BossWave" || toScene == "TutorialScene")
            {
                SoundManager.Instance.PlayBGM("Battle");
            }
        }
        catch (System.Exception)
        {
            SceneManager.LoadScene(toScene);
        }
    }
}
