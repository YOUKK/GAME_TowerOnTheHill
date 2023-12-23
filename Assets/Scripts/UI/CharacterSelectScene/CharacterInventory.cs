using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InventoryCanvas�� �ִ� ĳ���� ��ư���� ��ü������ �����ϴ� ��ũ��Ʈ
// InventoryCanvas�� ����

public enum CharacterButtonList // InventoryCanvas�� �ִ� ĳ���� ��ư �������
{
    Sunflower,
    PeaShooter,
    Walnut,
    Bomb,
    IceShooter,
    flyCharacter,
    GasMushroom,
    Eater,
    Buffer,
    None, // InventoryCanvas�� ĳ���� ��ư�� ���� �� �ǹ�
}

public class CharacterInventory : MonoBehaviour
{
    // enum�� ���� ĳ���� ��ư ������Ʈ�� ���� ��ųʸ�
    public Dictionary<CharacterButtonList, GameObject> enumPerButtonDic = new Dictionary<CharacterButtonList, GameObject>();


    void Start()
    {

    }

    void Update()
    {
        
    }
}
