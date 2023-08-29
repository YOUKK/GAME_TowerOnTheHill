using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 캐릭터 버튼에 붙는 스크립트
// 버튼이 공통적으로 가지는 기능이 적힘

public class CharacterSelectButton : MonoBehaviour
{
    private SelectedCharacter selectedCharacter;
    private CharacterInventory characterInventory;
    [SerializeField]
    private CharacterButtonList character; // 캐릭터 버튼 enum, 하이어라키창에서 설정
    private StartButton startButton;


    public bool isSelect = false;

    private int myLocation; // 현재 SelectedCanvas에서의 번호
    public int MyLocation { get { return myLocation; } set { myLocation = value; } }
    private Transform motherObject; // 원래 부모 오브젝트
    private Vector2 temp = new Vector2(0, 0); // 원래 anchordPosition


    void Start()
    {
        selectedCharacter = GameObject.Find("SelectedCanvas").GetComponent<SelectedCharacter>();
        characterInventory = GameObject.Find("InventoryCanvas").GetComponent<CharacterInventory>();
        characterInventory.enumPerButtonDic.Add(character, gameObject);
        startButton = GameObject.Find("InventoryCanvas").transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<StartButton>();

        motherObject = transform.parent;
    }

    void Update()
    {
        
    }

    // SelectedCanvas에서 빈공간 땡기는 기능
    public void PullCanvas(GameObject mother)
	{
        transform.SetParent(mother.transform);
        temp.y -= 100; // 한칸 위로 당겨지면서 anchoredPosition의 PosY가 -100 더해짐
        StartCoroutine(MoveGo(Vector2.zero));
	}

    // 버튼 눌렀을 때 실행하는 함수
    // 버튼 클릭하면 인벤토리 캔버스 <-> 캐릭터 선택 캔버스 왔다갔다 하는 기능
    public void MoveCanvas()
    {
        if (!isSelect) // 인벤토리 캔버스 -> 캐릭터 선택 캔버스
        {
            if (selectedCharacter.CanAddButton())
            {
                transform.SetParent(selectedCharacter.GetNextTurn().transform);
                // temp는 인벤토리 캔버스에 있으면서 캐릭터 선택 캔버스의 자식 오브젝트일 때의 anchoredPosition
                temp = gameObject.GetComponent<RectTransform>().anchoredPosition;
                StartCoroutine(MoveGo(Vector2.zero));

                myLocation = selectedCharacter.Turn;
                selectedCharacter.SetButtonOnList(myLocation, character);
                selectedCharacter.Turn++;
                isSelect = true;
            }
        }
        else // 인벤토리 캔버스 -> 캐릭터 선택 캔버스
        {
            StartCoroutine(MoveBack(temp));

            selectedCharacter.PullButtonList(myLocation);
            selectedCharacter.Turn--;
            isSelect = false;
        }

        // startButton 체크
        CheckStartButton();
    }

    private void CheckStartButton()
	{
		if (!selectedCharacter.CanAddButton()) // start 가능
            startButton.CanPressButton();
		else // start 불가능
            startButton.CannotPressButton();
	}

	IEnumerator MoveGo(Vector2 des)
	{
        float time = 0f;

		RectTransform rectT = gameObject.GetComponent<RectTransform>();

        while(time < 1f)
		{
            rectT.anchoredPosition = Vector2.Lerp(rectT.anchoredPosition, des, time+=(Time.deltaTime*2));
            yield return null;
        }

        rectT.anchoredPosition = des;
	}

    // 인벤토리 캔버스 -> 캐릭터 선택 캔버스로 움직일 땐 부모 오브젝트 바꾸는걸 움직인 뒤에 해야되서 MoveBack()함수 만듦
    IEnumerator MoveBack(Vector2 des)
	{
        float time = 0f;

        RectTransform rectT = gameObject.GetComponent<RectTransform>();

        while(time < 1f)
		{
            rectT.anchoredPosition = Vector2.Lerp(rectT.anchoredPosition, des, time+=(Time.deltaTime*2));
            yield return null;
        }

        rectT.anchoredPosition = des;

        transform.SetParent(motherObject);
        rectT.anchoredPosition = Vector2.zero;
    }
}
