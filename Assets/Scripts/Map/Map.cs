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

    public int              mapX, mapY;
    List<List<GameObject>>  seats = new List<List<GameObject>>();

    public List<List<GameObject>> Seats { get => seats; set => seats = value; }

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
        for (int i = 0; i < mapY; ++i)
        {
            seats.Add(new List<GameObject>());
            for (int j = 0; j < mapX; ++j, ++idx)
            {
                if (tempSeats[idx].CompareTag("Seat"))
                {
                    seats[i].Add(tempSeats[idx].gameObject);
                }
            }
        }
    }

    // 캐릭터 배치
    public void PutCharacter(Vector2 location, GameObject character)
    {
        int x = (int)location.x;
        int y = (int)location.y;

        seats[y][x].GetComponent<Seat>().character =
            Instantiate(character, seats[y][x].transform.position, transform.rotation);
        seats[y][x].GetComponent<Seat>().character.GetComponent<Character>().Location = new Vector2(x, y);
        if(character.GetComponent<Tower>() == null) // tower가 아닌 경우
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
