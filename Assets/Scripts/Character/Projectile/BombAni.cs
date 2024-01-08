using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAni : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OnEnterFinishScene()
    {
        gameObject.GetComponentInParent<Bomber>().Bombed();
    }
}
