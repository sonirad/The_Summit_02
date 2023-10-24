using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAction : MonoBehaviour
{
    /// <summary>
    /// �Է��ϴ� Ű�� ���� �޾� ����. Ȱ��ȭ �ϴ��� ���ϴ��� bool�� ����.
    /// </summary>
    [HideInInspector] public bool walkRight { get; private set; }
    [HideInInspector] public bool walkLeft { get; private set; }
    [HideInInspector] public bool run { get; private set; }
    [HideInInspector] public bool jump { get; private set; }

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
        IsStanding();
    }

    private void PlayerActionInputStorage()
    {
        playerInput.OnWalkRight += PlayerWalking_R;
        playerInput.OnWalkLeft += PlayerWalking_L;
        playerInput.OnRun += PlayerRunning;
        playerInput.OnJump += PlayerJumping;
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
            return isGround && !playerInput.walkRight && !playerInput.walkLeft && !playerInput.run && !playerInput.jump;
        }
    }

    /// <summary>
    /// �ȱ� ���� �˻�. ���� �־�� �ϰ� Ű A Ȥ�� D�� �Է��ؾ� �Ѵ�.
    /// </summary>
    private bool AvailableWalk
    {
        get
        {
            return isGround && !playerInput.run && !isJuming;
        }
    }

    /// <summary>
    /// �ٱ� ����. �ȱ� ������ ����Ǿ�� �ϰ� Ű ���� shift Ű�� �Է��ؾ� �Ѵ�.
    /// </summary>
    private bool AvailableRun
    {
        get
        {
            return (playerInput.walkRight || playerInput.walkLeft) && isGround && !isJuming;
        }
    }

    /// <summary>
    /// ĳ���Ͱ� ���� �־�� �ϰ� Ű space �ٸ� �Է��ؾ� ���� ����.            
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
    }

    /// <summary>
    /// ���ĵ� ���� �� ��� �Է��� �ް� ���� �ʴ� ������ �� ĳ���ʹ� �������� �ʾƾ� �Ѵ�. ������ ���� ó�� �Լ�.
    /// </summary>
    private void IsStanding()
    {
        if (AvailableStanding)
        {
            ani.SetFloat("Course", course);
            ani.SetTrigger("Standing");
            Debug.Log("���ĵ�");
        }
    }

    /// <summary>
    /// �ȱ� �Լ�
    /// </summary>
    public void PlayerWalking()
    {
        if (AvailableWalk && walkRight && !run)
        {
            course = 1f;
            ani.SetFloat("Course", course);
            ani.SetTrigger("Walking");
            // transform.Translate(Vector2.right * walkingSpeed * Time.deltaTime);
            rigidbody2d.velocity = Vector2.right * walkingSpeed;
            Debug.Log("������ �ȱ�");
        }
        else if (AvailableWalk && walkLeft && !run)
        {
            course = -1f;
            ani.SetFloat("Course", course);
            ani.SetTrigger("Walking");
            // transform.Translate(Vector2.left * walkingSpeed * Time.deltaTime);
            rigidbody2d.velocity = Vector2.left * walkingSpeed;
            Debug.Log("���� �ȱ�");
        }
    }

    private void PlayerWalking_R()
    {
        if (AvailableWalk)
        {
            course = 1f;
            ani.SetFloat("Course", course);
            ani.SetTrigger("Walking");
            // transform.Translate(Vector2.right * walkingSpeed * Time.deltaTime);
            rigidbody2d.velocity = Vector2.right * walkingSpeed;
            Debug.Log("������ �ȱ�");
        }
    }

    private void PlayerWalking_L()
    {
        if (AvailableWalk)
        {
            course = -1f;
            ani.SetFloat("Course", course);
            ani.SetTrigger("Walking");
            // transform.Translate(Vector2.left * walkingSpeed * Time.deltaTime);
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

            // if (walkRight)
            if (playerInput.walkRight)
            {
                course = 1f;
                ani.SetFloat("Course", course);
                ani.SetTrigger("Running");
                // transform.Translate(Vector2.right * runningSpeed * Time.deltaTime);
                rigidbody2d.velocity = Vector2.right * runningSpeed;
                Debug.Log("������ �ٱ�");
            }
            // else if (walkLeft)
            else if (playerInput.walkLeft)
            {
                course = -1f;
                ani.SetFloat("Course", course);
                ani.SetTrigger("Running");
                // transform.Translate(Vector2.left * runningSpeed * Time.deltaTime);
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
            rigidbody2d.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            Debug.Log("����");
        }

        if (isGround && !jump)
        {
            isJuming = false;
        }
    }
}