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

    // 다른 일반 화면 터치시에도 Pickax 클릭한 게 해제되게 만들기


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
                if (hitSeat.transform.gameObject.GetComponent<Seat>().isCharacterOn) // seat위의 식물 삭제
                {
                    Debug.Log("hitSeat3 " + hitSeat);
                    Vector2 location = hitSeat.transform.gameObject.GetComponent<Seat>().location;
                    Map.GetInstance().RemoveCharacter(location);

                    hitSeat.transform.GetComponent<Seat>().isCharacterOn = false;
                    hitSeat.transform.GetComponent<Seat>().usable = true;

                    ClickButton(); // Piackax 기능 끄기
                }
            }

			if (hit)
			{
				if (hit.transform.CompareTag("Pickax")) // 버튼을 다시 누른 경우
                {
                    // nothing
                    // Button컴포넌트에서 ClickButton()을 실행하게 했으므로 여기선 아무 수행도 하지 않음
                }
            }

			if(isFlicker)
			{
                ClickButton(); // Piackax 기능 끄기
            }
		}
	}
}
