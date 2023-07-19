using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PrePlacement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
	{
        if (Managers.MouseInputM.IsDrag)
        {
            if (transform.GetComponent<Seat>().usable)
                transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.7f);
        }
	}

    public void OnPointerExit(PointerEventData eventData)
	{
        transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }
}
