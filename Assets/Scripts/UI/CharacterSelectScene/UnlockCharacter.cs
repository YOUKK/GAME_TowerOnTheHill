using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĳ���ͼ��þ��� InventoryCanvas - Background - ScrollRect - Contents�� �ٴ� ��ũ��Ʈ
// �رݵ� ĳ���ͱ��� �����ִ� ���
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
