using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SelectedCanvas에서 선택된 캐릭터 버튼을 관리하는 스크립트
// SelectedCanvas에 붙음

public class SelectedCharacter : MonoBehaviour
{
    private int turn = 0; // 현재 채워져야 할 번호
    public int Turn { get { return turn; } set { turn = value;} }
    private List<GameObject> characterFrames = new List<GameObject>();
    public List<GameObject> CharacterFrames { get { return characterFrames; } }
    //private Dictionary<>

    void Start()
    {
        for (int i = 0; i < 8; i++) {
            characterFrames.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        
    }

    // 추가해야할 frame 오브젝트를 알려주는 함수
    public GameObject GetNextTurn()
	{
        return characterFrames[turn];
	}
}
