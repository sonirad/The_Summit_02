using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    public bool mainUIKey { get; private set; }
    [Tooltip("메인씬 UI 메뉴창 오브젝트")]
    [SerializeField] private GameObject mainUIObject;
    [Tooltip("플레이어 목숨 갯수 표시 텍스트")]
    [SerializeField] private TMP_Text lifeCountText;
    [SerializeField] private int lifeCountUINum;
    [SerializeField] private PlayerLife playerLife;

    private void Awake()
    {
        lifeCountText.text = " x" + PlayerLife.lifeCount;
        lifeCountUINum = PlayerLife.lifeCount;
    }

    private void Update()
    {
        MainUI_InputKey();
        MainUIActivate();
        UpdateLifeCountUI();
    }

    /// <summary>
    /// 메인씬 메뉴창 실헹 키 저장 함수
    /// </summary>
    private void MainUI_InputKey()
    {
        mainUIKey = Input.GetKeyDown(KeyCode.Escape);
    }

    /// <summary>
    /// esc 키를 입력하면 메뉴창 활성화하는 함수.
    /// </summary>
    private void MainUIActivate()
    {
        if (mainUIKey)
        {
            mainUIObject.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Esc 메뉴창 활성화");
        }
    }

    /// <summary>
    /// 타이틀 씬으로 가기
    /// </summary>
    public void ReturnToTile()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 게임으로 다시 돌아가기
    /// </summary>
    public void ReturnToGame()
    {
        mainUIObject.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// 플레이어 데미지를 입거나 사망했을때 라이프 갯수 UI 업데이트.
    /// 현재 조건이 충족이 안되서 화면에 출력이 안됨.
    /// </summary>
    private void UpdateLifeCountUI()
    {
        if (PlayerLife.lifeCount != lifeCountUINum)
        {
            Debug.Log(PlayerLife.lifeCount);
            lifeCountUINum = PlayerLife.lifeCount;
            lifeCountText.text = " x" + lifeCountUINum;
        }
    }
}
