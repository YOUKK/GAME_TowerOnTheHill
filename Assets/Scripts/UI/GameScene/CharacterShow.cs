using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class CharacterShow : MonoBehaviour
{
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI description;
    private CharacterButtonData characterData;

    void Start()
    {
        continueButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("GamePlayScene", "LevelSelectScene"));

        GamePlayManagers.Instance.LoadSelectPhaseStageFromJson();
        LoadCharacterInfo();

        image.sprite = characterData.Sprite.GetComponent<SpriteRenderer>().sprite;
        name.text = characterData.Sprite.name;
        description.text = characterData.Description;
        description.text = description.text.Replace("\\n", "\n");
    }

    void Update()
    {
        
    }

    // 캐릭터 스프라이트, 이름, 설명 데이터 로드하는 코드 추가하기
    private void LoadCharacterInfo()
	{
        int phase = GamePlayManagers.Instance.selectPS.phase;
        int stage = GamePlayManagers.Instance.selectPS.stage;

        if(phase == 1 && stage == 1)
		{
            characterData = Resources.Load<CharacterButtonData>("CharacterButton/WalnutButton");
		}
        else if(phase == 1 && stage == 2)
		{
            characterData = Resources.Load<CharacterButtonData>("CharacterButton/BombButton");
        }
        else if(phase == 1 && stage == 3)
		{
            characterData = Resources.Load<CharacterButtonData>("CharacterButton/IceShooterButton");
        }
        else if(phase == 1 & stage == 5)
		{
            characterData = Resources.Load<CharacterButtonData>("CharacterButton/AerialShooterButton");
        }
        else if(phase == 2 && stage == 1)
		{
            characterData = Resources.Load<CharacterButtonData>("CharacterButton/GasMushroomButton");
        }
        else if(phase == 2 && stage == 2)
		{
            characterData = Resources.Load<CharacterButtonData>("CharacterButton/EaterButton");
        }
        else if(phase == 2 && stage == 3)
		{
            characterData = Resources.Load<CharacterButtonData>("CharacterButton/BufferButton");
        }
        else if (phase == 2 && stage == 5)
        {
            characterData = Resources.Load<CharacterButtonData>("CharacterButton/StunnerButton");
        }
        else if (phase == 3 && stage == 1)
        {
            characterData = Resources.Load<CharacterButtonData>("CharacterButton/PeaShooter2Button");
        }
        else if (phase == 3 && stage == 2)
        {
            characterData = Resources.Load<CharacterButtonData>("CharacterButton/MadmanButton");
        }
    }

}
