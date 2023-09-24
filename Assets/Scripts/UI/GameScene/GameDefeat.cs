using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// StartEndUI 오브젝트 -> 자식 Endline오브젝트에 붙는다. 
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
