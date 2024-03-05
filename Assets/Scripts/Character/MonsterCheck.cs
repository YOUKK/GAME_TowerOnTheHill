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

    [SerializeField]
    private Vector2 rangeSize;
    [SerializeField]
    private Vector2 rangePos;


    [SerializeField]
    private Collider2D[] allObject;
    [SerializeField]
    private Collider2D[] hitRangeMonster;

    public Vector2 LocalPos { get => localPos; set => localPos = value; }
    public GameObject Monster { get => monster; set => monster = value; }

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = transform.GetComponentInParent<Character>();

        rangePos = new Vector2(transform.position.x + mainCharacter.Range / 2, transform.position.y);
        rangeSize = new Vector2(mainCharacter.Range, 0.3f);
        // A = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();
        // Range = LocalPos.x + (float)mainCharacter.Range;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(rangePos, rangeSize);
    }

    private void Update()
    {
        allObject = Physics2D.OverlapBoxAll(rangePos, rangeSize, 0);

        hitRangeMonster = allObject;

        bool flag = false;

        for (int i = 0; i < hitRangeMonster.Length; i++)
        {
            if(hitRangeMonster[i].CompareTag("Enemy"))
            {
                if (mainCharacter.Type == CharacterType.Normal          &&
                    MonsterType.Aerial == hitRangeMonster[i].gameObject.GetComponent<Monster>().GetMonsterType())
                {
                    continue;
                }
                else if (mainCharacter.Type == CharacterType.UnTouch    &&
                    MonsterType.Unique == hitRangeMonster[i].gameObject.GetComponent<Monster>().GetMonsterType() &&
                    MonsterType.Aerial == hitRangeMonster[i].gameObject.GetComponent<Monster>().GetMonsterType())
                {
                    continue;
                }

                monster = hitRangeMonster[i].gameObject;
                flag = true;
                break;
            }
        }

        if(flag && hitRangeMonster.Length != 0)
        {
            mainCharacter.CheckMonster = true;
        }
        else
        {
            mainCharacter.CheckMonster = false;
        }
    }
}
