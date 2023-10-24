using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerAction playerAction;
    [SerializeField] private Rigidbody2D rigi2d;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [Tooltip("�÷��̾� �ִ� ��� ����")]
    [SerializeField] private int maxLifeCount;
    [Tooltip("�������� �Ծ����� �����̴� ����Ʈ ������ �ð�")]
    [SerializeField] private float dmageBlinkTime;
    [Tooltip("��������� ��� �ִϸ��̼��� ������ ����ǵ��� �ϴ� ������ �ð�")]
    [SerializeField] private float deathAnimationDelayTime;
    [Tooltip("���� �÷��̾� ��� ����")]
    public static int lifeCount;
    [Tooltip("�������� �Ծ����� ��Ÿ���� ���� ȿ��")]
    [SerializeField] private Color damageRedValue;
    [Tooltip("���ӿ��� �Ϸ���Ʈ")]
    [SerializeField] private Image gameOverImage;

    [Tooltip("������ ����")]
    public bool isDamage = false;
    [Tooltip("��� ����")]
    public bool isDeath = false;

    [Tooltip("���ӿ��� �Ϸ���Ʈ���� ����� ���� Ű �Է� �޴� ����")]
    private bool gameOverAnyKey;
    private bool gameOverAnyMouseKey_R;
    private bool gameOverAnyMouseKey_L;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAction = GetComponent<PlayerAction>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigi2d = GetComponent<Rigidbody2D>();

        lifeCount = maxLifeCount;   // ������ ��� �ִ� ���� �ʱ�ȭ
    }

    private void Update()
    {
        gameOverAnyKey = Input.anyKeyDown;    // ���� ���� �� ����� ��ȣ Ű���� Ű
        gameOverAnyMouseKey_R= Input.GetMouseButtonDown(1);  // ���� ���� �� ����� ��ȣ ���콺 Ű
        gameOverAnyMouseKey_L = Input.GetMouseButtonDown(0);  // ���� ���� �� ����� ��ȣ ���콺 Ű
        PlayerDeathMoveStop();
    }

    /// <summary>
    /// �������� �Ծ����� lifeCount�� �����ϰ� ������ ���� ����Ʈ�� �����ϴ� �ڷ�ƾ �Լ�.
    /// �������� ������ ĳ���� ���� ������ ��.
    /// </summary>
    public IEnumerator Damage()
    {
        isDamage = true;
        --lifeCount;
        Debug.Log("������ �߻�. ������ -1 ����");
        
        for(int i = 0; i < 2; i++)
        {
            if (spriteRenderer.color != damageRedValue)
            {
                spriteRenderer.color = damageRedValue;
            }

            else
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            }

            Debug.Log("������ ����Ʈ ������ Ÿ��");
            yield return new WaitForSeconds(dmageBlinkTime);
        }

        isDamage = false;
    }

    /// <summary>
    /// ����� �߻������� ���� ����� ���� ȿ���� �����ϴ� �ڷ�ƾ �Լ�.
    /// </summary>
    public IEnumerator Death()
    {
        Debug.Log("���");
        isDeath = true;
        playerInput.enabled = false;
        playerAction.enabled = false;
        animator.SetFloat("Course", playerAction.course);
        animator.SetTrigger("Death");

        yield return new WaitForSeconds(deathAnimationDelayTime);

        gameOverImage.gameObject.SetActive(true);
        Debug.Log("�ƹ�Ű");

        yield return new WaitUntil(() => gameOverAnyKey || gameOverAnyMouseKey_R || gameOverAnyMouseKey_L);
        Debug.Log("�����");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// ���� ���� �� �ƹ�Ű �Է� �޾� ���� �ٽ� ����.
    /// </summary>
    private void PlayerDeathMoveStop()
    {
        if (isDeath)
        {
            rigi2d.velocity = Vector2.zero;
            Debug.Log("��� ĳ���� ����");
        }
    }
}
