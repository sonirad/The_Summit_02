using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAction : MonoBehaviour
{
    [Tooltip("�ȴ� �ӵ�")]
    [SerializeField] private float walkingSpeed;
    [Tooltip("�ٴ� �ӵ�")]
    [SerializeField] private float runningSpeed;
    [Tooltip("���� ��")]
    [SerializeField] private float jumpForce;
    [Tooltip("���� ����")]
    [SerializeField] private bool isJuming = false;
    [Tooltip("���� �˻�")]
    [SerializeField] private bool isGround = true;
    [Tooltip("����")]
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
    /// �̵� �̺�Ʈ ����.
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
    /// ���� �浹 �˻�. ���鿡 �浹�ϸ� �� ����.
    /// </summary>
    private bool AvailableGround
    {
        get
        {
            return Physics2D.Raycast(transform.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Ground"));
        }
    }

    /// <summary>
    /// ���� �÷��̾ ���ĵ� ���� ���� �˻�. ���� �־�� �ϰ� � Ű�� �Էµ� ���� �ʾƾ� �Ѵ�.
    /// </summary>
    private bool AvailableStanding
    {
        get
        {
            return isGround;
        }
    }

    /// <summary>
    /// �ȱ� ���� �˻�. ���� �־�� �ϰ� ���� ���� ���¿��� �Ѵ�.
    /// </summary>
    private bool AvailableWalk
    {
        get
        {
            return isGround && !isJuming;
        }
    }

    /// <summary>
    /// �ٱ� ����. �ȱ� ������ ����Ǿ�� �ϰ� Ű ���� shift Ű�� �Է��ؾ� �ϰ� ���� �־�� �ϰ� �������� �ʴ� ���¿��� �Ѵ�.
    /// </summary>
    private bool AvailableRun
    {
        get
        {
            return isGround && !isJuming;
        }
    }

    /// <summary>
    /// ĳ���Ͱ� ���� �־�� �ϰ� ������ ���� ���� ���¿��� �Ѵ�.            
    /// </summary>
    private bool AvailableJump
    {
        get
        {
            return !isJuming && isGround;
        }
    }

    /// <summary>
    /// ���� �浹 ó���� �ϴ� �Լ�
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
    /// ���ĵ� ����
    /// </summary>
    private void PlayerStanding()
    {
        if (AvailableStanding)
        {
            ani.SetTrigger("Standing");
            ani.SetFloat("Course", course);
            Debug.Log("���ĵ�");
            rigidbody2d.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// ������ �ȱ� �Լ�
    /// </summary>
    private void PlayerWalking_R()
    {
        if (AvailableWalk)
        {
            course = 1f;
            ani.SetTrigger("Walking");
            ani.SetFloat("Course", course);
            rigidbody2d.velocity = Vector2.right * walkingSpeed;
            Debug.Log("������ �ȱ�");
        }
    }

    /// <summary>
    /// ���� �ȱ� �Լ�
    /// </summary>
    private void PlayerWalking_L()
    {
        if (AvailableWalk)
        {
            course = -1f;
            ani.SetTrigger("Walking");
            ani.SetFloat("Course", course);
            rigidbody2d.velocity = Vector2.left * walkingSpeed;
            Debug.Log("���� �ȱ�");
        }
    }

    /// <summary>
    /// �ٱ� �Լ�
    /// </summary>
    private void PlayerRunning()
    {
        if (AvailableRun)
        {
            Debug.Log("�ٱ�");

            if (playerInput.walkRight)
            {
                course = 1f;
                ani.SetTrigger("Running");
                ani.SetFloat("Course", course);
                rigidbody2d.velocity = Vector2.right * runningSpeed;
                Debug.Log("������ �ٱ�");
            }
            else if (playerInput.walkLeft)
            {
                course = -1f;
                ani.SetTrigger("Running");
                ani.SetFloat("Course", course);
                rigidbody2d.velocity = Vector2.left * runningSpeed;
                Debug.Log("���� �ٱ�");
            }
        }
    }

    /// <summary>
    /// ���� �Լ�
    /// </summary>
    private void PlayerJumping()
    {
        if (AvailableJump)
        {
            isJuming = true;
            ani.SetTrigger("Jumping");
            ani.SetFloat("Course", course);
            rigidbody2d.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            Debug.Log("����");
        }
    }
}