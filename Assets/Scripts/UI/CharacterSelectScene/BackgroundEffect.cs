using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEffect : MonoBehaviour
{
    private Vector3 startPosition;
    private short flag = 1; // Moving x position direction
    public float distance;
    public float moveSpeed;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - startPosition.x) >= distance)
            flag *= -1;

        transform.Translate(moveSpeed * Time.deltaTime * flag, 0, 0);
    }
}
