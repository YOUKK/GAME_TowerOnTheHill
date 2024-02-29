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
        Debug.Log("GameManager��ũ��Ʈ Awake ȣ��!!!!!!!!!!!!!!!");

        if (!PlayerPrefs.HasKey("coin"))
        {
            PlayerPrefs.SetInt("coin", 0);
            Debug.Log("Coin ���� ����");
        }
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
            Debug.Log("GameManager was generated");
        }
    }

    public int GetPlayerData(PlayerDataKind kind)
    {
        string path = Path.Combine(Application.dataPath, "PlayerData.json");
        if (!File.Exists(path))
        {
            Debug.Log("File Is Not Found");
            SetPlayerData(PlayerDataKind.Coin, 0);
            SetPlayerData(PlayerDataKind.SlotNum, 2);
            SetPlayerData(PlayerDataKind.ChaUnlockLevel, 2);
        }

        string jsonString = File.ReadAllText(path);
        playerData = JsonUtility.FromJson<PlayerData>(jsonString);

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
        string jsonData = JsonUtility.ToJson(playerData, true);
        string path = Path.Combine(Application.dataPath, "PlayerData.json");
        File.WriteAllText(path, jsonData);
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
