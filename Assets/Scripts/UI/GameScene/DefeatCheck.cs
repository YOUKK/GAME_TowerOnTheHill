using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatCheck : MonoBehaviour
{
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
            GamePlayManagers.Instance.Defeat();

            // 패배 판정 후 또 판정이 일어나지 않게 비활성화
            gameObject.SetActive(false);
		}
	}
}
