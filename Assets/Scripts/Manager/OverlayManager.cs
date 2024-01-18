using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OverlayManager : MonoBehaviour
{
    [Header("��������")]

    [SerializeField] private GameObject optionsOverlay;
    [SerializeField] private GameObject shopOverlay;
    [SerializeField] private GameObject ExitOverlay;
    [SerializeField] private GameObject gameOverOverlay;
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject tutorialOverlay;

    private void Start()
    {

        if (optionsOverlay != null)
        {
            optionsOverlay.SetActive(false);
        }

        if(shopOverlay != null)
        {
            shopOverlay.SetActive(false);
        }

        if(ExitOverlay != null)
        {
            ExitOverlay.SetActive(false);
        }

        if(gameOverOverlay != null)
        {
            gameOverOverlay.SetActive(false);
        }

        if(pauseOverlay != null)
        {
            pauseOverlay.SetActive(false);
        }

        if (tutorialOverlay != null)
        {
            tutorialOverlay.SetActive(GameManager.instance.isTutorialEnabled);
        }
    }

    // �ɼ� �������̸� �Ѱ� ���� �޼���
    public void ToggleOptionsOverlay()
    {
        GameManager.Vibrate();

        if (optionsOverlay != null)
        {
            optionsOverlay.SetActive(!optionsOverlay.activeSelf);
        }

        if(shopOverlay != null && shopOverlay.activeSelf)
        {
            shopOverlay.SetActive(false);
        }

        if (ExitOverlay != null && ExitOverlay.activeSelf)
        {
            ExitOverlay.SetActive(false);
        }
    }

    // ���� �������̸� �Ѱ� ���� �޼���
    public void ToggleShopOverlay()
    {
        GameManager.Vibrate();

        if (shopOverlay != null)
        {
            shopOverlay.SetActive(!shopOverlay.activeSelf);
        }

        // ���� �������̸� �� �� �ɼ� �������̰� ���� �ִٸ� ���ϴ�.
        if (optionsOverlay != null && optionsOverlay.activeSelf)
        {
            optionsOverlay.SetActive(false);
        }

        if (ExitOverlay != null && ExitOverlay.activeSelf)
        {
            ExitOverlay.SetActive(false);
        }
    }

    public void ToggleExitOverlay()
    {
        GameManager.Vibrate();

        if (ExitOverlay != null)
        {
            ExitOverlay.SetActive(!ExitOverlay.activeSelf);
        }

        if (optionsOverlay != null && optionsOverlay.activeSelf)
        {
            optionsOverlay.SetActive(false);
        }

        if (shopOverlay != null && shopOverlay.activeSelf)
        {
            shopOverlay.SetActive(false);
        }
    }

    // ���� ���� �������̸� �Ѱ� ���� �޼���
    public void ToggleGameOverOverlay()
    {
        if (gameOverOverlay != null)
        {
            gameOverOverlay.SetActive(true);
        }
    }

    // �Ͻ����� �������̸� �Ѱ� ���� �޼���
    public void TogglePauseOverlay()
    {
        GameManager.Vibrate();

        if (pauseOverlay != null && gameOverOverlay.activeSelf == false)
        {
            pauseOverlay.SetActive(!pauseOverlay.activeSelf);
        }
    }

    // Ʃ�丮�� �������̸� �Ѱ� ���� �޼���
    public void ToggleTutorialOverlay()
    {
        if (tutorialOverlay != null && GameManager.instance.isTutorialEnabled == true)
        {
            tutorialOverlay.SetActive(true);
        }
    }

    // �ڷΰ��� ��ư�� ������ �� ��� �������̸� ���� �޼���
    public void ToggleBackButton()
    {
        GameManager.Vibrate();

        if(shopOverlay != null)
        {
            shopOverlay.SetActive(false);
        }

        if(optionsOverlay != null)
        {
            optionsOverlay.SetActive(false);
        }

        if(ExitOverlay != null)
        {
            ExitOverlay.SetActive(false);
        }

        if (pauseOverlay != null) 
        {
            pauseOverlay.SetActive(false);
        }

        if(tutorialOverlay != null)
        {
            tutorialOverlay.SetActive(false);
        }
    }

    // ���� ���� ��ư�� ������ �� ������ �����ϴ� �޼���
    public void ToggleExitButton()
    {
        GameManager.Vibrate();

        // ����� ����̽������� �� ��ư�� ������ ���� �⺻ ������ ����
        #if UNITY_ANDROID || UNITY_IOS
        // ����� �÷��������� ���ø����̼��� ��׶���� ������ ���� ����
        Application.runInBackground = true;
        #else
        // PC, ��, ������������ ������ ����
        Application.Quit();
        #endif
    }

    // ���� ���� �������̿��� �ٽ��ϱ� ��ư�� ������ �� �޼���
    public void ToggleGoToReTryButton()
    {
        GameManager.Vibrate();
        SceneManager.LoadScene(1);
    }

    // ���� ���� �������̿��� �������� ��ư�� ������ �� �޼���
    public void ToggleGoToIntroButton()
    {
        GameManager.Vibrate();
        SceneManager.LoadScene(0);
    }

    // �ɼ� �������� �Ǵ� ���� �������̰� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���ϴ� �޼���
    public bool IsAnyOverlayActive()
    {
        return (optionsOverlay != null && optionsOverlay.activeSelf) || (shopOverlay != null && shopOverlay.activeSelf);
    }

    // �Ͻ����� �������̰� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���ϴ� �޼���
    public bool IsPauseOverlayActive()
    {
        return (pauseOverlay != null && pauseOverlay.activeSelf);
    }

    // Ʃ�丮�� �������̰� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���ϴ� �޼���
    public bool IsTutorialOverlayActive()
    {
        return (tutorialOverlay != null && tutorialOverlay.activeSelf);
    }
}