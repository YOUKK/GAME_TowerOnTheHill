using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ĳ���� ��ư�� �ٴ� ��ũ��Ʈ
// ��ư�� ���������� ������ ����� ����

public class CharacterSelectButton : MonoBehaviour
{
    private SelectedCharacter selectedCharacter;
    private CharacterInventory characterInventory;
    [SerializeField]
    private CharacterButtonList character; // ĳ���� ��ư enum, ���̾��Űâ���� ����
    private StartButton startButton;


    public bool isSelect = false;

    private int myLocation; // ���� SelectedCanvas������ ��ȣ
    public int MyLocation { get { return myLocation; } set { myLocation = value; } }
    private Transform motherObject; // ���� �θ� ������Ʈ
    private Vector2 temp = new Vector2(0, 0); // ���� anchordPosition


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

    // SelectedCanvas���� ����� ����� ���
    public void PullCanvas(GameObject mother)
	{
        transform.SetParent(mother.transform);
        temp.y -= 100; // ��ĭ ���� ������鼭 anchoredPosition�� PosY�� -100 ������
        StartCoroutine(MoveGo(Vector2.zero));
	}

    // ��ư ������ �� �����ϴ� �Լ�
    // ��ư Ŭ���ϸ� �κ��丮 ĵ���� <-> ĳ���� ���� ĵ���� �Դٰ��� �ϴ� ���
    public void MoveCanvas()
    {
        if (!isSelect) // �κ��丮 ĵ���� -> ĳ���� ���� ĵ����
        {
            if (selectedCharacter.CanAddButton())
            {
                transform.SetParent(selectedCharacter.GetNextTurn().transform);
                // temp�� �κ��丮 ĵ������ �����鼭 ĳ���� ���� ĵ������ �ڽ� ������Ʈ�� ���� anchoredPosition
                temp = gameObject.GetComponent<RectTransform>().anchoredPosition;
                StartCoroutine(MoveGo(Vector2.zero));

                myLocation = selectedCharacter.Turn;
                selectedCharacter.SetButtonOnList(myLocation, character);
                selectedCharacter.Turn++;
                isSelect = true;
            }
        }
        else // �κ��丮 ĵ���� -> ĳ���� ���� ĵ����
        {
            StartCoroutine(MoveBack(temp));

            selectedCharacter.PullButtonList(myLocation);
            selectedCharacter.Turn--;
            isSelect = false;
        }

        // startButton üũ
        CheckStartButton();
    }

    private void CheckStartButton()
	{
		if (!selectedCharacter.CanAddButton()) // start ����
            startButton.CanPressButton();
		else // start �Ұ���
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

    // �κ��丮 ĵ���� -> ĳ���� ���� ĵ������ ������ �� �θ� ������Ʈ �ٲٴ°� ������ �ڿ� �ؾߵǼ� MoveBack()�Լ� ����
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
