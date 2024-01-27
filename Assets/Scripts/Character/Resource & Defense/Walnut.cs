using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walnut : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        HealthPoint = (int)DataManager.GetData.GetUpgradeDataDic()["Guard"].statIncrease[
                                DataManager.GetData.GetUpgradeDataDic()["Guard"].currentLevel];
    }

    public override void Attack()
    {
        if (projectile == null)
            return;
    }
}
