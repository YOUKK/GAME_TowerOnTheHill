using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectResource : MonoBehaviour
{
    static private int resource;
    public int GetResource() { return resource; }

    [SerializeField]
    private Text resourceText;

    void Start()
    {
        resourceText.text = "0";
    }

    void Update()
    {

    }

    // 게임 중 자원 얻기
    public void GetResource(int get) // num은 얻은 자원의 양
	{
        resource += get;
        ChangeText();
    }

    // 캐릭터 구매하기
    public void UseResource(int use) // price는 캐릭터의 자원소모량
	{
        resource -= use;
        ChangeText();
    }

    private void ChangeText()
	{
        resourceText.text = resource.ToString();
	}
}
