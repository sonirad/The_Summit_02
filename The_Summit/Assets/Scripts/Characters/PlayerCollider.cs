using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private PlayerLife playerLife;

    private void Start()
    {
        playerLife = GetComponent<PlayerLife>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AvailableDamangeAndDeath(collision);
        GameClear(collision);
    }

    /// <summary>
    /// 지속적을 충돌히거 있는 오브젝트의 레이어를 검사하여 장애물인지 검사하여 장애물이면 데지미를 주고
    /// 동시에 라이프 카운트가 0이 되면 사망 판정을 하여 씬을 재시작하는 코루틴 함수.
    /// 콜라이더 엔터에 호출.
    /// </summary>
    /// <param name="ob"></param>
    /// <returns></returns>
    private void AvailableDamangeAndDeath(Collider2D ob)
    {
        if (ob.gameObject.layer == 7)
        {
            Debug.Log("장애물");
            StartCoroutine(playerLife.Damage());
            playerLife.isDamage = false;

            if (PlayerLife.lifeCount <= 0)
            {
                Debug.Log("사망 판정");
                StartCoroutine(playerLife.Death());
            }
        }
    }

    /// <summary>
    /// 충돌한 오브젝트의 레이어를 검사하여 해당 오브젝트가 클리어 오브젝트면 엔딩씬으로 이동.
    /// </summary>
    /// <param name="ob"></param>
    private void GameClear(Collider2D ob)
    {
        if (ob.gameObject.layer == 8)
        {
            Debug.Log("클리어");
            SceneManager.LoadScene(2);
        }
    }
}
