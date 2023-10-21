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
    [Tooltip("�������� �Ծ����� ��Ÿ���� ȿ�� ���İ�")]
    [SerializeField] private Color damageAlphaValue;
    [Tooltip("���ӿ��� �Ϸ���Ʈ")]
    [SerializeField] private Image gameOverImage;

    [Tooltip("������ ����")]
    public bool isDamage;
    [Tooltip("��� ����")]
    public bool isDeath;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAction = GetComponent<PlayerAction>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        lifeCount = maxLifeCount;   // ������ 
    }

    /// <summary>
    /// �������� �Ծ����� lifeCount�� �����ϰ� ������ ���� ����Ʈ�� ������ �ڷ�ƾ �Լ�.
    /// </summary>
    public IEnumerator Damage()
    {
        isDamage = true;
        --lifeCount;
        Debug.Log(lifeCount);
        
        for(int i = 0; i < 4; i++)
        {
            if (spriteRenderer.color.a != damageAlphaValue.a)
            {
                spriteRenderer.color = damageAlphaValue;
            }

            else
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            }

            yield return new WaitForSecondsRealtime(dmageBlinkTime);
        }
    }

    /// <summary>
    /// ����� �߻������� ���� ����� ���� ȿ���� �����ϴ� �ڷ�ƾ �Լ�.
    /// </summary>
    public IEnumerator Death()
    {
        isDeath = true;
        playerInput.enabled = false;
        this.transform.position = Vector2.zero;
        animator.SetFloat("Course", playerAction.course);
        animator.SetTrigger("Death");
        Debug.Log("���");

        yield return new WaitForSecondsRealtime(deathAnimationDelayTime);

        gameOverImage.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(deathAnimationDelayTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
