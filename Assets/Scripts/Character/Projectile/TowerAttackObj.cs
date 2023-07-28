using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackObj : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float distance;
    private Vector2 initalLocation;

    void Start()
    {
        initalLocation = transform.position;
    }

    private void OnEnable()
    {
        
    }

    void Update()
    {
        if (initalLocation.x + distance > transform.position.x)
        {
            transform.Translate(new Vector3(transform.position.x + speed * Time.deltaTime,
                transform.position.y, transform.position.z));
        }
        else
        {
            transform.position = initalLocation;
            gameObject.SetActive(false);
        }
    }
}
