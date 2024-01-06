using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShooter : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        strength = int.Parse(DataManager.GetData.GetUpgradeDataDic()["IceShooter"].statIncrease[
                    DataManager.GetData.GetUpgradeDataDic()["IceShooter"].currentLevel].ToString());
    }

    public override void Attack()
    {
        if (!IsDragged && CheckMonster)
        {
            anim.SetTrigger("canAttack");
            Invoke("attackDelaySet", 0.3f);
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
