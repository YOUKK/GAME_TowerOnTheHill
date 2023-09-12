using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        anim.SetTrigger("canAttack");
        Invoke("attackDelaySet", 0.7f);
    }
    void attackDelaySet()
    {
        projectiles.Clear();

        projectile = gameObject.transform.Find("Projectiles").transform.GetChild(Random.Range(0, 3)).gameObject;
        projectiles.Enqueue(projectile);
        activatedProj.Enqueue(projectile);
        projectile.SetActive(true);
        Invoke("Duration", attackDuration);
    }
    void Duration()
    {
        GameObject T = activatedProj.Dequeue();
        T.SetActive(false);
    }
}
