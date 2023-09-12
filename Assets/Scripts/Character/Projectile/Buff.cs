using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Character" && collision.name != "Buffer")
        {
            switch ((int)gameObject.name[gameObject.name.Length - 1] - 48)
            {
                case 1:
                    collision.gameObject.GetComponent<Character>().Strength = collision.gameObject.GetComponent<Character>().status.strength + 5;
                    break;
                case 2:
                    collision.gameObject.GetComponent<Character>().AttackDelay = collision.gameObject.GetComponent<Character>().status.attackDelay - 3;
                    break;
                case 3:
                    collision.gameObject.GetComponent<Character>().HealthPoint = collision.gameObject.GetComponent<Character>().status.healthPoint + 20;
                    break;
                default:
                    break;
            }
        }
    }
}
