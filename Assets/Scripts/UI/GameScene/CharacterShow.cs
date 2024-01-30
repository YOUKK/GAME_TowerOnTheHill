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
            characterData = AssetDatabase.LoadAssetAtPath<CharacterButtonData>("Assets/Scripts/UI/Button/WalnutButton.asset");
		}
        else if(phase == 1 && stage == 2)
		{
            characterData = AssetDatabase.LoadAssetAtPath<CharacterButtonData>("Assets/Scripts/UI/Button/BombButton.asset");
        }
        else if(phase == 1 && stage == 3)
		{
            characterData = AssetDatabase.LoadAssetAtPath<CharacterButtonData>("Assets/Scripts/UI/Button/IceShooterButton.asset");
        }
        else if(phase == 1 & stage == 5)
		{
            characterData = AssetDatabase.LoadAssetAtPath<CharacterButtonData>("Assets/Scripts/UI/Button/AerialShooterButton.asset");
        }
        else if(phase == 2 && stage == 1)
		{
            characterData = AssetDatabase.LoadAssetAtPath<CharacterButtonData>("Assets/Scripts/UI/Button/GasMushroomButton.asset");
        }
        else if(phase == 2 && stage == 2)
		{
            characterData = AssetDatabase.LoadAssetAtPath<CharacterButtonData>("Assets/Scripts/UI/Button/EaterButton.asset");
        }
        else if(phase == 2 && stage == 3)
		{
            characterData = AssetDatabase.LoadAssetAtPath<CharacterButtonData>("Assets/Scripts/UI/Button/BufferButton.asset");
        }
        else if (phase == 2 && stage == 5)
        {
            characterData = AssetDatabase.LoadAssetAtPath<CharacterButtonData>("Assets/Scripts/UI/Button/StunnerButton.asset");
        }
        else if (phase == 3 && stage == 1)
        {
            characterData = AssetDatabase.LoadAssetAtPath<CharacterButtonData>("Assets/Scripts/UI/Button/PeaShooter2Button.asset");
        }
        else if (phase == 3 && stage == 2)
        {
            characterData = AssetDatabase.LoadAssetAtPath<CharacterButtonData>("Assets/Scripts/UI/Button/MadmanButton.asset");
        }
    }

}
