using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// CharacterSelectScene에서 선택한 캐릭터 정보를 Load하는 스크립트
// CharacterButtonCanvas에 붙음

public class LoadCharacterButtonList : MonoBehaviour
{
    private ButtonList loadButtonList = new ButtonList();

    void Start()
    {

    }

	private void Awake()
	{
		LoadButtonListFromJson();
		SetButton();
	}

	private void LoadButtonListFromJson()
	{
        string path = Path.Combine(Application.dataPath, "buttonList.json");
        string jsonData = File.ReadAllText(path);
        loadButtonList = JsonUtility.FromJson<ButtonList>(jsonData);
    }

    private void SetButton()
	{
        int i = 0;
        while(loadButtonList.list[i] != CharacterButtonList.None)
		{
			switch (loadButtonList.list[i])
			{
                case CharacterButtonList.Sunflower:
                    GameObject button = Instantiate(Resources.Load<GameObject>("Prefabs/CharacterButton/SunFlowerButton"), transform.position, Quaternion.identity);
                    button.transform.SetParent(transform.GetChild(i));
                    button.transform.localPosition = Vector3.zero;
                    button.transform.localScale = Vector3.one;
                    break;
                case CharacterButtonList.PeaShooter:
                    button = Instantiate(Resources.Load<GameObject>("Prefabs/CharacterButton/PeaShooterButton"), transform.position, Quaternion.identity);
                    button.transform.SetParent(transform.GetChild(i));
                    button.transform.localPosition = Vector3.zero;
                    button.transform.localScale = Vector3.one;
                    break;
                case CharacterButtonList.Walnut:
                    button = Instantiate(Resources.Load<GameObject>("Prefabs/CharacterButton/WalnutButton"), transform.position, Quaternion.identity);
                    button.transform.SetParent(transform.GetChild(i));
                    button.transform.localPosition = Vector3.zero;
                    button.transform.localScale = Vector3.one;
                    break;
                case CharacterButtonList.GasMushroom:
                    button = Instantiate(Resources.Load<GameObject>("Prefabs/CharacterButton/GasMushroomButton"), transform.position, Quaternion.identity);
                    button.transform.SetParent(transform.GetChild(i));
                    button.transform.localPosition = Vector3.zero;
                    button.transform.localScale = Vector3.one;
                    break;
                case CharacterButtonList.IceShooter:
                    button = Instantiate(Resources.Load<GameObject>("Prefabs/CharacterButton/IceShooterButton"), transform.position, Quaternion.identity);
                    button.transform.SetParent(transform.GetChild(i));
                    button.transform.localPosition = Vector3.zero;
                    button.transform.localScale = Vector3.one;
                    break;
                case CharacterButtonList.Eater:
                    button = Instantiate(Resources.Load<GameObject>("Prefabs/CharacterButton/EaterButton"), transform.position, Quaternion.identity);
                    button.transform.SetParent(transform.GetChild(i));
                    button.transform.localPosition = Vector3.zero;
                    button.transform.localScale = Vector3.one;
                    break;
                case CharacterButtonList.Bomb:
                    button = Instantiate(Resources.Load<GameObject>("Prefabs/CharacterButton/BombButton"), transform.position, Quaternion.identity);
                    button.transform.SetParent(transform.GetChild(i));
                    button.transform.localPosition = Vector3.zero;
                    button.transform.localScale = Vector3.one;
                    break;
                case CharacterButtonList.Buffer:
                    button = Instantiate(Resources.Load<GameObject>("Prefabs/CharacterButton/BufferButton"), transform.position, Quaternion.identity);
                    button.transform.SetParent(transform.GetChild(i));
                    button.transform.localPosition = Vector3.zero;
                    button.transform.localScale = Vector3.one;
                    break;
            }



            i++;
		}
	}
}
