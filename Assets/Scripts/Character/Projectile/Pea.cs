using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pea : MonoBehaviour
{
    private Character myShooter;

    private void Start()
    {
        myShooter = gameObject.GetComponentInParent<Character>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "Enemy" )
        {
            collision.gameObject.GetComponent<Monster>().Hit((int)myShooter.Strength);
            gameObject.SetActive(false);
        }
    }
}
