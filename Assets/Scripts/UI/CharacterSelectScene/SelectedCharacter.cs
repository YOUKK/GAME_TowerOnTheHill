using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// CharacterSelectScene의 SelectedCanvas에서 선택된 캐릭터 버튼을 관리하는 스크립트
// SelectedCanvas에 붙음

// json파일로 저장하기 위해 만든 class
[System.Serializable]
public class ButtonList
{
    public List<CharacterButtonList> list = new List<CharacterButtonList>();
}

public class SelectedCharacter : MonoBehaviour
{
    private CharacterInventory characterInventory;

    [SerializeField]
    private int turn = 0; // SelectedCanvas에서 현재 채워져야 할 번호
    public int Turn { get { return turn; } set { turn = value;} }
    private int max = 0; // SelectedCanvas에서 채울 수 있는 최대 프레임 수(활성화된 프레임 오브젝트 수)
    private bool canStart = false;


    private List<GameObject> characterFrames = new List<GameObject>();
    public List<GameObject> CharacterFrames { get { return characterFrames; } }
    //[SerializeField]
    //private List<CharacterButtonList> buttonList = new List<CharacterButtonList>(); // 현재 SelectCanvas에 있는 버튼들의 리스트
    private ButtonList saveButtonList = new ButtonList();

	private void Awake()
	{
        max = Managers.Instance.slotNum;
        SetSlot();
    }

	void Start()
    {
        characterInventory = GameObject.Find("InventoryCanvas").GetComponent<CharacterInventory>();

        for (int i = 0; i < 8; i++)
            characterFrames.Add(gameObject.transform.GetChild(i).gameObject);
        for(int i = 0; i < 8; i++)
            saveButtonList.list.Add(CharacterButtonList.None); // 모든 칸이 비어있음
    }

    void Update()
    {

    }

    private void SetSlot()
	{
        for(int i = 0; i < max; i++)
		{
            transform.GetChild(i).gameObject.SetActive(true);
		}
	}

    // buttonList를 json 파일로 저장하기
    public void SaveButtonListToJson()
	{
        string jsonData = JsonUtility.ToJson(saveButtonList, true);
        string path = Path.Combine(Application.dataPath, "buttonList.json");
        File.WriteAllText(path, jsonData);

        Debug.Log(jsonData);
	}

    public void PrintDic()
	{
        for(int i=0;i<4; i++)
		{
            Debug.Log(i + " " + saveButtonList.list[i]);
		}
	}

    // 리스트 당기는 기능 & 당겨지는 오브젝트들의 myLocation-1
    public void PullButtonList(int index)
	{
        for(int i = index; i < turn - 1; i++)
		{
            saveButtonList.list[i] = saveButtonList.list[i + 1];
            characterInventory.enumPerButtonDic[saveButtonList.list[i]].GetComponent<CharacterSelectButton>().MyLocation--;

            // 당겨지는 버튼의 부모 오브젝트를 위에 있는 프레임 오브젝트로 교체하기
            characterInventory.enumPerButtonDic[saveButtonList.list[i + 1]].GetComponent<CharacterSelectButton>().PullCanvas(characterFrames[i]);
		}
        saveButtonList.list[turn - 1] = CharacterButtonList.None; // 마지막칸 None이 됨

        //PrintDic();
	}

    public void SetButtonOnList(int index, CharacterButtonList cha)
	{
        saveButtonList.list[index] = cha;

        //PrintDic();
	}

    // 추가해야할 frame 오브젝트를 알려주는 함수
    public GameObject GetNextTurn()
	{
        return characterFrames[turn];
	}

    // 빈 characterFrame이 있으면 true, 다 채워지면 false
    public bool CanAddButton()
	{
        if (turn < max)
            canStart = true;
        else
            canStart = false;

        return canStart;
	}
}
