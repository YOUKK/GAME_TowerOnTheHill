using UnityEngine;

public class MonsterCheck : MonoBehaviour
{
    [SerializeField]
    private Character mainCharacter;
    [SerializeField]
    private GameObject monster;

    private MonsterSpawner A;
    [SerializeField]
    private Vector2 localPos;

    public Vector2 LocalPos { get => localPos; set => localPos = value; }
    public GameObject Monster { get => monster; set => monster = value; }

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = transform.GetComponentInParent<Character>();
        A = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();
        //Range = LocalPos.x + (float)mainCharacter.Range;
    }

    private void Update()
    {
        //print((int)LocalPos.y);
        var T = A.GetLineMonstersInfo((int)LocalPos.y);
        if (T != null)
        {
            //print(T[0].transform.position.x);
            //print(mainCharacter.transform.position.x);
            //print((float)mainCharacter.Range);
            if (T[0].transform.position.x - mainCharacter.transform.position.x <= (float)mainCharacter.Range)
            {
                monster = T[0];
                mainCharacter.CheckMonster = true;
            }
            else
            {
                mainCharacter.CheckMonster = false;
            }
        }
        else
        {
            mainCharacter.CheckMonster = false;
        }
    }
}
