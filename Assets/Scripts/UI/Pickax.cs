using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickax : MonoBehaviour
{
    private bool isFlicker; // 버튼 활성화되면 true, 비활성화면 false

    void Start()
    {
        isFlicker = false;
    }

    void Update()
    {
        if (isFlicker)
            DeleteCharac();
    }

    // 버튼 활성화 <-> 비활성화 바꾸는 함수
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
                    if (hitSeat[i].transform.gameObject.GetComponent<Seat>().isCharacterOn) // seat위의 식물 삭제
                    {
                        Vector2 location = hitSeat[i].transform.gameObject.GetComponent<Seat>().location;
                        Map.GetInstance().RemoveCharacter(location);

                        //hitSeat[i].transform.GetComponent<Seat>().isCharacterOn = false;
                        //hitSeat[i].transform.GetComponent<Seat>().usable = true;

                        ClickButton(); // Piackax 기능 끄기
                        break;
                    }
                }
                else if (hitSeat[i].transform.CompareTag("Pickax"))
				{
                    flag = true; // button컴포넌트로 눌리는 ClickButton()과 겹치지 않게 flag로 체크
                    break;
				}
            }

			if (isFlicker && !flag) // 캐릭터 삭제 & Pickax버튼 다시 누르기 빼고는 기능 해제
			{
                ClickButton();
			}
		}
	}
}
