using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] public delegate void WalkRight();
    [SerializeField] public delegate void WalkLeft();
    [SerializeField] public delegate void Run();
    [SerializeField] public delegate void Jump();
    [SerializeField] public event WalkRight OnWalkRight;
    [SerializeField] public event WalkLeft OnWalkLeft;
    [SerializeField] public event Run OnRun;
    [SerializeField] public event Jump OnJump;

    /// <summary>
    /// 입력하는 키의 값을 받아 저장. 활성화 하는지 안하는지 bool로 구별.
    /// </summary>
    [HideInInspector] public bool walkRight { get; private set; }
    [HideInInspector] public bool walkLeft { get; private set; }
    [HideInInspector] public bool run { get; private set; }
    [HideInInspector] public bool jump { get; private set; }

    void Update()
    {
        PlayerInputKey();

        if (walkRight && !jump)
        {
            Debug.Log("키 오른쪽 걷기");
            OnWalkRight.Invoke();
        }
        else if (walkLeft && !jump)
        {
            Debug.Log("키 왼쪽 걷기");
            OnWalkLeft.Invoke();
        }

        if (run)
        {
            Debug.Log("키 뛰기");
            OnRun.Invoke();
        }

        if (jump)
        {
            Debug.Log("키 점프");
            OnJump.Invoke();
        }
    }

    private void PlayerInputKey()
    {
        walkRight = Input.GetKey(KeyCode.D);
        walkLeft = Input.GetKey(KeyCode.A);
        run = Input.GetKey(KeyCode.LeftShift);
        jump = Input.GetKeyDown(KeyCode.Space);
    }
}
