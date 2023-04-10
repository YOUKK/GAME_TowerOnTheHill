using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColl : MonoBehaviour
{
    IEnumerator HitCoroutine = null;

    private float pHitDelay;
    private float hitDelay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(HitCoroutine);
        if (HitCoroutine == null)
        {
            HitCoroutine = HitCoolTime();
            StartCoroutine(HitCoroutine);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(HitCoroutine);
        HitCoroutine = null;
    }

    IEnumerator HitCoolTime()
    {
        Debug.Log("Start Hit Coroutine");
        while(true)
        {
            pHitDelay += Time.deltaTime;
            if(pHitDelay >= hitDelay)
            {
                //gameObject.GetComponents<>[]
                pHitDelay -= hitDelay;
            }
            yield return new WaitForSeconds(0);
        }
    }
}
