using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunner : Character
{
    protected override void Start()
    {
        base.Start();
        AttackDuration = (int)DataManager.GetData.GetUpgradeDataDic()["Stunner"].statIncrease[DataManager.GetData.GetUpgradeDataDic()["Stunner"].currentLevel];
    }
    public override void Attack()
    {
        if (!IsDragged && CheckMonster)
        {
            anim.SetTrigger("canAttack");
            Invoke("attackDelaySet", 0.05f);
        }
    }
    void attackDelaySet()
    {
        projectile = projectiles.Dequeue();
        projectiles.Enqueue(projectile);
        activatedProj.Enqueue(projectile);
        projectile.SetActive(true);

        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(ProjectileSpeed, 0) * 100);
        Invoke("Duration", AttackDuration);
    }
    void Duration()
    {
        activatedProj.Peek().transform.position = gameObject.transform.position;
        GameObject T = activatedProj.Dequeue();
        T.SetActive(false);
    }
}
