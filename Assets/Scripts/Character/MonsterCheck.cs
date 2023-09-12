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
        var T = A.GetLineMonstersInfo((int)LocalPos.y);
        if (T != null)
        {
            if (T[0].transform.position.x - mainCharacter.transform.position.x <= (float)mainCharacter.Range)
            {
                monster = T[0];
                mainCharacter.CheckMonster = true;
                if (mainCharacter.status.type == CharacterName.Eater)
                {
                    Invoke("EatingLate", 0.3f);
                    Invoke("Eating", mainCharacter.AttackDuration);
                }
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

    void EatingLate()
    { 
        mainCharacter.GetComponent<Eater>().CanAttack = false;
    }

    void Eating()
    {
        mainCharacter.GetComponent<Eater>().CanAttack = true;
    }
}
