using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ��ư�� unlock ����� �����ϴ� ��ũ��Ʈ
public class DoUnlock : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UnLock()
	{
        GetComponent<Button>().interactable = true;
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true); // monster
        gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false); // lock
        gameObject.transform.GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(true); // character
        gameObject.transform.GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(false); // lock
    }
}
