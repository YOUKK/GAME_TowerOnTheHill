using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject hammer;
    [SerializeField]
    private GameObject victoryCharacter;

    public void ActiveHammer(bool active)
    {
        if(hammer)
        {
            hammer.SetActive(active);
        }
    }

    public void ActiveMonsterWaveTimer(bool active = true)
    {
        gameObject.GetComponent<MonsterWaveTimer>().enabled = active;
    }

    public void ActiveVictoryCharacter()
	{
        victoryCharacter.SetActive(true);
    }

    public void ActiveCharacterShow()
	{
        victoryCharacter.SetActive(false);

        GameObject obj = GetComponentInChildren<CharacterShow>(true).gameObject;
        obj.SetActive(true);
	}

    public void ActivePopupVictory()
    {
        GameObject obj = GetComponentInChildren<GameVictory>(true).gameObject;
        if(obj)
        {
            obj.SetActive(true);
        }
        /*else
        {
            Debug.LogError("Failed to find object Popup_Victory");
        }*/
    }

    public void ActivePopupDefeat()
	{
        GameObject obj = GetComponentInChildren<GameDefeat>(true).gameObject;
        if (obj)
        {
            obj.SetActive(true);
        }
        /*else
        {
            Debug.LogError("Failed to find object Popup_Defeat");
        }*/
    }
}
