using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eater : Character
{
    [SerializeField]
    private int pSec = 0;
    [SerializeField]
    private bool eating = false;
    [SerializeField]
    private float A = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        AttackDuration = (int)DataManager.GetData.GetUpgradeDataDic()["Eater"].statIncrease[DataManager.GetData.GetUpgradeDataDic()["Eater"].currentLevel];
    }
    // Update is called once per frame
    public override void Attack()
    {
        if(!eating)
        {
            if (!IsDragged && CheckMonster)
            {
                eating = true;
                anim.SetTrigger("canAttack");
                Invoke("attackDelaySet", 1.3f);
            }
            A = GamePlayManagers.TimeM.Sec + AttackDuration;
            return;
        }
        if(pSec <= A)
        {
            pSec = GamePlayManagers.TimeM.Sec;
            return;
        }
        else
        {
            eating = false;
            pSec = 0;
        }
    }
    void attackDelaySet()
    {
        if (Monster != null)
        {
            Monster.GetComponent<Monster>().Hit(1, AttackType.DEAD);
            Monster.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
