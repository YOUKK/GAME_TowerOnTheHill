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
