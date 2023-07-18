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

    // �ٸ� �Ϲ� ȭ�� ��ġ�ÿ��� Pickax Ŭ���� �� �����ǰ� �����


    private void DeleteCharac()
	{
		if (Input.GetMouseButtonDown(0))
		{
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z));
            //Debug.DrawRay(mousePoint, Vector3.forward * 10.0f, Color.red, 3.0f);

            RaycastHit2D hitSeat = Physics2D.Raycast(mousePoint, Vector3.forward, 10.0f, LayerMask.NameToLayer("Seat"));
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector3.forward, 10.0f);

			if (hitSeat)
			{
                Debug.Log("hitSeat " + hitSeat);
                Debug.Log("hitSeat2 " + hitSeat.transform.gameObject.GetComponent<Seat>().isCharacterOn);
                if (hitSeat.transform.gameObject.GetComponent<Seat>().isCharacterOn) // seat���� �Ĺ� ����
                {
                    Debug.Log("hitSeat3 " + hitSeat);
                    Vector2 location = hitSeat.transform.gameObject.GetComponent<Seat>().location;
                    Map.GetInstance().RemoveCharacter(location);

                    hitSeat.transform.GetComponent<Seat>().isCharacterOn = false;
                    hitSeat.transform.GetComponent<Seat>().usable = true;

                    ClickButton(); // Piackax ��� ����
                }
            }

			if (hit)
			{
				if (hit.transform.CompareTag("Pickax")) // ��ư�� �ٽ� ���� ���
                {
                    // nothing
                    // Button������Ʈ���� ClickButton()�� �����ϰ� �����Ƿ� ���⼱ �ƹ� ���൵ ���� ����
                }
            }

			if(isFlicker)
			{
                ClickButton(); // Piackax ��� ����
            }
		}
	}
}
