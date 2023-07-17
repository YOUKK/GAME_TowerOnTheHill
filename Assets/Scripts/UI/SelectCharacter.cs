using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 플레이에 쓸 캐릭터를 고르는 기능 관련 스크립트
public class SelectCharacter : MonoBehaviour
{
    //private List<chaType> selectedCha = new List<chaType>(); // 선택된 캐릭터 리스트
    //public List<chaType> SelectedCha { get { return selectedCha; } }

    private List<GameObject> objectCha = new List<GameObject>(); // 선택된 캐릭터의 게임 오브젝트가 담기는 리스트
    public List<GameObject> ObjectCha { get { return objectCha; } }

    private List<int> chaPrice = new List<int>();
    public List<int> ChaPrice { get { return chaPrice; } }

    private List<int> coolTime = new List<int>();
    public List<int> CoolTime { get { return coolTime; } }

    void Start()
    {
        // 아래 4개가 순서대로 선택되었다고 가정
        //selectedCha.Add(chaType.Sun);
        //selectedCha.Add(chaType.PeaSh);
        //selectedCha.Add(chaType.Wall);
        //selectedCha.Add(chaType.GasMu);

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
