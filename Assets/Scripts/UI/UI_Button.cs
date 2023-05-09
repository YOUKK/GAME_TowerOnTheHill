using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool _pressed = false;
    [SerializeField] GameObject character;
    GameObject dragCharacter;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        _pressed = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragCharacter = Instantiate(character, mousePosition + Vector3.forward, transform.rotation);
    }

    public void OnPointerUp(PointerEventData eventData)
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

    void Update()
    {
        if (_pressed)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward;
            if (dragCharacter != null)
                dragCharacter.transform.position = mousePosition;
        }
    }
}
