using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    [Tooltip("���ξ� UI �޴�â ������Ʈ")]
    [SerializeField] private GameObject mainUIObject;
    [Tooltip("�÷��̾� ��� ���� ǥ�� �ؽ�Ʈ")]
    [SerializeField] private TMP_Text lifeCountText;
    [Tooltip("���� ȭ�鿡 ��µǰ� �ִ� lifeCount UI�� ��.")]
    private int lifeCountUINum;
    [HideInInspector] public bool mainUIKey { get; private set; }

    private void Awake()
    {
        lifeCountText.text = " x" + PlayerLife.lifeCount;
        lifeCountUINum = PlayerLife.lifeCount;  // ������ ī��Ʈ UI ������Ʈ�� ���� �ʱ�ȭ.
    }

    private void Update()
    {
        MainUIActiveKey();
        MainUIActivate();
        UpdateLifeCountUI();
    }

    /// <summary>
    /// ���ξ� �޴�â ���� Ű ���� �Լ�
    /// </summary>
    private void MainUIActiveKey()
    {
        mainUIKey = Input.GetKeyDown(KeyCode.Escape);
    }

    /// <summary>
    /// esc Ű�� �Է��ϸ� �޴�â Ȱ��ȭ�ϴ� �Լ�.
    /// </summary>
    private void MainUIActivate()
    {
        if (mainUIKey)
        {
            Time.timeScale = 0f;
            mainUIObject.SetActive(true);
            
            Debug.Log("Esc �޴�â Ȱ��ȭ");
        }
    }

    /// <summary>
    /// Ÿ��Ʋ ������ �̵�
    /// </summary>
    public void ReturnToTile()
    {
        Debug.Log("Ÿ��Ʋ ������ �̵�");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// �������� �ٽ� ���ư���. ���� ��� ����
    /// </summary>
    public void ReturnToGame()
    {
        Time.timeScale = 1;
        mainUIObject.SetActive(false);
        Debug.Log("���� ��� ����");
    }

    /// <summary>
    /// �÷��̾� lifeCount�� ���� ���� ȭ�鿡 ��µǰ� �ִ� ��(lifeCountUINum)��
    /// ���Ͽ� �޶������� lifeCountUINum �� ������Ʈ.
    /// </summary>
    private void UpdateLifeCountUI()
    {
        if (PlayerLife.lifeCount != lifeCountUINum)
        {
            lifeCountUINum = PlayerLife.lifeCount;
            lifeCountText.text = " x" + lifeCountUINum;
            Debug.Log("���� ������ ī��Ʈ : " + PlayerLife.lifeCount);
        }

        else
        {
            Debug.Log("���� lifeCount ���� ����.");
        }
    }
}
