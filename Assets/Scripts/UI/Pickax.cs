using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickax : MonoBehaviour
{
    private bool isFlicker;

    void Start()
    {
        isFlicker = false;
    }

    void Update()
    {
        if (isFlicker)
            DeleteCharac();
    }

    public void ClickButton()
	{
        isFlicker = !isFlicker;
        gameObject.GetComponent<Animation>().enabled = isFlicker;

        if (!isFlicker)
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
	}

    // 다른 일반 화면 터치시에도 Pickax 클릭한 게 해제되게 만들기


    private void DeleteCharac()
	{
		if (Input.GetMouseButtonDown(0))
		{
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z));
            Debug.DrawRay(mousePoint, Vector3.forward * 10.0f, Color.red, 3.0f);

            int layer = 1 << LayerMask.NameToLayer("Seat");
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector3.forward, 10.0f, layer);
			if (hit)
			{
                if (hit.transform.gameObject.GetComponent<Seat>().isCharacterOn)
                {
                    Vector2 location = hit.transform.gameObject.GetComponent<Seat>().location;
                    Map.GetInstance().RemoveCharacter(location);

                    ClickButton(); // Piackax 기능 끄기
                }
			}
		}
	}
}
