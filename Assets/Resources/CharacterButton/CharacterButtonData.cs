using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 버튼에 쓰이는 스크립터블 오브젝트
// 캐릭터 해금시 캐릭터 설명 UI에도 쓰인다
[CreateAssetMenu(fileName = "CharacterButton", menuName = "ScriptableObject/CharacterButton", order = 0)]
public class CharacterButtonData : ScriptableObject
{
	[SerializeField]
	private GameObject sprite;
	public GameObject Sprite { get { return sprite; } }
	[SerializeField]
	private GameObject characterObject;
	public GameObject CharacterObject { get { return characterObject; } }
	[SerializeField]
	private int coolTime;
	public int CoolTime { get { return coolTime; } }
	[SerializeField]
	private int price;
	public int Price { get { return price; } }
	[SerializeField]
	private string description;
	public string Description { get { return description; } }
}
