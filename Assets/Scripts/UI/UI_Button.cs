using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _pressed = false;
    private GameObject character;
    private GameObject dragCharacter;
    private CollectResource menuCanvas;

    private int price;
    private bool onPrice = false;

    void Start()
    {
        character = transform.GetComponentInParent<SelectCharacter>().ObjectCha[transform.GetSiblingIndex()];
        menuCanvas = GameObject.Find("MenuCanvas").GetComponent<CollectResource>();
        price = transform.GetComponentInParent<SelectCharacter>().ChaPrice[transform.GetSiblingIndex()];
    }

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
                if (hit.transform.CompareTag("Seat")) // Seat에 올려놓음
                {
                    Vector2 location = hit.transform.gameObject.GetComponent<Seat>().location;

                    //dragCharacter.transform.position = hit.transform.position;
                    Map.GetInstance().PutCharacter(location, character);
                }
            }

            Destroy(dragCharacter.gameObject);
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

        if (menuCanvas.GetResource() >= price)
		{
            gameObject.GetComponent<Image>().color = new Color(1f, 141/255f, 0, 1f);
            onPrice = true;
		}
		else
		{
            gameObject.GetComponent<Image>().color = new Color(1f, 141/255f, 0, 100/255f);
            onPrice = false;
        }
    }
}
