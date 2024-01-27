using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMushroom : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        strength = (int)DataManager.GetData.GetUpgradeDataDic()["Magician"].statIncrease[
            DataManager.GetData.GetUpgradeDataDic()["Magician"].currentLevel];
    }

    public override void Attack()
    {
        if (projectile == null)
            return;
        if (!IsDragged && CheckMonster)
        {
            anim.SetTrigger("canAttack");
            Invoke("attackDelaySet", 0.7f);
        }
    }
    void attackDelaySet()
    {
        projectile.SetActive(true);
        Invoke("Duration", attackDuration);
    }
    void Duration()
    {
        projectile.SetActive(false);
    }
}
