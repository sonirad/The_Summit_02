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
    [Tooltip("점프 상태")]
    [SerializeField] private bool isJuming = false;
    [Tooltip("지면 검사")]
    [SerializeField] private bool isGround = true;
    [Tooltip("방향")]
    [HideInInspector] public float course = 1f;

    public Rigidbody2D rigidbody2d;
    [SerializeField] private Animator ani;
    [SerializeField] private PlayerInput playerInput;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        PlayerActionInputStorage();
    }

    private void Update()
    {
        IsGround();
    }

    /// <summary>
    /// 이동 이벤트 구독.
    /// </summary>
    private void PlayerActionInputStorage()
    {
        playerInput.OnWalkRight += PlayerWalking_R;
        playerInput.OnWalkLeft += PlayerWalking_L;
        playerInput.OnRun += PlayerRunning;
        playerInput.OnJump += PlayerJumping;
        playerInput.OnStanding += PlayerStanding;
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
            return isGround;
        }
    }

    /// <summary>
    /// 걷기 조건 검사. 땅에 있어야 하고 뛰지 않은 상태여야 한다.
    /// </summary>
    private bool AvailableWalk
    {
        get
        {
            return isGround && !isJuming;
        }
    }

    /// <summary>
    /// 뛰기 조건. 걷기 조건이 선행되어야 하고 키 왼쪽 shift 키를 입력해야 하고 땅에 있어야 하고 점프하지 않는 상태여야 한다.
    /// </summary>
    private bool AvailableRun
    {
        get
        {
            return isGround && !isJuming;
        }
    }

    /// <summary>
    /// 캐릭터가 땅에 있어야 하고 점프를 하지 않은 상태여야 한다.            
    /// </summary>
    private bool AvailableJump
    {
        get
        {
            return !isJuming && isGround;
        }
    }

    /// <summary>
    /// 지면 충돌 처리를 하는 함수
    /// </summary>
    private void IsGround()
    {
        isGround = AvailableGround;
        ani.SetBool("isGround", isGround);

        if (isGround && !playerInput.jump)
        {
            isJuming = false;
        }
    }

    /// <summary>
    /// 스탠딩 상태
    /// </summary>
    private void PlayerStanding()
    {
        if (AvailableStanding)
        {
            ani.SetTrigger("Standing");
            ani.SetFloat("Course", course);
            Debug.Log("스탠딩");
            rigidbody2d.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// 오른쪽 걷기 함수
    /// </summary>
    private void PlayerWalking_R()
    {
        if (AvailableWalk)
        {
            course = 1f;
            ani.SetTrigger("Walking");
            ani.SetFloat("Course", course);
            rigidbody2d.velocity = Vector2.right * walkingSpeed;
            Debug.Log("오른쪽 걷기");
        }
    }

    /// <summary>
    /// 왼쪽 걷기 함수
    /// </summary>
    private void PlayerWalking_L()
    {
        if (AvailableWalk)
        {
            course = -1f;
            ani.SetTrigger("Walking");
            ani.SetFloat("Course", course);
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
                ani.SetTrigger("Running");
                ani.SetFloat("Course", course);
                rigidbody2d.velocity = Vector2.right * runningSpeed;
                Debug.Log("오른쪽 뛰기");
            }
            else if (playerInput.walkLeft)
            {
                course = -1f;
                ani.SetTrigger("Running");
                ani.SetFloat("Course", course);
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
            isJuming = true;
            ani.SetTrigger("Jumping");
            ani.SetFloat("Course", course);
            rigidbody2d.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            Debug.Log("점프");
        }
    }
}