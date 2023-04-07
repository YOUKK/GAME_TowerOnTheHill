using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public void OnDrag(PointerEventData data)
    {
        gameObject.transform.position = data.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"current position is {Camera.main.ScreenToWorldPoint(eventData.position)}");
    }
}
