using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //// 입력받은 키를 저장할 변수
    //private KeyCode keyWalkRight = KeyCode.D;
    //private KeyCode keyWalkLeft = KeyCode.A;
    //private KeyCode keyRun = KeyCode.LeftShift;
    //private KeyCode keyJump = KeyCode.Space;

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
    }

    /// <summary>
    /// 지속적으로 키를 입력받아 초기화를 하는 함수.
    /// </summary>
    private void PlayerInputKey()
    {
        walkRight = Input.GetKey(KeyCode.D);
        walkLeft = Input.GetKey(KeyCode.A);
        run = Input.GetKey(KeyCode.LeftShift);
        jump = Input.GetKeyDown(KeyCode.Space);
    }
}
