using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private TextMeshProUGUI text;
    private SelectedCharacter selectedCharacter;

    void Start()
    {
        text = transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
        selectedCharacter = GameObject.Find("SelectedCanvas").GetComponent<SelectedCharacter>();
    }

    void Update()
    {
        
    }

    // StartButton 클릭시 1. 캐릭터 선택 정보 json으로 저장됨 2. 게임플레이씬으로 전환
    public void ClickButton()
	{
        // json 파일 저장
        selectedCharacter.SaveButtonListToJson();

        // 씬 이동
        SceneManager.LoadScene("GamePlayScene");
        SoundManager.Instance.PlayBGM("Battle");
	}

    public void CanPressButton()
	{
        transform.GetComponent<Button>().interactable = true;
        text.color = new Color(1, 196/255f, 175/255f, 1);
	}

    public void CannotPressButton()
	{
        transform.GetComponent<Button>().interactable = false;
        text.color = new Color(1, 196 / 255f, 175 / 255f, 0.5f);
    }
}
