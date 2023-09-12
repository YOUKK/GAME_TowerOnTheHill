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
        while (!CheckMonster) { }
        if (!IsDragged && canAttack && CheckMonster)
        {
            anim.SetTrigger("canAttack");
            Invoke("attackDelaySet", 1.7f);
        }
    }
    void attackDelaySet()
    {
        gameObject.GetComponentInChildren<MonsterCheck>().Monster.GetComponent<Monster>().Hit(10000000);
    }
}
