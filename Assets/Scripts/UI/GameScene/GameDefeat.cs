using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// StartEndUI 오브젝트 -> 자식 Endline오브젝트에 붙는다. 
public class GameDefeat : MonoBehaviour
{
	public TextMeshProUGUI coinText;
	public Button nextButton;
	public Button restartButton;

	private void OnEnable()
	{
		restartButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("GamePlayScene", "CharacterSelectScene"));
		nextButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("GamePlayScene", "LevelSelectScene"));

		coinText.text = GamePlayManagers.Instance.GetEarnedCoin.ToString();

		SoundManager.Instance.PlayEffect("Lose");
	}
}
