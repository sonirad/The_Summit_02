using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    /// <summary>
    /// 타이틀 메뉴의 상태를 둘로 나타냄. 시작과 종료의 상태에 따라 코드를 실행할 수 있게 설정.
    /// </summary>
    public enum TitleUIState
    {
        NewGame,
        ExitGame
    }

    [Tooltip("실행할 타이틀 ui의 상태 선택")]
     public TitleUIState state;

    /// <summary>
    /// state 변수로 주는 UI의 상태에 따라 맞는 시작과 종료를 실행.
    /// </summary>
    public void TitleUIFuunction()
    {
        switch (state)
        {
            case TitleUIState.NewGame:
                SceneManager.LoadScene(1);
                Debug.Log("새게임");
                break;

            case TitleUIState.ExitGame:
                Application.Quit();
                Debug.Log("종료");
                break;
        }
    }
}
