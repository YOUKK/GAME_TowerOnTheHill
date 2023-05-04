using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartMonster : Monster
{
    [SerializeField]
    private float   readyTime;
    private bool    isLaunch = false;
    public  bool    IsLaunch { get => isLaunch; set => isLaunch = value; }

    void Update()
    {
        if (!isLaunch)
            return;

        if (target == null) Move();
        else
        {
            if (!isAttack)
            {
                isAttack = true;
                StartCoroutine(AttackCoroutine());
            }
        }
        anim.SetBool("isAttack", isAttack);
    }

    public void UniqueTypeLaunch()
    {
        StartCoroutine(LaunchCoroutine());
    }

    IEnumerator LaunchCoroutine()
    {
        isLaunch = true;
        yield return new WaitForSeconds(readyTime);
        // Find way logic
        int upLine = (lineNumber == 4) ? 3 : lineNumber + 1;
        int downLine = (lineNumber == 0) ? 1 : lineNumber - 1;
        float chooseA = Map.GetInstance().GetLineInfo(upLine);
        float chooseB = Map.GetInstance().GetLineInfo(downLine);

        if (chooseA > chooseB) ChangeLine(chooseB);
        else ChangeLine(chooseA);
    }

    private void ChangeLine(float line)
    {
        if (lineNumber == line) return;


    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator AttackCoroutine()
    {
        Debug.Log("Coroutine Start");
        Attack();
        yield return new WaitForSeconds(status.hitSpeed);
        isAttack = false;
        Debug.Log("Coroutine End");
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
