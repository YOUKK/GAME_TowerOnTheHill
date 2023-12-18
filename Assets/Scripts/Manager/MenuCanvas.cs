using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject hammer;

    private void Start()
    {
        
    }

    public void ActiveHammer(bool active)
    {
        if(hammer)
        {
            hammer.SetActive(active);
        }
    }

    public void ActiveMonsterWaveTimer()
    {
        gameObject.GetComponent<MonsterWaveTimer>().enabled = true;
    }

    public void ActivePopupVectory()
    {
        GameObject obj = GetComponentInChildren<GameVictory>(true).gameObject;
        if(obj)
        {
            obj.SetActive(true);
        }
        else
        {
            Debug.LogError("Failed to find object Popup_Victory");
        }
    }
}
