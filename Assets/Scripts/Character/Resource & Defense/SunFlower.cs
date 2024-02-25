using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        AttackDuration = DataManager.GetData.GetUpgradeDataDic()["Fairy"].statIncrease[
                         DataManager.GetData.GetUpgradeDataDic()["Fairy"].currentLevel];

        GamePlayManagers.Instance.finishProcess += StopGem;
    }

    private void StopGem()
    {
        StopCoroutine(AttackCoroutine);
    }

    protected override void Dead()
    {
        GamePlayManagers.Instance.finishProcess -= StopGem;
        base.Dead();
    }

    public override void Attack()
    {
        anim.SetTrigger("canAttack");
        Invoke("attackDelaySet", 0.7f);
    }
    void attackDelaySet()
    {
        projectile = projectiles.Dequeue();
        projectiles.Enqueue(projectile);
        activatedProj.Enqueue(projectile);
        //print(projectile.name+","+ projectiles.Count);
        projectile.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0f, 1f),
                                                    gameObject.transform.position.y + Random.Range(-1f, 1f));
        projectile.SetActive(true);
        Invoke("Duration", attackDuration);
    }
    void Duration()
    {
        activatedProj.Peek().transform.position = gameObject.transform.position;
        GameObject T = activatedProj.Dequeue();
        T.transform.GetComponent<Resource>().MinusDelegate();
        T.SetActive(false);
    }
}
