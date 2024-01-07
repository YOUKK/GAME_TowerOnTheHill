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
            string path = $"Prefabs/CharacterButton/{loadButtonList.list[i]}Button";
            GameObject button = Instantiate(Resources.Load<GameObject>(path), transform.position, Quaternion.identity);
            button.transform.SetParent(transform.GetChild(i));
            button.transform.localPosition = Vector3.zero;
            button.transform.localScale = Vector3.one;
 
            i++;
		}
	}
}
