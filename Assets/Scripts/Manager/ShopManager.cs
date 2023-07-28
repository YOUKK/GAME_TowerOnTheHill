using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    GameObject[] buttons;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ExitShop()
    {
        gameObject.SetActive(false);
    }

    public void ActivateHammer(GameObject hammerUI)
    {
        hammerUI.SetActive(true);
        // 코인 감소
    }
}
