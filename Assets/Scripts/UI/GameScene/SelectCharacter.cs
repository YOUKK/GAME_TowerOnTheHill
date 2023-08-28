using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// CharacterSelectScene���� ������ ĳ���� ������ Load�ϴ� ��ũ��Ʈ
// CharacterButtonCanvas�� ����

public class SelectCharacter : MonoBehaviour
{
    private ButtonList loadButtonList = new ButtonList();

    //private List<chaType> selectedCha = new List<chaType>(); // ���õ� ĳ���� ����Ʈ
    //public List<chaType> SelectedCha { get { return selectedCha; } }

    //private List<GameObject> spriteCha = new List<GameObject>(); // ���õ� ĳ������ ��������Ʈ
    //public List<GameObject> SpriteCha { get { return spriteCha; } }

    //private List<GameObject> objectCha = new List<GameObject>(); // ���õ� ĳ������ ���� ������Ʈ�� ���� ����Ʈ
    //public List<GameObject> ObjectCha { get { return objectCha; } }

    //private List<int> chaPrice = new List<int>(); // ���õ� ĳ������ ���ݵ�
    //public List<int> ChaPrice { get { return chaPrice; } }

    //private List<int> coolTime = new List<int>(); // ���õ� ĳ������ ��Ÿ��
    //public List<int> CoolTime { get { return coolTime; } }

    void Start()
    {
        // �Ʒ� 4���� ������� ���õǾ��ٰ� ����
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
        Debug.Log("json������ �о����!");
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

        Debug.Log("��ư �����ϱ�!");
	}
}
