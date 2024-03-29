using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialShooter : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        strength = (int)DataManager.GetData.GetUpgradeDataDic()["Aerial Shooter"].statIncrease[
            DataManager.GetData.GetUpgradeDataDic()["Aerial Shooter"].currentLevel];
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
