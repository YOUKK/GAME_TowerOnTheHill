using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Attacker : MonoBehaviour
{
    public List<GameObject> monsterList = new List<GameObject>();
    public List<GameObject> MonsterList
    {
        get
        {
            for (int i = 0; i < monsterList.Count; ++i)
                if (monsterList[i] == null)
                {
                    // Debug.Log("Attacker : Removed");
                    monsterList.RemoveAt(i);
                }
            return monsterList;
        }
    }
    public int monsterCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Debug.Log($"Attacker : Mosnter in {collision.name}");
            monsterList.Add(collision.gameObject);
            ++monsterCount;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Debug.Log("Attacker : Mosnter dead");
            if (monsterList.Remove(collision.gameObject) == false)
                Debug.Log("Attacker : Wrong Access");
            --monsterCount;
        }
    }
}
