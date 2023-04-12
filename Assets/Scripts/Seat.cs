using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public Vector2 location;
    public GameObject character;
    public SpriteRenderer background;
    public bool isCharacterOn;
    public bool usable;

    void Start()
    {
        int y = transform.parent.name[4] - '0';
        int x = name[3] - '0';
        location = new Vector2(x, y);
        usable = true;
    }
}
