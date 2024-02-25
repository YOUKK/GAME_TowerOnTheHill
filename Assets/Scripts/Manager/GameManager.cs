using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public struct PlayerData
{
    public int coin;
    public int slotNum;
    public int chaUnlockLevel;
}

public enum PlayerDataKind { Coin, SlotNum, ChaUnlockLevel }

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager GetInstance { get { Init(); return instance; } }

    private MouseInputManager mouseInputM = new MouseInputManager();
    public static MouseInputManager MouseInputM { get { return instance.mouseInputM; } }

    private static PlayerData playerData;

    void Awake()
    {
        Init();

        playerData = new PlayerData();

        if (!PlayerPrefs.HasKey("coin"))
        {
            PlayerPrefs.SetInt("coin", 0);
            Debug.Log("Coin 새로 생성");
        }
        
        // 아래는 테스트를 위한 코드
        //PlayerPrefs.DeleteKey("chaUnlockLevel");
        //PlayerPrefs.DeleteKey("slotNum");
        //PlayerPrefs.SetInt("chaUnlockLevel", 12);
        //PlayerPrefs.SetInt("slotNum", 6);
        //Debug.Log("PlayerPref 변수 설정 - 테스트용");
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
        Debug.Log("GameManager was generated");
    }

    public int GetPlayerData(PlayerDataKind kind)
    {
        string path = Path.Combine(Application.dataPath, "PlayerData.json");
        if(!File.Exists(path))
        {
            Debug.Log("File Is Not Found");
            SetPlayerData(PlayerDataKind.Coin, 0);
        }
        switch (kind)
        {
            case PlayerDataKind.Coin:
                {
                    return playerData.coin;
                }
            case PlayerDataKind.SlotNum:
                {
                    return playerData.slotNum;
                }
            case PlayerDataKind.ChaUnlockLevel:
                {
                    return playerData.chaUnlockLevel;
                }
            default:
                return playerData.coin;
        }
    }

    public void SetPlayerData(PlayerDataKind kind, int val)
    {
        switch (kind)
        {
            case PlayerDataKind.Coin:
                {
                    playerData.coin = val;
                    break;
                }
            case PlayerDataKind.SlotNum:
                {
                    playerData.slotNum = val;
                    break;
                }
            case PlayerDataKind.ChaUnlockLevel:
                {
                    playerData.chaUnlockLevel = val;
                    break;
                }
            default:
                break;
        }
        string jsonData = JsonUtility.ToJson(playerData);
        string path = Path.Combine(Application.dataPath, "PlayerData.json");
        File.WriteAllText(path, jsonData);
    }

    // fromScene은 현재 씬
    // toScene은 이동하려는 씬
    // 씬에 따라 BGM 재생이 달라서 인자로 2개 씬을 받는 걸로 수정
    public void MoveScene(string fromScene, string toScene)
    {
        // 버튼 사운드
        try
        {
            SoundManager.Instance.PlayEffect("Button1");

            // 씬 이동
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
