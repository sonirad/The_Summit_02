using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    [Tooltip("메인씬 UI 메뉴창 오브젝트")]
    [SerializeField] private GameObject mainUIObject;
    [Tooltip("플레이어 목숨 갯수 표시 텍스트")]
    [SerializeField] private TMP_Text lifeCountText;
    [Tooltip("현재 화면에 출력되고 있는 lifeCount UI의 값.")]
    private int lifeCountUINum;
    [HideInInspector] public bool mainUIKey { get; private set; }

    private void Awake()
    {
        lifeCountText.text = " x" + PlayerLife.lifeCount;
        lifeCountUINum = PlayerLife.lifeCount;  // 라이프 카운트 UI 업데이트를 위한 초기화.
    }

    private void Update()
    {
        MainUIActiveKey();
        MainUIActivate();
        UpdateLifeCountUI();
    }

    /// <summary>
    /// 메인씬 메뉴창 실헹 키 저장 함수
    /// </summary>
    private void MainUIActiveKey()
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
            Time.timeScale = 0f;
            mainUIObject.SetActive(true);
            
            Debug.Log("Esc 메뉴창 활성화");
        }
    }

    /// <summary>
    /// 타이틀 씬으로 이동
    /// </summary>
    public void ReturnToTile()
    {
        Debug.Log("타이틀 씬으로 이동");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 게임으로 다시 돌아가기. 게임 계속 진행
    /// </summary>
    public void ReturnToGame()
    {
        Time.timeScale = 1;
        mainUIObject.SetActive(false);
        Debug.Log("게임 계속 진행");
    }

    /// <summary>
    /// 플레이어 lifeCount의 값이 현재 화면에 출력되고 있는 값(lifeCountUINum)과
    /// 비교하여 달라졌을때 lifeCountUINum 값 업데이트.
    /// </summary>
    private void UpdateLifeCountUI()
    {
        if (PlayerLife.lifeCount != lifeCountUINum)
        {
            lifeCountUINum = PlayerLife.lifeCount;
            lifeCountText.text = " x" + lifeCountUINum;
            Debug.Log("현재 라이프 카운트 : " + PlayerLife.lifeCount);
        }

        else
        {
            Debug.Log("현재 lifeCount 값은 같다.");
        }
    }
}
