using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �÷��̿� �� ĳ���͸� ���� ��� ���� ��ũ��Ʈ
public class SelectCharacter : MonoBehaviour
{
    private List<chaType> selectedCha = new List<chaType>(); // ���õ� ĳ���� ����Ʈ
    public List<chaType> SelectedCha { get { return selectedCha; } }
    private List<GameObject> objectCha = new List<GameObject>(); // ���õ� ĳ������ ���� ������Ʈ�� ���� ����Ʈ
    public List<GameObject> ObjectCha { get { return objectCha; } }

    void Start()
    {
        // �Ʒ� 4���� ������� ���õǾ��ٰ� ����
        selectedCha.Add(chaType.Sun);
        selectedCha.Add(chaType.Wall);
        selectedCha.Add(chaType.PeaSh);
        selectedCha.Add(chaType.GasMu);

        objectCha.Add(Resources.Load<GameObject>("Prefabs/Character/SunFlower"));
        objectCha.Add(Resources.Load<GameObject>("Prefabs/Character/Walnut"));
        objectCha.Add(Resources.Load<GameObject>("Prefabs/Character/PeaShooter"));
        objectCha.Add(Resources.Load<GameObject>("Prefabs/Character/GasMushroom"));
    }

    void Update()
    {
        
    }
}
