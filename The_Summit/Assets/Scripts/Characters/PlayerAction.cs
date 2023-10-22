using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAction : MonoBehaviour
{
    [Tooltip("걷는 속도")]
    [SerializeField] private float walkingSpeed;
    [Tooltip("뛰는 속도")]
    [SerializeField] private float runningSpeed;
    [Tooltip("점프 힘")]
    [SerializeField] private float jumpForce;
    // [SerializeField] private float jumpForce;
    [Tooltip("점프 카운트")]
    [SerializeField] private int jumpCount = 0;
    [Tooltip("지면 검사")]
    [SerializeField] private bool isGround = true;
    [Tooltip("방향")]
    [HideInInspector] public float course = 1f;

    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private Animator ani;
    [SerializeField] private PlayerInput playerInput;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        IsGround();
        IsStanding();
        Playerwalking();
        PlayerRunning();
        PlayerJumping();
    }

    /// <summary>
    /// 지면 충돌 검사. 지면에 충돌하면 참 리턴.
    /// </summary>
    private bool AvailableGround
    {
        get
        {
            return Physics2D.Raycast(transform.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Ground"));
        }
    }

    /// <summary>
    /// 현재 플레이어가 스탠딩 상태 인지 검사. 땅에 있어야 하고 어떤 키의 입력도 받지 않아야 한다.
    /// </summary>
    private bool AvailableStanding
    {
        get
        {
            return isGround && !playerInput.walkRight && !playerInput.walkLeft && !playerInput.run && !playerInput.jump;
        }
    }

    /// <summary>
    /// 걷기 조건 검사. 땅에 있어야 하고 키 A 혹은 D를 입력해야 한다.
    /// </summary>
    private bool AvailableWalk
    {
        get
        {
            return isGround && (playerInput.walkRight || playerInput.walkLeft);
        }
    }

    /// <summary>
    /// 뛰기 조건. 걷기 조건이 선행되어야 하고 키 왼쪽 shift 키를 입력해야 한다.
    /// </summary>
    private bool AvailableRun
    {
        get
        {
            return AvailableWalk && playerInput.run;
        }
    }

    /// <summary>
    /// 이단 점프 조건. 점프 카운트가 2가 되면 점프 불가능하며 키 space 바를 입력해야 한다.
    /// </summary>
    private bool AvailableJump
    {
        get
        {
            return playerInput.jump && jumpCount < 1;
        }
    }

    /// <summary>
    /// 지면 충돌 처리를 하는 함수
    /// </summary>
    private void IsGround()
    {
        isGround = AvailableGround;
    }

    /// <summary>
    /// 스탠딩 상태 즉 모든 입력을 받고 있지 않는 상태일 때 캐릭터는 움직이지 않아야 한다. 움직임 봉쇄 처리 함수.
    /// </summary>
    private void IsStanding()
    {
        if (AvailableStanding)
        {
            ani.SetFloat("Course", course);
            ani.SetTrigger("Standing");
            rigidbody2d.velocity = Vector2.zero;
            Debug.Log("스탠딩");
        }
    }

    /// <summary>
    /// 걷기 함수
    /// </summary>
    private void Playerwalking()
    {
        if (AvailableWalk && playerInput.walkRight && !playerInput.run)
        {
            course = 1f;
            ani.SetFloat("Course", course);
            ani.SetTrigger("Walking");
            rigidbody2d.velocity = Vector2.right * walkingSpeed;
            Debug.Log("오른쪽 걷기");
        }
        else if (AvailableWalk && playerInput.walkLeft && !playerInput.run)
        {
            course = -1f;
            ani.SetFloat("Course", course);
            ani.SetTrigger("Walking");
            rigidbody2d.velocity = Vector2.left * walkingSpeed;
            Debug.Log("왼쪽 걷기");
        }
    }

    /// <summary>
    /// 뛰기 함수
    /// </summary>
    private void PlayerRunning()
    {
        if (AvailableRun)
        {
            Debug.Log("뛰기");

            if (playerInput.walkRight)
            {
                course = 1f;
                ani.SetFloat("Course", course);
                ani.SetTrigger("Running");
                rigidbody2d.velocity = Vector2.right * runningSpeed;
                Debug.Log("오른쪽 뛰기");
            }
            else if (playerInput.walkLeft)
            {
                course = -1f;
                ani.SetFloat("Course", course);
                ani.SetTrigger("Running");
                rigidbody2d.velocity = Vector2.left * runningSpeed;
                Debug.Log("왼쪽 뛰기");
            }
        }
    }

    /// <summary>
    /// 점프 함수
    /// </summary>
    private void PlayerJumping()
    {
        if (AvailableJump)
        {
            ++jumpCount;
            rigidbody2d.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            Debug.Log(rigidbody2d.velocity.y);
            Debug.Log("점프");
        }

        if (isGround && !playerInput.jump)
        {
            jumpCount = 0;
        }
    }
}