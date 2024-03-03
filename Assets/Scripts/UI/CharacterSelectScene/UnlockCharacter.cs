using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터선택씬의 InventoryCanvas - Background - ScrollRect - Contents에 붙는 스크립트
// 해금된 캐릭터까지 보여주는 기능
public class UnlockCharacter : MonoBehaviour
{
	void Start()
    {

        int chaUnlockLevel = GameManager.GetInstance.GetPlayerData(PlayerDataKind.ChaUnlockLevel);
        //Debug.Log("chaUnlockLevel" + chaUnlockLevel);
        for(int i = 0; i < chaUnlockLevel; i++)
		{
            transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(true);
		}
    }

    void Update()
    {
        
    }

}
