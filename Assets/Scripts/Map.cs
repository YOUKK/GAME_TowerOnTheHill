using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Seat
/// : ĳ���� ����, ��� ����, ĳ���� ����, ��밡�� ����
/// 
/// Map
/// : Seat�� ����, ���κ� �ջ� ����, ��� ����, ���� �ջ�, ĳ���� ��ġ&����
/// </summary>

public class Map : MonoBehaviour
{
    static Map _map;
    public static Map GetInstance() { return _map; }
    public int mapX, mapY;
    
    List<List<GameObject>> seats = new List<List<GameObject>>();

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
        Transform[] seatTransform = GetComponentsInChildren<Transform>();

        List<GameObject> tempSeats = new List<GameObject>();
        for (int i = 0; i < seatTransform.Length; i++)
            if (seatTransform[i].CompareTag("Seat"))
                tempSeats.Add(seatTransform[i].gameObject);

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

    public void PutCharacter(Vector2 _location, GameObject _character)
    {
        int x = (int)_location.x;
        int y = (int)_location.y;

        seats[y][x].GetComponent<Seat>().character =
            Instantiate(_character, seats[y][x].transform.position, transform.rotation);
        seats[y][x].GetComponent<Seat>().isCharacterOn = true;
        seats[y][x].GetComponent<Seat>().usable = false;
    }

    public void RemoveCharacter(Vector2 _location)
    {
        int x = (int)_location.x;
        int y = (int)_location.y;

        Destroy(seats[y][x].GetComponent<Seat>().character);
        seats[y][x].GetComponent<Seat>().isCharacterOn = false;
        seats[y][x].GetComponent<Seat>().usable = true; // ���� ���� ������ ���� �Ұ��ϰ� ����
    }
}
