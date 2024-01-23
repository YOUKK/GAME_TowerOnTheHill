using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShow : MonoBehaviour
{
    [SerializeField]
    private Button continueButton;

    void Start()
    {
        continueButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("GamePlayScene", "LevelSelectScene"));
    }

    void Update()
    {
        
    }

    // 캐릭터 스프라이트, 이름, 설명 데이터 로드하는 코드 추가하기


}
