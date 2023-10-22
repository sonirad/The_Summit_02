using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    public bool mainUIKey { get; private set; }
    [Tooltip("���ξ� UI �޴�â ������Ʈ")]
    [SerializeField] private GameObject mainUIObject;
    [Tooltip("�÷��̾� ��� ���� ǥ�� �ؽ�Ʈ")]
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
    /// ���ξ� �޴�â ���� Ű ���� �Լ�
    /// </summary>
    private void MainUI_InputKey()
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
            mainUIObject.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Esc �޴�â Ȱ��ȭ");
        }
    }

    /// <summary>
    /// Ÿ��Ʋ ������ ����
    /// </summary>
    public void ReturnToTile()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// �������� �ٽ� ���ư���
    /// </summary>
    public void ReturnToGame()
    {
        mainUIObject.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// �÷��̾� �������� �԰ų� ��������� ������ ���� UI ������Ʈ.
    /// ���� ������ ������ �ȵǼ� ȭ�鿡 ����� �ȵ�.
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
