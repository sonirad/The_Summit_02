using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private bool gameEndingKey;

    private void Awake()
    {
        StartCoroutine(GameEnding());
    }

    void Update()
    {
        gameEndingKey = Input.anyKeyDown;
        Debug.Log(gameEndingKey);
        //StartCoroutine(GameEnding());
    }

    /// <summary>
    /// ���������� �ƹ�Ű ������ Ÿ��Ʋ ������ �ٽ� �̵�
    /// </summary>
    private IEnumerator GameEnding()
    {
        Debug.Log("�ε�");
        yield return new WaitUntil(() => gameEndingKey == true);
        Debug.Log("Ÿ��Ʋ ������");
        SceneManager.LoadScene(0);
    }
}
