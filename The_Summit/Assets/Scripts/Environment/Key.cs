using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public enum KeyAnimation
    {
        A,
        D,
        LeftShift,
        Space
    }
    [Tooltip("애니메이션을 실행할 키 선택")]
    public KeyAnimation keySelect;

    void Awake()
    {
        animator = GetComponent<Animator>();
        KeyAnimationSelect();
    }

    public void KeyAnimationSelect()
    {
        switch (keySelect)
        {
            case KeyAnimation.A:
                animator.SetTrigger("A");
                break;

            case KeyAnimation.D:
                animator.SetTrigger("D");
                break;

            case KeyAnimation.LeftShift:
                animator.SetTrigger("LeftShift");
                break;

            case KeyAnimation.Space:
                animator.SetTrigger("Space");
                break;
        }
    }
}
