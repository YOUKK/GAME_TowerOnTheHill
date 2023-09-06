using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAreaScript : MonoBehaviour
{
    private bool isDetectedCharacter = false;
    public bool IsDetectedCharacter { get => isDetectedCharacter; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Character"))
        {
            isDetectedCharacter = true;
        }
    }
}
