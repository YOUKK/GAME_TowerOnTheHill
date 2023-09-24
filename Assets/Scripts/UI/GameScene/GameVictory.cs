using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVictory : MonoBehaviour
{
    [SerializeField]
    private GameObject victoryText;

    void Start()
    {
        
    }

    void Update()
    {
		if (MonsterSpawner.GetInstance.IsAllMonsterDead)
		{
            victoryText.SetActive(true);
            Debug.Log("½Â¸®!!!!");
		}
    }
}
