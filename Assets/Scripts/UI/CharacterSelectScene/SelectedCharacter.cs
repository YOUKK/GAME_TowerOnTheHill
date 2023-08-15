using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacter : MonoBehaviour
{
    private int turn = 0; // 현재 채워져야 할 번호
    private int Turn { get { return turn; } set { turn = value;} }
    public static List<GameObject> characterFrames = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 8; i++) {
            characterFrames.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        
    }
}
