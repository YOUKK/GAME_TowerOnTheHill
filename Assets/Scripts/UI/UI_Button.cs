using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 캐릭터 버튼을 누르면 캐릭터가 복사된 뒤 설치되는 스크립트
// 캐릭터 쿨타임, 가격에 따라 설치 가능 여부 UI 표시
public class UI_Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _pressed = false;
    private GameObject character;
    private GameObject dragCharacter;
    private CollectResource menuCanvas;

    private float coolTime;
    private int price;
    private bool onCoolTime = false;
    private bool onPrice = false;

    private Image coolTimeImage;
    private GameObject priceImage;

    void Start()
    {
        menuCanvas = GameObject.Find("MenuCanvas").GetComponent<CollectResource>();
        character = transform.GetComponentInParent<SelectCharacter>().ObjectCha[transform.GetSiblingIndex()];
        coolTime = transform.GetComponentInParent<SelectCharacter>().CoolTime[transform.GetSiblingIndex()];
        price = transform.GetComponentInParent<SelectCharacter>().ChaPrice[transform.GetSiblingIndex()];
        coolTimeImage = transform.GetChild(2).GetComponent<Image>();
        priceImage = transform.GetChild(3).gameObject;
    }

    // 캐릭터 선택
    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPrice)
        {
            Debug.Log("Down");
            _pressed = true;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragCharacter = Instantiate(character, mousePosition + Vector3.forward, transform.rotation);
            dragCharacter.tag = "DragCharacter";
            menuCanvas.UseResource(price);
        }
    }

    // 맵에 캐릭터 설치
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_pressed)
        {
            Debug.Log("Up");
            _pressed = false;

            Vector3 rayStart = new Vector3(dragCharacter.transform.position.x, dragCharacter.transform.position.y, -1);
            Debug.DrawRay(rayStart, Vector3.forward * 10.0f, Color.red, 3.0f);

            int layerMask = 1 << LayerMask.NameToLayer("Seat");
            RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector3.forward, 10.0f, layerMask);
            if (hit)
            {
                if (hit.transform.CompareTag("Seat")) // Seat¿¡ ¿Ã·Á³õÀ½
                {
                    Vector2 location = hit.transform.gameObject.GetComponent<Seat>().location;

                    //dragCharacter.transform.position = hit.transform.position;
                    Map.GetInstance().PutCharacter(location, character);
                }
            }

            Destroy(dragCharacter.gameObject);
            StartCoroutine(CoolTimeColor());
        }
    }

    void Update()
    {
         /*
         if (_pressed)
         {
             Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward;
             if (dragCharacter != null)
             {
                 dragCharacter.transform.position = mousePosition;
             }
         }
         */

        if (_pressed)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward;
            if (dragCharacter != null)
            {
                dragCharacter.GetComponent<Character>().IsDragged = true;
                dragCharacter.transform.position = mousePosition;
            }
        }
        else
        {
            if (dragCharacter != null)
            {
                dragCharacter.GetComponent<Character>().IsDragged = false;
            }
        }

        PriceColor();
    }

    IEnumerator CoolTimeColor()
	{
        Debug.Log("쿨타임 시작");
        onCoolTime = true;

        float time = coolTime;
        Debug.Log(time);
        coolTimeImage.fillAmount = 1f;
        while (time > 0.1f)
        {
            Debug.Log(time);
            time -= Time.deltaTime;
            coolTimeImage.fillAmount = time / coolTime;

            yield return null;
        }

        coolTimeImage.fillAmount = 0f;
        yield return new WaitForSeconds(1f);
        onCoolTime = false;
        Debug.Log("쿨타임 끝");
    }

    private void PriceColor()
	{
        if(menuCanvas.GetResource() >= price && !onCoolTime) // 구매 가능
        {
            priceImage.SetActive(false);
            onPrice = true;
        }
        else  // 구매 불가능
        {
            priceImage.SetActive(true);
            onPrice = false;
        }
	}
}
