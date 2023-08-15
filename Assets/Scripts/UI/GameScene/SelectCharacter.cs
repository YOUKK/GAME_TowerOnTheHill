using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �÷��̿� �� ĳ���͸� ������ ��� ���� ��ũ��Ʈ
public class SelectCharacter : MonoBehaviour
{
    //private List<chaType> selectedCha = new List<chaType>(); // ���õ� ĳ���� ����Ʈ
    //public List<chaType> SelectedCha { get { return selectedCha; } }

    private List<GameObject> spriteCha = new List<GameObject>(); // ���õ� ĳ������ ��������Ʈ
    public List<GameObject> SpriteCha { get { return spriteCha; } }

    private List<GameObject> objectCha = new List<GameObject>(); // ���õ� ĳ������ ���� ������Ʈ�� ���� ����Ʈ
    public List<GameObject> ObjectCha { get { return objectCha; } }

    private List<int> chaPrice = new List<int>(); // ���õ� ĳ������ ���ݵ�
    public List<int> ChaPrice { get { return chaPrice; } }

    private List<int> coolTime = new List<int>(); // ���õ� ĳ������ ��Ÿ��
    public List<int> CoolTime { get { return coolTime; } }

    void Start()
    {
        // �Ʒ� 4���� ������� ���õǾ��ٰ� ����
        //selectedCha.Add(chaType.Sun);
        //selectedCha.Add(chaType.PeaSh);
        //selectedCha.Add(chaType.Wall);
        //selectedCha.Add(chaType.GasMu);

        spriteCha.Add(Resources.Load<GameObject>("Prefabs/Sprite/SunFlower"));
        spriteCha.Add(Resources.Load<GameObject>("Prefabs/Sprite/PeaShooter"));
        spriteCha.Add(Resources.Load<GameObject>("Prefabs/Sprite/Walnut"));
        spriteCha.Add(Resources.Load<GameObject>("Prefabs/Sprite/WideArea"));

        objectCha.Add(Resources.Load<GameObject>("Prefabs/Character/SunFlower"));
        objectCha.Add(Resources.Load<GameObject>("Prefabs/Character/PeaShooter"));
        objectCha.Add(Resources.Load<GameObject>("Prefabs/Character/Walnut"));
        objectCha.Add(Resources.Load<GameObject>("Prefabs/Character/GasMushroom"));

        chaPrice.Add(50);
        chaPrice.Add(100);
        chaPrice.Add(50);
        chaPrice.Add(75);

        coolTime.Add(7);
        coolTime.Add(7);
        coolTime.Add(30);
        coolTime.Add(7);
    }

    void Update()
    {
        
    }
}