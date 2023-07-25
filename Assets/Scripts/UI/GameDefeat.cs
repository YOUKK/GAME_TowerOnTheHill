using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDefeat : MonoBehaviour
{
    [SerializeField]
    private GameObject defeat;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
            defeat.SetActive(true);
		}
	}
}
