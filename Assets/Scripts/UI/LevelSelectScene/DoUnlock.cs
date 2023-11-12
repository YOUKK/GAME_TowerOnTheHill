using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 실제 버튼의 unlock 기능을 수행하는 스크립트
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
