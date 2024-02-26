using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    private Character mainCharacter;

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = transform.GetComponentInParent<GasMushroom>();
        transform.position = new Vector2(2.5f,
                                         transform.position.y);
        transform.localScale = new Vector2(8.5f, 4);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && collision.gameObject.GetComponent<Monster>().GetMonsterType() != MonsterType.Aerial)
        {
            collision.gameObject.GetComponent<Monster>().Hit((int)mainCharacter.Strength);
        }
    }
}