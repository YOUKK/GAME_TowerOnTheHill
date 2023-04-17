using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monster;
    public bool monsterCreate = false;
    public int lineNum;
    public GameObject[] lines = new GameObject[5];

    void Start()
    {   
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (monsterCreate)
        {
            Instantiate(monster, lines[lineNum].transform.position, transform.rotation);
            monsterCreate = false;
        }
    }
}
