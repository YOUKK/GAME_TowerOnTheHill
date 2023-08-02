using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickax : MonoBehaviour
{
    private bool isFlicker; // ��ư Ȱ��ȭ�Ǹ� true, ��Ȱ��ȭ�� false

    void Start()
    {
        isFlicker = false;
    }

    void Update()
    {
        if (isFlicker)
            DeleteCharac();
    }

    // ��ư Ȱ��ȭ <-> ��Ȱ��ȭ �ٲٴ� �Լ�
    public void ClickButton()
	{
        isFlicker = !isFlicker;
        gameObject.GetComponent<Animation>().enabled = isFlicker;

        if (!isFlicker)
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
	}

    private void DeleteCharac()
	{
		if (Input.GetMouseButtonDown(0))
		{
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z));
            //Debug.DrawRay(mousePoint, Vector3.forward * 10.0f, Color.red, 3.0f);

            RaycastHit2D[] hitSeat = Physics2D.RaycastAll(mousePoint, Vector3.forward, 10.0f);

            bool flag = false;

            for (int i = 0; i < hitSeat.Length; i++) {
                if (hitSeat[i].transform.gameObject.layer == LayerMask.NameToLayer("Seat"))
                {
                    if (hitSeat[i].transform.gameObject.GetComponent<Seat>().isCharacterOn) // seat���� �Ĺ� ����
                    {
                        Vector2 location = hitSeat[i].transform.gameObject.GetComponent<Seat>().location;
                        Map.GetInstance().RemoveCharacter(location);

                        //hitSeat[i].transform.GetComponent<Seat>().isCharacterOn = false;
                        //hitSeat[i].transform.GetComponent<Seat>().usable = true;

                        ClickButton(); // Piackax ��� ����
                        break;
                    }
                }
                else if (hitSeat[i].transform.CompareTag("Pickax"))
				{
                    flag = true; // button������Ʈ�� ������ ClickButton()�� ��ġ�� �ʰ� flag�� üũ
                    break;
				}
            }

			if (isFlicker && !flag) // ĳ���� ���� & Pickax��ư �ٽ� ������ ����� ��� ����
			{
                ClickButton();
			}
		}
	}
}
