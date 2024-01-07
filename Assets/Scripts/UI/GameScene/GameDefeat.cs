using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// StartEndUI ������Ʈ -> �ڽ� Endline������Ʈ�� �ٴ´�. 
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
