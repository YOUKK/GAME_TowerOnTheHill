using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InventoryCanvas에 있는 캐릭터 버튼들을 전체적으로 관리하는 스크립트
// InventoryCanvas에 붙음

public enum CharacterButtonList // InventoryCanvas에 있는 캐릭터 버튼 순서대로
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
    None, // InventoryCanvas에 캐릭터 버튼이 없는 걸 의미
}

public class CharacterInventory : MonoBehaviour
{
    // enum에 따른 캐릭터 버튼 오브젝트를 가진 딕셔너리
    public Dictionary<CharacterButtonList, GameObject> enumPerButtonDic = new Dictionary<CharacterButtonList, GameObject>();


    void Start()
    {

    }

    void Update()
    {
        
    }
}
