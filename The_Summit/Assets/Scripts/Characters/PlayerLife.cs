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
    [SerializeField] private AudioSource audioSource;

    [Tooltip("플레이어 최대 목숨 갯수")]
    [SerializeField] private int maxLifeCount;
    [Tooltip("데미지를 입었을때 깜빡이는 이펙트 딜레이 시간")]
    [SerializeField] private float dmageBlinkTime;
    [Tooltip("사망했을때 사망 애니메이션이 끝까지 진행되도록 하는 딜레이 시간")]
    [SerializeField] private float deathAnimationDelayTime;
    [Tooltip("현재 플레이어 목숨 갯수")]
    public static int lifeCount;
    [Tooltip("데미지를 입었을때 나타나는 빨강 효과")]
    [SerializeField] private Color damageRedValue;
    [Tooltip("게임오버 일러스트")]
    [SerializeField] private Image gameOverImage;

    [Tooltip("데미지 입음")]
    public bool isDamage = false;
    [Tooltip("사망 상태")]
    public bool isDeath = false;

    [Tooltip("게임오버 일러스트에서 벗어나기 위한 키 입력 받는 변수")]
    private bool anyKey;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAction = GetComponent<PlayerAction>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rigi2d = GetComponent<Rigidbody2D>();

        lifeCount = maxLifeCount;   // 지정한 목숨 최대 갯수 초기화
    }

    private void Update()
    {
        anyKey = Input.anyKeyDown;    // 게임 오버 시 재시작 신호 키보드 키
        PlayerDeathMoveStop();
    }

    /// <summary>
    /// 데미지를 입었을때 lifeCount를 차감하고 데미지 받은 이펙트를 실행하는 코루틴 함수.
    /// 데미지를 받으면 캐릭터 색이 빨강이 됨.
    /// </summary>
    public IEnumerator Damage()
    {
        isDamage = true;
        --lifeCount;
        Debug.Log("데미지 발생. 라이프 -1 차감");
        
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

            Debug.Log("데미지 이펙트 딜레이 타임");
            yield return new WaitForSeconds(dmageBlinkTime);
        }

        isDamage = false;
    }

    /// <summary>
    /// 사망이 발생했을때 각종 사망에 따른 효과를 실행하는 코루틴 함수.
    /// 사망했을때 플레이어 input을 받고, 캐릭터가 움직이는 기능이 아직 구현이 안됨.
    /// 그리고 사망했을때 갑자기 캐릭터가 오른쪽으로 날아가는 버그가 존재. 리지드바디 velocity 와 position의 x 값이 동결이 안됨.
    /// </summary>
    public IEnumerator Death()
    {
        Debug.Log("사망");
        isDeath = true;
        playerInput.enabled = false;
        playerAction.enabled = false;
        animator.SetFloat("Course", playerAction.course);
        animator.SetTrigger("Death");

        yield return new WaitForSeconds(deathAnimationDelayTime);

        gameOverImage.gameObject.SetActive(true);
        Debug.Log("아무키");

        yield return new WaitUntil(() => anyKey);
        Debug.Log("재시작");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PlayerDeathMoveStop()
    {
        if (isDeath)
        {
            rigi2d.velocity = Vector2.zero;
            Debug.Log("사망 캐릭터 멈춤");
        }
    }
}
