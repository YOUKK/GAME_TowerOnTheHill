using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private SunFlower mainCharacter;
    private float pAttackDuration = 0;
    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = transform.GetComponentInParent<SunFlower>();
    }

    // Update is called once per frame
    void Update()
    {
        pAttackDuration += Time.deltaTime;
        if (pAttackDuration >= mainCharacter.AttackDuration)
        {
            Destroy(gameObject);
        }
        //��Ŭ�� ���� ��
        if(Input.GetMouseButtonDown(0))
        {
            //��Ŭ���� ����Ʈ�� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                       Input.mousePosition.y,
                                                                       -Input.mousePosition.z));
            if(Vector2.Distance(transform.position, point) < 1f)
            {
                //��ü �ڿ� �߰� �̱���
                Destroy(gameObject);
            }
        }
    }
}