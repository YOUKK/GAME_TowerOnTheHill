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

    // StartButton Ŭ���� 1. ĳ���� ���� ���� json���� ����� 2. �����÷��̾����� ��ȯ
    public void ClickButton()
	{
        // json ���� ����
        selectedCharacter.SaveButtonListToJson();

        // �� �̵�
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
