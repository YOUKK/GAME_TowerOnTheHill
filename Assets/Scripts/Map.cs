using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    void Update()
    {
        /*if(Input.GetMouseButtonUp(0))
        {
            Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawRay(MousePosition, Vector3.forward * 10.0f, Color.red, 3.0f);

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, Vector3.forward, 10.0f);
            if (hit)
            {
                hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }*/
    }
}
