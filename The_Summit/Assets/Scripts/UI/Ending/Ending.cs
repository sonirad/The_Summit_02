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
    /// 엔딩씬에서 아무키 누르면 타이틀 씬으로 다시 이동
    /// </summary>
    private IEnumerator GameEnding()
    {
        Debug.Log("로딩");
        yield return new WaitUntil(() => gameEndingKey == true);
        Debug.Log("타이틀 씬으로");
        SceneManager.LoadScene(0);
    }
}
