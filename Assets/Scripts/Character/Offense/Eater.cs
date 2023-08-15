using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eater : Character
{
    [SerializeField]
    private bool canAttack;
    // Start is called before the first frame update
    protected override void Start()
    {
        canAttack = true;
        base.Start();
    }

    public bool CanAttack { get => canAttack; set => canAttack = value; }
    // Update is called once per frame
    public override void Attack()
    {
        if (!IsDragged && canAttack)
        {
            anim.SetTrigger("canAttack");
            //canAttack = false;
            Invoke("attackDelaySet", 1.7f);
        }
    }
    void attackDelaySet()
    {
        gameObject.GetComponentInChildren<MonsterCheck>().Monster.SetActive(false);
        Invoke("Duration", AttackDuration);
    }
    void Duration()
    {
        if (healthPoint < 0)
        {
            gameObject.GetComponentInChildren<MonsterCheck>().Monster.SetActive(true);
        }
        else
        {
            Destroy(gameObject.GetComponentInChildren<MonsterCheck>().Monster);
            gameObject.GetComponentInChildren<MonsterCheck>().Monster = null;
            canAttack = true;
        }
    }
}
