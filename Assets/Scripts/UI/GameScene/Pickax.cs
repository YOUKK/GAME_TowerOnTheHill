using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickax : MonoBehaviour
{
    enum ItemName { HAMMER, AX }

    private bool isFlicker; // 버튼 활성화되면 true, 비활성화면 false
    [SerializeField]
    private ItemName itemName;
    private RaycastHit2D[] hits;

    void Start()
    {
        isFlicker = false;
    }

    void Update()
    {
        if (isFlicker)
        {
            FindClickedObjects();

            if (hits != null)
            {
                if (itemName == ItemName.AX)
                    DeleteCharac();
                else if (itemName == ItemName.HAMMER)
                    KillMonster();

                hits = null;
            }
        }
    }

    // 버튼 활성화 <-> 비활성화 바꾸는 함수
    public void ItemEnableButton()
	{
        isFlicker = !isFlicker;
        gameObject.GetComponent<Animation>().enabled = isFlicker;

        if (!isFlicker)
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
	}

    private void FindClickedObjects()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z));
            //Debug.DrawRay(mousePoint, Vector3.forward * 10.0f, Color.red, 3.0f);

            hits = Physics2D.RaycastAll(mousePoint, Vector3.forward, 10.0f);
        }
    }

    private void KillMonster()
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.CompareTag("Enemy"))
            {
                hits[i].transform.gameObject.GetComponent<Monster>().Hit(100000);
                
                ShopData shopData = DataManager.GetData.GetShopData();
                shopData.hasHammer = false;
                DataManager.GetData.SaveShopData(shopData);
                transform.parent.gameObject.SetActive(false);
                break;
            }
        }
        ItemEnableButton();
    }

    private void DeleteCharac()
    {
        bool flag = false;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.layer == LayerMask.NameToLayer("Seat"))
            {
                if (hits[i].transform.gameObject.GetComponent<Seat>().isCharacterOn) // seat위의 식물 삭제
                {
                    Vector2 location = hits[i].transform.gameObject.GetComponent<Seat>().location;
                    Map.GetInstance().RemoveCharacter(location);

                    ItemEnableButton(); // Piackax 기능 끄기
                    break;
                }
            }
            else if (hits[i].transform.CompareTag("Pickax"))
            {
                flag = true; // button컴포넌트로 눌리는 ClickButton()과 겹치지 않게 flag로 체크
                break;
            }
        }

        if (isFlicker && !flag) // 캐릭터 삭제 & Pickax버튼 다시 누르기 빼고는 기능 해제
        {
            ItemEnableButton();
        }
    }
}
