using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public Vector2          location;       // 2차원 배열 상의 좌표
    public GameObject       character;      // Seat위에 놓인 캐릭터 정보
    public SpriteRenderer   background;     // Seat 이미지 정보
    public bool             isCharacterOn;  // 캐릭터가 배치되면 true
    public bool             usable;         // 캐릭터 배치 가능 여부

    // 부모 Object의 이름을 통해 행 확인, 자기 Object의 이름을 통해 열 확인
    void Start()
    {
        int y = transform.parent.name[4] - '0';
        int x = name[3] - '0';
        location = new Vector2(x, y);
        isCharacterOn = false;
        usable = true;
    }
}
