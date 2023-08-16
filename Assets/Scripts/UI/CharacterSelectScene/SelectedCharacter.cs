using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SelectedCanvas���� ���õ� ĳ���� ��ư�� �����ϴ� ��ũ��Ʈ
// SelectedCanvas�� ����

public class SelectedCharacter : MonoBehaviour
{
    private int turn = 0; // ���� ä������ �� ��ȣ
    public int Turn { get { return turn; } set { turn = value;} }
    private List<GameObject> characterFrames = new List<GameObject>();
    public List<GameObject> CharacterFrames { get { return characterFrames; } }
    //private Dictionary<>

    void Start()
    {
        for (int i = 0; i < 8; i++) {
            characterFrames.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        
    }

    // �߰��ؾ��� frame ������Ʈ�� �˷��ִ� �Լ�
    public GameObject GetNextTurn()
	{
        return characterFrames[turn];
	}
}
