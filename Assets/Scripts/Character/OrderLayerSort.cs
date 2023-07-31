using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderLayerSort : MonoBehaviour
{
    private void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = (-1) * (int)transform.position.y;
        Debug.Log((int)transform.position.y);
    }
}
