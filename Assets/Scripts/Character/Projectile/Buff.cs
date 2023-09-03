using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        switch((int)gameObject.name[gameObject.name.Length - 1])
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
