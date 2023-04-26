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

    // 캐릭터 배치
    public void PutCharacter(Vector2 location, GameObject character)
    {
        int x = (int)location.x;
        int y = (int)location.y;

        seats[y][x].GetComponent<Seat>().character =
            Instantiate(character, seats[y][x].transform.position, transform.rotation);
        seats[y][x].GetComponent<Seat>().isCharacterOn = true;
        seats[y][x].GetComponent<Seat>().usable = false;
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
}
