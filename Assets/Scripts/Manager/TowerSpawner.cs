using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] GameObject tower;
    void Start()
    {
        if(Map.GetInstance() != null)
        { 
            Map.GetInstance().PutCharacter(new Vector2(0, 0), tower);
            Map.GetInstance().PutCharacter(new Vector2(0, 2), tower);
            Map.GetInstance().PutCharacter(new Vector2(0, 4), tower);
        }
    }

    void Update()
    {
        
    }
}
