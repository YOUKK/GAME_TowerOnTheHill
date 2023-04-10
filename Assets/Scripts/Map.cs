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

[System.Serializable]
public struct Seat
{
    public GameObject character;
    public SpriteRenderer background;
    bool isCharacterOn;
    bool usable;

    public Seat(GameObject go = null, SpriteRenderer sp = null)
    {
        character = go;
        background = sp;
        isCharacterOn = false;
        usable = true;
    }
}

public class Map : MonoBehaviour
{
    public List<Seat> line1 = new List<Seat>();
    public List<Seat> line2 = new List<Seat>();
    public List<Seat> line3 = new List<Seat>();
    public List<Seat> line4 = new List<Seat>();
    public List<Seat> line5 = new List<Seat>();

    void Start()
    {

    }

    void Update()
    {

    }
}
