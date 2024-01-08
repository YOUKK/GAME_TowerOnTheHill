using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter2 : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        strength = int.Parse(DataManager.GetData.GetUpgradeDataDic()["PeaShooter2"].statIncrease[
                    DataManager.GetData.GetUpgradeDataDic()["PeaShooter2"].currentLevel].ToString());
    }

    public override void Attack()
    {
        if (!IsDragged && CheckMonster)
        {
            anim.SetTrigger("canAttack");
            Invoke("attackDelaySet", 0.1f);
        }
    }

    void attackDelaySet()
    {
        projectile = projectiles.Dequeue();
        // print(projectile.name);
        projectiles.Enqueue(projectile);
        activatedProj.Enqueue(projectile);

        projectileSecond = projectiles.Dequeue();
        // print(projectileSecond.name);
        projectiles.Enqueue(projectileSecond);
        activatedProj.Enqueue(projectileSecond);

        projectile.SetActive(true);
        projectileSecond.SetActive(true);

        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(ProjectileSpeed, 0) * 100);
        Invoke("attackDelaySet2", 0.15f);
    }
    void attackDelaySet2()
    {
        projectileSecond.GetComponent<Rigidbody2D>().AddForce(new Vector2(ProjectileSpeed, 0) * 100);
        Invoke("Duration", AttackDuration);
    }
    void Duration()
    {
        GameObject T = activatedProj.Dequeue();
        T.SetActive(false);
        T = activatedProj.Dequeue();
        T.SetActive(false);
    }
}
