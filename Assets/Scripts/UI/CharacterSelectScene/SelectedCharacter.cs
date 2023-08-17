using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SelectedCanvas���� ���õ� ĳ���� ��ư�� �����ϴ� ��ũ��Ʈ
// SelectedCanvas�� ����

public class SelectedCharacter : MonoBehaviour
{
    private CharacterInventory characterInventory;

    [SerializeField]
    private int turn = 0; // SelectedCanvas���� ���� ä������ �� ��ȣ
    public int Turn { get { return turn; } set { turn = value;} }

    private List<GameObject> characterFrames = new List<GameObject>();
    public List<GameObject> CharacterFrames { get { return characterFrames; } }
    [SerializeField]
    private List<CharacterButtonList> buttonList = new List<CharacterButtonList>(); // ���� SelectCanvas�� �ִ� ��ư���� ����Ʈ

    void Start()
    {
        characterInventory = GameObject.Find("InventoryCanvas").GetComponent<CharacterInventory>();

        for (int i = 0; i < 8; i++)
            characterFrames.Add(gameObject.transform.GetChild(i).gameObject);
        for(int i = 0; i < 4; i++)
            buttonList.Add(CharacterButtonList.None); // ��� ĭ�� �������
    }

    void Update()
    {
        
    }

    public void PrintDic()
	{
        for(int i=0;i<4; i++)
		{
            Debug.Log(i + " " + buttonList[i]);
		}
	}

    public void PullButtonList(int index)
	{
        // ����Ʈ ���� ��� & ������� ������Ʈ���� myLocation-1
        for(int i = index; i < turn - 1; i++)
		{
            buttonList[i] = buttonList[i + 1];
            characterInventory.enumPerButtonDic[buttonList[i]].GetComponent<CharacterSelectButton>().MyLocation--;
		}
        buttonList[turn - 1] = CharacterButtonList.None; // ������ĭ None�� ��

        PrintDic();
	}

    public void SetButtonOnList(int index, CharacterButtonList cha)
	{
        buttonList[index] = cha;

        PrintDic();
	}

    // �߰��ؾ��� frame ������Ʈ�� �˷��ִ� �Լ�
    public GameObject GetNextTurn()
	{
        return characterFrames[turn];
	}
}
