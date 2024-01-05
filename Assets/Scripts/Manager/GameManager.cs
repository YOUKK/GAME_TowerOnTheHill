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
            Debug.Log("Coin 새로 생성");
        }

        // 아래 두줄은 테스트를 위한 코드
        //PlayerPrefs.DeleteKey("chaUnlockLevel");
        //PlayerPrefs.DeleteKey("slotNum");
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

    // fromScene은 현재 씬
    // toScene은 이동하려는 씬
    // 씬에 따라 BGM 재생이 달라서 인자로 2개 씬을 받는 걸로 수정
    public void MoveScene(string fromScene, string toScene)
    {
        SceneManager.LoadScene(toScene);

        if(fromScene == "GamePlayScene")
		{
            SoundManager.Instance.PlayBGM("Title");
		}
        
        if(toScene == "GamePlayScene" || toScene == "BossWave")
		{
            SoundManager.Instance.PlayBGM("Battle");
		}
    }
}
