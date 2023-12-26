using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walnut : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        HealthPoint = int.Parse(dataManager.GetUpgradeDataDic()["Walnut"].statIncrease[
                                dataManager.GetUpgradeDataDic()["Walnut"].currentLevel].ToString());
    }

    public override void Attack()
    {
        if (projectile == null)
            return;
    }
}
