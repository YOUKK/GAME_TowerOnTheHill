using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderLayerSort : MonoBehaviour
{
    private void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder;
    }
}
