using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartButton : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        
    }

    public void CanPressButton()
	{
        transform.GetComponent<Button>().interactable = true;
        text.color = new Color(1, 196/255f, 175/255f, 1);
	}

    public void CannotPressButton()
	{
        transform.GetComponent<Button>().interactable = false;
        text.color = new Color(1, 196 / 255f, 175 / 255f, 0.5f);
    }
}
