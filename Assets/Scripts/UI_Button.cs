using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool _pressed = false;
    [SerializeField] GameObject character;
    GameObject iCharacter;

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        iCharacter = Instantiate(character, mousePosition + Vector3.forward, transform.rotation);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;

        Vector3 rayStart = new Vector3(iCharacter.transform.position.x,
            iCharacter.transform.position.y, -1);
        Debug.DrawRay(rayStart, Vector3.forward * 10.0f, Color.red, 3.0f);

        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector3.forward, 10.0f);
        if (hit)
        {
            hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            iCharacter.transform.position = hit.transform.position;
        }
    }

    void Update()
    {
        if (_pressed)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward;
            if (iCharacter != null)
                iCharacter.transform.position = mousePosition;
        }
    }
}
