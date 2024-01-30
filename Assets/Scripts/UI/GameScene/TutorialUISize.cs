using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUISize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == "Frame")
            GetComponent<RectTransform>().sizeDelta = new Vector2(GameObject.Find("MenuCanvas").GetComponent<RectTransform>().sizeDelta.x,
                                                                  GetComponent<RectTransform>().sizeDelta.y);
        else if(gameObject.name == "Text")
            GetComponent<RectTransform>().sizeDelta = new Vector2(GameObject.Find("MenuCanvas").GetComponent<RectTransform>().sizeDelta.x - 170,
                                                                  GetComponent<RectTransform>().sizeDelta.y);
    }
}
