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
    /// �������� �浹���� �ִ� ������Ʈ�� ���̾ �˻��Ͽ� ��ֹ����� �˻��Ͽ� ��ֹ��̸� �����̸� �ְ�
    /// ���ÿ� ������ ī��Ʈ�� 0�� �Ǹ� ��� ������ �Ͽ� ���� ������ϴ� �ڷ�ƾ �Լ�.
    /// �ݶ��̴� ���Ϳ� ȣ��.
    /// </summary>
    /// <param name="ob"></param>
    /// <returns></returns>
    private void AvailableDamangeAndDeath(Collider2D ob)
    {
        if (ob.gameObject.layer == 7)
        {
            Debug.Log("��ֹ�");
            StartCoroutine(playerLife.Damage());
            playerLife.isDamage = false;

            if (PlayerLife.lifeCount <= 0)
            {
                Debug.Log("��� ����");
                StartCoroutine(playerLife.Death());
            }
        }
    }

    /// <summary>
    /// �浹�� ������Ʈ�� ���̾ �˻��Ͽ� �ش� ������Ʈ�� Ŭ���� ������Ʈ�� ���������� �̵�.
    /// </summary>
    /// <param name="ob"></param>
    private void GameClear(Collider2D ob)
    {
        if (ob.gameObject.layer == 8)
        {
            Debug.Log("Ŭ����");
            SceneManager.LoadScene(2);
        }
    }
}
