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

    // ĳ���� ��������Ʈ, �̸�, ���� ������ �ε��ϴ� �ڵ� �߰��ϱ�


}
