using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public enum TrapAnimation
    {
        Trap01,
        Trap02,
        Trap03
    }

    [Tooltip("애니메이션을 실행할 트랩 선택")]
    public TrapAnimation trapSelect;

    void Awake()
    {
        animator = GetComponent<Animator>();
        TrapAnimationSelect();
    }

    public void TrapAnimationSelect()
    {
        switch (trapSelect)
        {
            case TrapAnimation.Trap01:
                animator.SetTrigger("Trap01");
                break;

            case TrapAnimation.Trap02:
                animator.SetTrigger("Trap02");
                break;

            case TrapAnimation.Trap03:
                animator.SetTrigger("Trap03");
                break;
        }
    }
}
