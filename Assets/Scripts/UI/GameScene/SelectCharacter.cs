using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// CharacterSelectScene에서 선택한 캐릭터 정보를 Load하는 스크립트
// CharacterButtonCanvas에 붙음

public class SelectCharacter : MonoBehaviour
{
    private ButtonList loadButtonList = new ButtonList();

    //private List<chaType> selectedCha = new List<chaType>(); // 선택된 캐릭터 리스트
    //public List<chaType> SelectedCha { get { return selectedCha; } }

    //private List<GameObject> spriteCha = new List<GameObject>(); // 선택된 캐릭터의 스프라이트
    //public List<GameObject> SpriteCha { get { return spriteCha; } }

    //private List<GameObject> objectCha = new List<GameObject>(); // 선택된 캐릭터의 게임 오브젝트가 담기는 리스트
    //public List<GameObject> ObjectCha { get { return objectCha; } }

    //private List<int> chaPrice = new List<int>(); // 선택된 캐릭터의 가격들
    //public List<int> ChaPrice { get { return chaPrice; } }

    //private List<int> coolTime = new List<int>(); // 선택된 캐릭터의 쿨타임
    //public List<int> CoolTime { get { return coolTime; } }

    void Start()
    {
        // 아래 4개가 순서대로 선택되었다고 가정
        //selectedCha.Add(chaType.Sun);
        //selectedCha.Add(chaType.PeaSh);
        //selectedCha.Add(chaType.Wall);
        //selectedCha.Add(chaType.GasMu);

        //spriteCha.Add(Resources.Load<GameObject>("Prefabs/Sprite/SunFlower"));
        //spriteCha.Add(Resources.Load<GameObject>("Prefabs/Sprite/PeaShooter"));
        //spriteCha.Add(Resources.Load<GameObject>("Prefabs/Sprite/Walnut"));
        //spriteCha.Add(Resources.Load<GameObject>("Prefabs/Sprite/WideArea"));

        //objectCha.Add(Resources.Load<GameObject>("Prefabs/Character/SunFlower"));
        //objectCha.Add(Resources.Load<GameObject>("Prefabs/Character/PeaShooter"));
        //objectCha.Add(Resources.Load<GameObject>("Prefabs/Character/Walnut"));
        //objectCha.Add(Resources.Load<GameObject>("Prefabs/Character/GasMushroom"));

        //chaPrice.Add(50);
        //chaPrice.Add(100);
        //chaPrice.Add(50);
        //chaPrice.Add(75);

        //coolTime.Add(7);
        //coolTime.Add(7);
        //coolTime.Add(30);
        //coolTime.Add(7);
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
        Debug.Log("json데이터 읽어오기!");
        Debug.Log(jsonData);
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
            }

			i++;
		}

        Debug.Log("버튼 설정하기!");
	}
}
