using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    private GasMushroom mainCharacter;

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = transform.GetComponentInParent<GasMushroom>();
        transform.position = new Vector2(transform.position.x + ((1 + mainCharacter.Range) / 2),
                                         transform.position.y);
        transform.localScale = new Vector2(mainCharacter.Range, transform.localScale.y);
    }
}
