using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĳ���ͼ��þ��� InventoryCanvas - Background - ScrollRect - Contents�� �ٴ� ��ũ��Ʈ
// �رݵ� ĳ���ͱ��� �����ִ� ���
public class UnlockCharacter : MonoBehaviour
{
    private int chaUnlockLevel = 0;

	void Start()
    {
		if (!PlayerPrefs.HasKey("chaUnlockLevel"))
		{
			PlayerPrefs.SetInt("chaUnlockLevel", 2);
            Debug.Log("unlocklevel ���� ������!");
		}

        chaUnlockLevel = PlayerPrefs.GetInt("chaUnlockLevel");
        Debug.Log("chaUnlockLevel" + chaUnlockLevel);
        for(int i = 0; i < chaUnlockLevel; i++)
		{
            transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(true);
		}
    }

    void Update()
    {
        
    }

}
