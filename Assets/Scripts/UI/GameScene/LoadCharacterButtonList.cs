using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// CharacterSelectScene���� ������ ĳ���� ������ Load�ϴ� ��ũ��Ʈ
// CharacterButtonCanvas�� ����

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
            string path = $"Prefabs/CharacterButton/{loadButtonList.list[i]}Button";
            GameObject button = Instantiate(Resources.Load<GameObject>(path), transform.position, Quaternion.identity);
            button.transform.SetParent(transform.GetChild(i));
            button.transform.localPosition = Vector3.zero;
            button.transform.localScale = Vector3.one;
 
            i++;
		}
	}
}
