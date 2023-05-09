using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectResource : MonoBehaviour
{
    private int resource;
    public int GetResource() { return resource; }

    [SerializeField]
    private Text resourceText;

    void Start()
    {
        resource = 0;
        resourceText.text = "0";
    }

    void Update()
    {

    }

    // ���� �� �ڿ� ���
    public void GetResource(int get) // get�� ���� �ڿ��� ��
	{
        resource += get;
        ChangeText();
    }

    // ĳ���� �����ϱ�
    public void UseResource(int use) // use�� ĳ������ �ڿ��Ҹ�
	{
        resource -= use;
        ChangeText();
    }

    private void ChangeText()
	{
        resourceText.text = resource.ToString();
	}
}
