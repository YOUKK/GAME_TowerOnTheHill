using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Line
{
    public int lineNumber;
    public float hpSum;
    public Vector2 location;
}

public class Map : MonoBehaviour
{
    static Map              _map;
    public static Map GetInstance() { return _map; }

    private int mapX = 9, mapY = 5;
    List<List<GameObject>>  seats = new List<List<GameObject>>();
    public List<List<GameObject>> Seats { get => seats; set => seats = value; }

    // 추가 방어선 오브젝트
    [SerializeField]
    private GameObject heightLine4;
    [SerializeField]
    private GameObject Line9;

	void Awake()
	{
		if (_map != null)
		{
			Destroy(gameObject);
			return;
		}
		_map = this;

        // Map의 자식 Object들 저장
        Transform[] seatTransform = GetComponentsInChildren<Transform>();

        // 자식 Object 중 Seat만 저장
        List<GameObject> tempSeats = new List<GameObject>();
        for (int i = 0; i < seatTransform.Length; i++)
            if (seatTransform[i].CompareTag("Seat"))
                tempSeats.Add(seatTransform[i].gameObject);

        // 1차원 배열의 Seat Object들을 2차원 List로 변환
        int idx = 0;
        for(int i = 0; i < mapY; i++)
            seats.Add(new List<GameObject>());
        for(int i = 0; i < mapX; i++)
		{
            for(int j = 0; j < mapY; j++)
			{
                seats[j].Add(tempSeats[idx].gameObject);
                idx++;
			}
		}
    }

    // AddLine() 테스트 코드
    public bool flag = false;
	private void Update()
	{
        if (flag)
		{
            AddLine();
            flag = false;
		}
	}
    

	// (상점에서 구매시) 방어선 증가 기능
	private void AddLine()
	{
        // seats에 새로운 방어선 seat 추가
        seats.Add(new List<GameObject>());
        for(int i = 0; i < mapY; i++)
            seats[i].Add(Line9.transform.GetChild(i).gameObject);

        // UI상 방어선 활성화
        Line9.SetActive(true);
        heightLine4.SetActive(true);
	}

    // 캐릭터 배치
    public void PutCharacter(Vector2 location, GameObject character)
    {
        int x = (int)location.x;
        int y = (int)location.y;

        seats[y][x].GetComponent<Seat>().character = Instantiate(character, seats[y][x].transform.position, transform.rotation);
        seats[y][x].GetComponent<Seat>().character.GetComponent<Character>().Location = new Vector2(x, y);
        seats[y][x].GetComponent<Seat>().character.name = character.name;

        if (character.GetComponent<Tower>() == null) // tower가 아닌 경우
            seats[y][x].GetComponent<Seat>().isCharacterOn = true;
        seats[y][x].GetComponent<Seat>().usable = false;
        seats[y][x].GetComponent<Seat>().character.GetComponentInChildren<SpriteRenderer>().sortingOrder = y * (-1);
        seats[y][x].GetComponent<Seat>().character.GetComponentInChildren<MonsterCheck>().LocalPos = location;
    }

    // 캐릭터 제거
    public void RemoveCharacter(Vector2 location)
    {
        int x = (int)location.x;
        int y = (int)location.y;

        Destroy(seats[y][x].GetComponent<Seat>().character);
        seats[y][x].GetComponent<Seat>().isCharacterOn = false;
        seats[y][x].GetComponent<Seat>().usable = true; // 좀비가 Seat 위에 있으면 생성 불가하게 수정
    }

    public Vector2[] GetCharacterPlacedSeats()
    {
        List<Vector2> seatMatrix = new List<Vector2>();

        for (int i = 0; i < mapX; i++)
        {
            for (int j = 0; j < mapY; j++)
            {
                if (seats[j][i].GetComponent<Seat>().isCharacterOn)
                    seatMatrix.Add(seats[j][i].GetComponent<Seat>().location);
            }
        }

        return seatMatrix.ToArray();
    }

    public void ChangeCharacterSeat(Vector2 seat1, Vector2 seat2)
    {
        // 다른 몬스터가 공격중인 캐릭터가 자리를 바꾸면 어떻게 해야할까
        // 이미 존재하는 캐릭터의 위치를 바꾸지 말고, 존재하는 두 캐릭터를 죽이고
        // 같은 종류의 새로운 캐릭터를 자리만 바꿔서 다시 생성한다.
        // 이때, 원래 캐릭터의 체력 정보를 저장했다가 새로 생성된 애들한테 적용해줘야 함.
        int x1 = (int)seat1.x;
        int y1 = (int)seat1.y;
        int x2 = (int)seat2.x;
        int y2 = (int)seat2.y;

        string characterName1 = seats[y1][x1].GetComponent<Seat>().character.name;
        string characterName2 = seats[y2][x2].GetComponent<Seat>().character.name;
        GameObject newCharacter1 = Resources.Load<GameObject>($"/Prefabs/Character/{characterName1}");
        GameObject newCharacter2 = Resources.Load<GameObject>($"/Prefabs/Character/{characterName2}");


    }

    public Line GetLineInfo(int lineNum)
    {
        Line line;

        line.lineNumber = lineNum;
        line.location = seats[lineNum][0].GetComponent<Transform>().position;
        line.hpSum = 0;

        for(int i = 0; i < seats[lineNum].Count; ++i)
        {
            GameObject go = seats[lineNum][i].GetComponent<Seat>().character;
            if (go) line.hpSum += go.GetComponent<Character>().HealthPoint;
        }

        return line;
    }
}
