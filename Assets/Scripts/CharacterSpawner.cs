using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterSpawner : MonoBehaviour
{
    GameObject character;
    bool isHoldingChar = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(isHoldingChar)
        {
            Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            character.transform.position = MousePosition + Vector3.forward;

            if (Input.GetMouseButtonUp(0))
            {
                isHoldingChar = false;
                Debug.DrawRay(MousePosition, Vector3.forward * 10.0f, Color.red, 3.0f);

                RaycastHit2D hit = Physics2D.Raycast(MousePosition, Vector3.forward, 10.0f);
                if (hit)
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
    }

    public void GetCharacterInfo(string name)
    {
        if (!isHoldingChar)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            character = Instantiate(Resources.Load<GameObject>($"Prefabs/{name}"), mousePos + Vector3.forward, transform.rotation);
            isHoldingChar = true;
        }
    }
}
