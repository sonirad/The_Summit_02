using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Tooltip("�ȴ� �ӵ�")]
    [SerializeField] private float walkingSpeed;
    [Tooltip("�ٴ� �ӵ�")]
    [SerializeField] private float runningSpeed;
    [Tooltip("���� ��")]
    [SerializeField] private Vector2 jumpForce;
    [Tooltip("���� ī��Ʈ")]
    [SerializeField] private int jumpCount = 0;
    [Tooltip("���� �˻�")]
    [SerializeField] private bool isGround = true;
    [Tooltip("����")]
    private float course = 1f;

    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private Animator ani;
    [SerializeField] private PlayerInput playerInput;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        IsGround();
        IsStanding();
        Playerwalking();
        PlayerRunning();
        PlayerJumping();
    }

    /// <summary>
    /// ���� �浹 �˻�. ���鿡 �浹�ϸ� �� ����.
    /// </summary>
    private bool AvailableGround
    {
        get
        {
            return Physics2D.Raycast(transform.position, Vector2.down, 2f, 1 << LayerMask.NameToLayer("Ground"));
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
            return isGround && (playerInput.walkRight || playerInput.walkLeft);
        }
    }

    /// <summary>
    /// �ٱ� ����. �ȱ� ������ ����Ǿ�� �ϰ� Ű ���� shift Ű�� �Է��ؾ� �Ѵ�.
    /// </summary>
    private bool AvailableRun
    {
        get
        {
            return AvailableWalk && playerInput.run;
        }
    }

    /// <summary>
    /// �̴� ���� ����. ���� ī��Ʈ�� 2�� �Ǹ� ���� �Ұ����ϸ� Ű space �ٸ� �Է��ؾ� �Ѵ�.
    /// </summary>
    private bool AvailableJump
    {
        get
        {
            return playerInput.jump && jumpCount < 2;
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
            rigidbody2d.velocity = Vector2.zero;
            Debug.Log("���ĵ�");
        }
    }

    /// <summary>
    /// �ȱ� �Լ�
    /// </summary>
    private void Playerwalking()
    {
        if (AvailableWalk && playerInput.walkRight && !playerInput.run)
        {
            course = 1f;
            ani.SetFloat("Course", course);
            ani.SetTrigger("Walking");
            rigidbody2d.velocity = Vector2.right * walkingSpeed * Time.deltaTime;
            Debug.Log("������ �ȱ�");
        }
        else if (AvailableWalk && playerInput.walkLeft && !playerInput.run)
        {
            course = -1f;
            ani.SetFloat("Course", course);
            ani.SetTrigger("Walking");
            rigidbody2d.velocity = Vector2.left * walkingSpeed * Time.deltaTime;
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
                ani.SetFloat("Course", course);
                ani.SetTrigger("Running");
                rigidbody2d.velocity = Vector2.right * runningSpeed * Time.deltaTime;
                Debug.Log("������ �ٱ�");
            }
            else if (playerInput.walkLeft)
            {
                course = -1f;
                ani.SetFloat("Course", course);
                ani.SetTrigger("Running");
                rigidbody2d.velocity = Vector2.left * runningSpeed * Time.deltaTime;
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
            ++jumpCount;
            rigidbody2d.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            Debug.Log("����");
        }

        if (isGround && !playerInput.jump)
        {
            jumpCount = 0;
        }
    }
}
