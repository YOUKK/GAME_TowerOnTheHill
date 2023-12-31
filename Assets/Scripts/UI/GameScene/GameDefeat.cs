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
		restartButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("CharacterSelectScene"));
		nextButton.onClick.AddListener(() => GameManager.GetInstance.MoveScene("LevelSelectScene"));

		coinText.text = GamePlayManagers.Instance.GetEarnedCoin.ToString();
	}
}
