using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SelectedCanvas에서 선택된 캐릭터 버튼을 관리하는 스크립트
// SelectedCanvas에 붙음

public class SelectedCharacter : MonoBehaviour
{
    private CharacterInventory characterInventory;

    [SerializeField]
    private int turn = 0; // SelectedCanvas에서 현재 채워져야 할 번호
    public int Turn { get { return turn; } set { turn = value;} }

    private List<GameObject> characterFrames = new List<GameObject>();
    public List<GameObject> CharacterFrames { get { return characterFrames; } }
    [SerializeField]
    private List<CharacterButtonList> buttonList = new List<CharacterButtonList>(); // 현재 SelectCanvas에 있는 버튼들의 리스트

    void Start()
    {
        characterInventory = GameObject.Find("InventoryCanvas").GetComponent<CharacterInventory>();

        for (int i = 0; i < 8; i++)
            characterFrames.Add(gameObject.transform.GetChild(i).gameObject);
        for(int i = 0; i < 4; i++)
            buttonList.Add(CharacterButtonList.None); // 모든 칸이 비어있음
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
        // 리스트 당기는 기능 & 당겨지는 오브젝트들의 myLocation-1
        for(int i = index; i < turn - 1; i++)
		{
            buttonList[i] = buttonList[i + 1];
            characterInventory.enumPerButtonDic[buttonList[i]].GetComponent<CharacterSelectButton>().MyLocation--;
		}
        buttonList[turn - 1] = CharacterButtonList.None; // 마지막칸 None이 됨

        PrintDic();
	}

    public void SetButtonOnList(int index, CharacterButtonList cha)
	{
        buttonList[index] = cha;

        PrintDic();
	}

    // 추가해야할 frame 오브젝트를 알려주는 함수
    public GameObject GetNextTurn()
	{
        return characterFrames[turn];
	}
}
