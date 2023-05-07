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

    // ���� �� �ڿ� ���
    public void GetResource(int get) // num�� ���� �ڿ��� ��
	{
        resource += get;
        ChangeText();
    }

    // ĳ���� �����ϱ�
    public void UseResource(int use) // price�� ĳ������ �ڿ��Ҹ�
	{
        resource -= use;
        ChangeText();
    }

    private void ChangeText()
	{
        resourceText.text = resource.ToString();
	}
}
