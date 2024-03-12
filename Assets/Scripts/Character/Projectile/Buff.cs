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
    private void OnEnable()
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, new Vector2(3, 3), 0);

        for (int i = 0; i < hit.Length; i++)
        {
            if(hit[i].tag == "Character" && hit[i].name != "Buffer")
            {
                try
                {
                    switch ((int)gameObject.name[gameObject.name.Length - 1] - 48)
                    {
                        case 1:
                            hit[i].gameObject.GetComponent<Character>().Strength = hit[i].gameObject.GetComponent<Character>().status.strength + 5;
                            hit[i].gameObject.GetComponent<Character>().AttackDelay = hit[i].gameObject.GetComponent<Character>().status.attackDelay;
                            hit[i].gameObject.GetComponent<Character>().HealthPoint = hit[i].gameObject.GetComponent<Character>().status.healthPoint;
                            break;
                        case 2:
                            hit[i].gameObject.GetComponent<Character>().Strength = hit[i].gameObject.GetComponent<Character>().status.strength;
                            hit[i].gameObject.GetComponent<Character>().AttackDelay = hit[i].gameObject.GetComponent<Character>().status.attackDelay - 0.5f;
                            hit[i].gameObject.GetComponent<Character>().HealthPoint = hit[i].gameObject.GetComponent<Character>().status.healthPoint;
                            break;
                        case 3:
                            hit[i].gameObject.GetComponent<Character>().Strength = hit[i].gameObject.GetComponent<Character>().status.strength;
                            hit[i].gameObject.GetComponent<Character>().AttackDelay = hit[i].gameObject.GetComponent<Character>().status.attackDelay;
                            hit[i].gameObject.GetComponent<Character>().HealthPoint = hit[i].gameObject.GetComponent<Character>().status.healthPoint + 20;
                            break;
                        default:
                            break;
                    }
                }
                catch (System.Exception) { }
            }
        }
    }
}
