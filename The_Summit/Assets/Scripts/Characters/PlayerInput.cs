using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //// �Է¹��� Ű�� ������ ����
    //private KeyCode keyWalkRight = KeyCode.D;
    //private KeyCode keyWalkLeft = KeyCode.A;
    //private KeyCode keyRun = KeyCode.LeftShift;
    //private KeyCode keyJump = KeyCode.Space;

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
    }

    /// <summary>
    /// ���������� Ű�� �Է¹޾� �ʱ�ȭ�� �ϴ� �Լ�.
    /// </summary>
    private void PlayerInputKey()
    {
        walkRight = Input.GetKey(KeyCode.D);
        walkLeft = Input.GetKey(KeyCode.A);
        run = Input.GetKey(KeyCode.LeftShift);
        jump = Input.GetKeyDown(KeyCode.Space);
    }
}
