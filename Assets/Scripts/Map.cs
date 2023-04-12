using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Seat
/// : 캐릭터 정보, 배경 정보, 캐릭터 유무, 사용가능 유무
/// 
/// Map
/// : Seat들 정보, 라인별 합산 정보, 배경 정보, 라인 합산, 캐릭터 배치&삭제
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
        seats[y][x].GetComponent<Seat>().usable = true; // 좀비가 아직 있으면 생성 불가하게 수정
    }
}
