using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerAction playerAction;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    [Tooltip("�÷��̾� �ִ� ��� ����")]
    [SerializeField] private int maxLifeCount;
    [Tooltip("�������� �Ծ����� �����̴� ����Ʈ ������ �ð�")]
    [SerializeField] private float dmageBlinkTime;
    [Tooltip("��������� ��� �ִϸ��̼��� ������ ����ǵ��� �ϴ� ������ �ð�")]
    [SerializeField] private float deathAnimationDelayTime;
    [Tooltip("���� �÷��̾� ��� ����")]
    public static int lifeCount;
    [Tooltip("�������� �Ծ����� ��Ÿ���� ���� ȿ��")]
    [SerializeField] private Color damageAlphaValue;
    [Tooltip("���ӿ��� �Ϸ���Ʈ")]
    [SerializeField] private Image gameOverImage;

    [Tooltip("������ ����")]
    public bool isDamage = false;
    [Tooltip("��� ����")]
    public bool isDeath = false;

    [Tooltip("���ӿ��� �Ϸ���Ʈ���� ����� ���� Ű �Է� �޴� ����")]
    private bool anyKey;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAction = GetComponent<PlayerAction>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        lifeCount = maxLifeCount;   // ������ ��� �ִ� ���� �ʱ�ȭ
    }

    private void Update()
    {
        anyKey = Input.anyKeyDown;
    }

    /// <summary>
    /// �������� �Ծ����� lifeCount�� �����ϰ� ������ ���� ����Ʈ�� ������ �ڷ�ƾ �Լ�.
    /// �������� ������ ĳ���� ���� ������ ��.
    /// </summary>
    public IEnumerator Damage()
    {
        isDamage = true;
        --lifeCount;
        Debug.Log(lifeCount);
        
        for(int i = 0; i < 2; i++)
        {
            if (spriteRenderer.color != damageAlphaValue)
            {
                spriteRenderer.color = damageAlphaValue;
            }

            else
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            }

            yield return new WaitForSeconds(dmageBlinkTime);
        }
    }

    /// <summary>
    /// ����� �߻������� ���� ����� ���� ȿ���� �����ϴ� �ڷ�ƾ �Լ�.
    /// </summary>
    public IEnumerator Death()
    {
        playerInput.enabled = false;
        playerAction.rigidbody2d.position = Vector2.zero;
        playerAction.rigidbody2d.velocity = Vector2.zero;
        animator.SetFloat("Course", playerAction.course);
        animator.SetTrigger("Death");
        Debug.Log("���");

        yield return new WaitForSeconds(deathAnimationDelayTime);

        gameOverImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(deathAnimationDelayTime);

        Debug.Log("�ƹ�Ű");

        yield return new WaitUntil(() => anyKey);
        Debug.Log("�����");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //if (Input.anyKey)
        //{
        //    Debug.Log("�����");
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
    }
}
