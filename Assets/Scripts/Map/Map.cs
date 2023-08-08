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

	void Awake()
	{
		if (_map != null)
		{
			Destroy(gameObject);
			return;
		}
		_map = this;

        // Map�� �ڽ� Object�� ����
        Transform[] seatTransform = GetComponentsInChildren<Transform>();

        // �ڽ� Object �� Seat�� ����
        List<GameObject> tempSeats = new List<GameObject>();
        for (int i = 0; i < seatTransform.Length; i++)
            if (seatTransform[i].CompareTag("Seat"))
                tempSeats.Add(seatTransform[i].gameObject);

        // 1���� �迭�� Seat Object���� 2���� List�� ��ȯ
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

    // ĳ���� ��ġ
    public void PutCharacter(Vector2 location, GameObject character)
    {
        int x = (int)location.x;
        int y = (int)location.y;

        seats[y][x].GetComponent<Seat>().character =
            Instantiate(character, seats[y][x].transform.position, transform.rotation);
        seats[y][x].GetComponent<Seat>().character.GetComponent<Character>().Location = new Vector2(x, y);
        if(character.GetComponent<Tower>() == null) // tower�� �ƴ� ���
            seats[y][x].GetComponent<Seat>().isCharacterOn = true;
        seats[y][x].GetComponent<Seat>().usable = false;
        seats[y][x].GetComponent<Seat>().character.GetComponentInChildren<SpriteRenderer>().sortingOrder = y * (-1);
    }

    // ĳ���� ����
    public void RemoveCharacter(Vector2 location)
    {
        int x = (int)location.x;
        int y = (int)location.y;

        Destroy(seats[y][x].GetComponent<Seat>().character);
        seats[y][x].GetComponent<Seat>().isCharacterOn = false;
        seats[y][x].GetComponent<Seat>().usable = true; // ���� Seat ���� ������ ���� �Ұ��ϰ� ����
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
