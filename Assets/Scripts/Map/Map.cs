using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	}

	void Start()
    {
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
            for(int j = 0; j < mapX; ++j, ++idx)
            {
                if(tempSeats[idx].CompareTag("Seat"))
                {
                    seats[i].Add(tempSeats[idx].gameObject);
                }
            }
        }
    }

    void Update()
    {

    }

    // ĳ���� ��ġ
    public void PutCharacter(Vector2 _location, GameObject _character)
    {
        int x = (int)_location.x;
        int y = (int)_location.y;

        seats[y][x].GetComponent<Seat>().character =
            Instantiate(_character, seats[y][x].transform.position, transform.rotation);
        seats[y][x].GetComponent<Seat>().isCharacterOn = true;
        seats[y][x].GetComponent<Seat>().usable = false;
    }

    // ĳ���� ����
    public void RemoveCharacter(Vector2 _location)
    {
        int x = (int)_location.x;
        int y = (int)_location.y;

        Destroy(seats[y][x].GetComponent<Seat>().character);
        seats[y][x].GetComponent<Seat>().isCharacterOn = false;
        seats[y][x].GetComponent<Seat>().usable = true; // ���� Seat ���� ������ ���� �Ұ��ϰ� ����
    }

    public float GetLineInfo(int line)
    {
        float lineInfo = 0;
        for(int i = 0; i < seats[line].Count; ++i)
        {
            GameObject go = seats[line][i].GetComponent<Seat>().character;
            if (go) lineInfo += go.GetComponent<Character>().HealthPoint;
        }

        return lineInfo;
    }
}
