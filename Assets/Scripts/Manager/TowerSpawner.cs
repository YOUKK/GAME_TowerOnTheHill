using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] GameObject tower;
    void Start()
    {
        //Debug.Log("빌드 오류 체크***** " + Map.GetInstance());
        if(Map.GetInstance() != null)
        { 
            if(SceneManager.GetActiveScene().name == "TutorialScene")
                Map.GetInstance().PutCharacter(new Vector2(0, 2), tower);
            else
            {
                Map.GetInstance().PutCharacter(new Vector2(0, 0), tower);
                Map.GetInstance().PutCharacter(new Vector2(0, 2), tower);
                Map.GetInstance().PutCharacter(new Vector2(0, 4), tower);
            }
        }
    }
}
