using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public Vector2          location;       // 2���� �迭 ���� ��ǥ
    public GameObject       character;      // Seat���� ���� ĳ���� ����
    public SpriteRenderer   background;     // Seat �̹��� ����
    public bool             isCharacterOn;  // ĳ���Ͱ� ��ġ�Ǹ� true
    public bool             usable;         // ĳ���� ��ġ ���� ����

    // �θ� Object�� �̸��� ���� �� Ȯ��, �ڱ� Object�� �̸��� ���� �� Ȯ��
    void Start()
    {
        int y = transform.parent.name[4] - '0';
        int x = name[3] - '0';
        location = new Vector2(x, y);
        isCharacterOn = false;
        usable = true;
    }
}
