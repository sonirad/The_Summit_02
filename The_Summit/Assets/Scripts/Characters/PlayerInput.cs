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
    /// �Է��ϴ� Ű�� ���� �޾� ����. Ȱ��ȭ �ϴ��� ���ϴ��� bool�� ����.
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
            Debug.Log("Ű ������ �ȱ�");
            OnWalkRight.Invoke();
        }
        else if (walkLeft && !jump)
        {
            Debug.Log("Ű ���� �ȱ�");
            OnWalkLeft.Invoke();
        }

        if (run)
        {
            Debug.Log("Ű �ٱ�");
            OnRun.Invoke();
        }

        if (jump)
        {
            Debug.Log("Ű ����");
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
