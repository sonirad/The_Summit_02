using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] public delegate void WalkRight();
    [SerializeField] public delegate void WalkLeft();
    [SerializeField] public delegate void Run();
    [SerializeField] public delegate void Jump();
    [SerializeField] public delegate void Standing();
    [SerializeField] public event WalkRight OnWalkRight;
    [SerializeField] public event WalkLeft OnWalkLeft;
    [SerializeField] public event Run OnRun;
    [SerializeField] public event Jump OnJump;
    [SerializeField] public event Standing OnStanding;

    /// <summary>
    /// 입력하는 키의 값을 받아 저장. 활성화 하는지 안하는지 bool로 구별.
    /// </summary>
    [HideInInspector] public bool walkRight { get; private set; }
    [HideInInspector] public bool walkLeft { get; private set; }
    [HideInInspector] public bool run { get; private set; }
    [HideInInspector] public bool jump { get; private set; }

    [SerializeField] private Rigidbody2D rig2d;
    private bool isJump;

    private void Awake()
    {
        rig2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerInputKey();
        JumpInputCheck();

        if (jump)
        {
            isJump = true;
            Debug.Log("키 점프");
            OnJump.Invoke();
        }

        else if (walkRight && !jump && !run && !isJump)
        {
            Debug.Log("키 오른쪽 걷기");
            OnWalkRight.Invoke();
        }
        else if (walkLeft && !jump && !run && !isJump)
        {
            Debug.Log("키 왼쪽 걷기");
            OnWalkLeft.Invoke();
        }

        else if ((walkRight || walkLeft) && run && !isJump)
        {
            Debug.Log("키 뛰기");
            OnRun.Invoke();
        }

        else if(!walkRight && !walkLeft && !run && !jump)
        {
            Debug.Log("키 스탠딩");
            OnStanding.Invoke();
        }
    }

    private void PlayerInputKey()
    {
        walkRight = Input.GetKey(KeyCode.D);
        walkLeft = Input.GetKey(KeyCode.A);
        run = Input.GetKey(KeyCode.LeftShift);
        jump = Input.GetKeyDown(KeyCode.Space);
    }

    /// <summary>
    /// 점프키를 위한 다른 키 input 막기.
    /// 리지드바디 velocity y 값이 -값 즉 점프 한 후 내려오는 도중에 다른 키 조건 풀기.
    /// </summary>
    private void JumpInputCheck()
    {
        if (rig2d.velocity.y < 0)
        {
            isJump = false;
        }
    }
}
