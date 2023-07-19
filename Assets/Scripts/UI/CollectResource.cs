using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ���ҽ� ȹ��, ���� ���ҽ� �� UI�� �����ִ� �� ���� ��ũ��Ʈ
public class CollectResource : MonoBehaviour
{
    private int resource = 0;
    public int GetResource() { return resource; }

    [SerializeField]
    //private Text resourceText;
    private TMP_Text resourceText;

    void Start()
    {
        resource = 0;
        resourceText.text = "0";
    }

    void Update()
    {

    }

    // ���� �� �ڿ� ���
    public void EarnResource(int get) // get�� ���� �ڿ��� ��
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
