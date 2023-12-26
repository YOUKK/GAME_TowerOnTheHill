using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter2 : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        strength = int.Parse(dataManager.GetUpgradeDataDic()["PeaShooter2"].statIncrease[
                    dataManager.GetUpgradeDataDic()["PeaShooter2"].currentLevel].ToString());
    }

    public override void Attack()
    {
        anim.SetTrigger("canAttack");
        Invoke("attackDelaySet", 0.1f);
        if (!IsDragged && CheckMonster)
        {
            anim.SetTrigger("canAttack");
            Invoke("attackDelaySet", 0.1f);
        }
    }
    private void Update()
    {
        for (int i = 0; i < gameObject.transform.GetChild(1).childCount; i++)
        {
            if(!gameObject.transform.GetChild(1).GetChild(i).gameObject.activeSelf)
            {
                gameObject.transform.GetChild(1).GetChild(i).position = gameObject.transform.position;
            }
        }
    }
    void attackDelaySet()
    {
        projectile = projectiles.Dequeue();
        projectiles.Enqueue(projectile);
        activatedProj.Enqueue(projectile);
        projectileSecond = projectiles.Dequeue();
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
