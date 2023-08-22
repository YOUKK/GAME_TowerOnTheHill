using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 플레이에 쓸 캐릭터를 고르는 기능 관련 스크립트
// CharacterButtonCanvas에 붙음

public class SelectCharacter : MonoBehaviour
{
    //private List<chaType> selectedCha = new List<chaType>(); // 선택된 캐릭터 리스트
    //public List<chaType> SelectedCha { get { return selectedCha; } }

    private List<GameObject> spriteCha = new List<GameObject>(); // 선택된 캐릭터의 스프라이트
    public List<GameObject> SpriteCha { get { return spriteCha; } }

    private List<GameObject> objectCha = new List<GameObject>(); // 선택된 캐릭터의 게임 오브젝트가 담기는 리스트
    public List<GameObject> ObjectCha { get { return objectCha; } }

    private List<int> chaPrice = new List<int>(); // 선택된 캐릭터의 가격들
    public List<int> ChaPrice { get { return chaPrice; } }

    private List<int> coolTime = new List<int>(); // 선택된 캐릭터의 쿨타임
    public List<int> CoolTime { get { return coolTime; } }

    void Start()
    {
        // 아래 4개가 순서대로 선택되었다고 가정
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
