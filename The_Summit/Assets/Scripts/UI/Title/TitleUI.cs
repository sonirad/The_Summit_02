using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    /// <summary>
    /// Ÿ��Ʋ �޴��� ���¸� �ѷ� ��Ÿ��. ���۰� ������ ���¿� ���� �ڵ带 ������ �� �ְ� ����.
    /// </summary>
    public enum TitleUIState
    {
        NewGame,
        ExitGame
    }

    [Tooltip("������ Ÿ��Ʋ ui�� ���� ����")]
     public TitleUIState state;

    /// <summary>
    /// state ������ �ִ� UI�� ���¿� ���� �´� ���۰� ���Ḧ ����.
    /// </summary>
    public void TitleUIFuunction()
    {
        switch (state)
        {
            case TitleUIState.NewGame:
                SceneManager.LoadScene(1);
                Debug.Log("������");
                break;

            case TitleUIState.ExitGame:
                Application.Quit();
                Debug.Log("����");
                break;
        }
    }
}
