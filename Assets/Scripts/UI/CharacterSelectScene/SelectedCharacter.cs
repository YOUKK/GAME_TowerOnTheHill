using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// CharacterSelectScene�� SelectedCanvas���� ���õ� ĳ���� ��ư�� �����ϴ� ��ũ��Ʈ
// SelectedCanvas�� ����

// json���Ϸ� �����ϱ� ���� ���� class
[System.Serializable]
public class ButtonList
{
    public List<CharacterButtonList> list = new List<CharacterButtonList>();
}

public class SelectedCharacter : MonoBehaviour
{
    private CharacterInventory characterInventory;

    [SerializeField]
    private int turn = 0; // SelectedCanvas���� ���� ä������ �� ��ȣ
    public int Turn { get { return turn; } set { turn = value;} }
    private int max = 0; // SelectedCanvas���� ä�� �� �ִ� �ִ� ������ ��(Ȱ��ȭ�� ������ ������Ʈ ��)
    private bool canStart = false;


    private List<GameObject> characterFrames = new List<GameObject>();
    public List<GameObject> CharacterFrames { get { return characterFrames; } }
    //[SerializeField]
    //private List<CharacterButtonList> buttonList = new List<CharacterButtonList>(); // ���� SelectCanvas�� �ִ� ��ư���� ����Ʈ
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
            saveButtonList.list.Add(CharacterButtonList.None); // ��� ĭ�� �������
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

    // buttonList�� json ���Ϸ� �����ϱ�
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

    // ����Ʈ ���� ��� & ������� ������Ʈ���� myLocation-1
    public void PullButtonList(int index)
	{
        for(int i = index; i < turn - 1; i++)
		{
            saveButtonList.list[i] = saveButtonList.list[i + 1];
            characterInventory.enumPerButtonDic[saveButtonList.list[i]].GetComponent<CharacterSelectButton>().MyLocation--;

            // ������� ��ư�� �θ� ������Ʈ�� ���� �ִ� ������ ������Ʈ�� ��ü�ϱ�
            characterInventory.enumPerButtonDic[saveButtonList.list[i + 1]].GetComponent<CharacterSelectButton>().PullCanvas(characterFrames[i]);
		}
        saveButtonList.list[turn - 1] = CharacterButtonList.None; // ������ĭ None�� ��

        //PrintDic();
	}

    public void SetButtonOnList(int index, CharacterButtonList cha)
	{
        saveButtonList.list[index] = cha;

        //PrintDic();
	}

    // �߰��ؾ��� frame ������Ʈ�� �˷��ִ� �Լ�
    public GameObject GetNextTurn()
	{
        return characterFrames[turn];
	}

    // �� characterFrame�� ������ true, �� ä������ false
    public bool CanAddButton()
	{
        if (turn < max)
            canStart = true;
        else
            canStart = false;

        return canStart;
	}
}
