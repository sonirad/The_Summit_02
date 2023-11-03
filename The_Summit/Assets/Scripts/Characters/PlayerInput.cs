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
    /// �Է��ϴ� Ű�� ���� �޾� ����. Ȱ��ȭ �ϴ��� ���ϴ��� bool�� ����.
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
            Debug.Log("Ű ����");
            OnJump.Invoke();
        }

        else if (walkRight && !jump && !run && !isJump)
        {
            Debug.Log("Ű ������ �ȱ�");
            OnWalkRight.Invoke();
        }
        else if (walkLeft && !jump && !run && !isJump)
        {
            Debug.Log("Ű ���� �ȱ�");
            OnWalkLeft.Invoke();
        }

        else if ((walkRight || walkLeft) && run && !isJump)
        {
            Debug.Log("Ű �ٱ�");
            OnRun.Invoke();
        }

        else if(!walkRight && !walkLeft && !run && !jump)
        {
            Debug.Log("Ű ���ĵ�");
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
    /// ����Ű�� ���� �ٸ� Ű input ����.
    /// ������ٵ� velocity y ���� -�� �� ���� �� �� �������� ���߿� �ٸ� Ű ���� Ǯ��.
    /// </summary>
    private void JumpInputCheck()
    {
        if (rig2d.velocity.y < 0)
        {
            isJump = false;
        }
    }
}
