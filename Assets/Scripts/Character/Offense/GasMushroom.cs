using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMushroom : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        if (projectile == null)
            return;
        if (!IsDragged && CheckMonster)
        {
            projectile.SetActive(true);
            Invoke("Duration", attackDuration);
        }
    }
    void Duration()
    {
        projectile.SetActive(false);
    }
}
