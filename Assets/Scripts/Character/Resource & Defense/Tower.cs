using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Character
{
    [SerializeField]
    private int     createResourceTime = 1;
    private bool    isCreating = false;
    private bool    isDamaged = false;

    void Update()
    {
        if (!isCreating)
        {
            isCreating = true;
            StartCoroutine(CreateResourceCoroutine());
        }
        if(isDamaged)
        {
            
        }
    }

    public void Hit(int damage)
    {
        if(status.healthPoint - damage <= 0)
        {
            // 해당 라인 즉사기 공격
            Destroy(gameObject);
        }
        else
        {
            
        }
    }

    IEnumerator CreateResourceCoroutine()
    {
        CreateResource();
        yield return new WaitForSeconds(createResourceTime);
        isCreating = false;
    }

    void CreateResource()
    {
        projectile = projectiles.Dequeue();
        projectiles.Enqueue(projectile);
        activatedProj.Enqueue(projectile);
        //print(projectile.name+","+ projectiles.Count);
        projectile.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(-1f, 1f),
                                                    gameObject.transform.position.y + Random.Range(-1f, 1f));
        projectile.SetActive(true);
        Invoke("Duration", attackDuration);
    }

    void Duration()
    {
        activatedProj.Peek().transform.position = gameObject.transform.position;
        GameObject T = activatedProj.Dequeue();
        T.SetActive(false);
    }

    IEnumerator AttackCoroutine()
    {
        Attack();
        yield return new WaitForSeconds(0f);
        
    }
    
    public override void Attack()
    {
        
    }
}
