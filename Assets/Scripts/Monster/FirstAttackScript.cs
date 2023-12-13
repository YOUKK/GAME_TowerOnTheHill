using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAttackScript : Monster
{
    private float distance = 8.0f;
    private Vector3 startPosition;

    protected override void Start()
    {
        currentHP = status.hp;
        currentSpeed = status.speed;
        currentForce = status.force;
        int ran = Random.Range(0, 100);
        if (ran <= randomPercent) isGetCoin = true;
        else isGetCoin = false;

        startPosition = transform.position;
        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null) sprite = GetComponentInChildren<SpriteRenderer>();
        randomCoin = Resources.Load<GameObject>("Prefabs/Projectile/Coin");
        if (randomCoin == null) Debug.LogError("Wrond Path Prefab Coin");
    }

    protected override void Update()
    {
        if (startPosition.x - transform.position.x < distance)
            Move(currentSpeed);
        else
            Destroy(gameObject);
    }

    protected override void Move(float speed)
    {
        base.Move(speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Character"))
        {
            collision.GetComponent<Character>().Hit(currentForce, this);
        }
    }
}
