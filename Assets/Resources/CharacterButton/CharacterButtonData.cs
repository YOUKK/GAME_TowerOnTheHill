using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĳ���� ��ư�� ���̴� ��ũ���ͺ� ������Ʈ
// ĳ���� �رݽ� ĳ���� ���� UI���� ���δ�
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
