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
	}
}
