using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OverlayManager : MonoBehaviour
{
    [Header("오버레이")]

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

    // 옵션 오버레이를 켜고 끄는 메서드
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

    // 상점 오버레이를 켜고 끄는 메서드
    public void ToggleShopOverlay()
    {
        GameManager.Vibrate();

        if (shopOverlay != null)
        {
            shopOverlay.SetActive(!shopOverlay.activeSelf);
        }

        // 상점 오버레이를 켤 때 옵션 오버레이가 켜져 있다면 끕니다.
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

    // 게임 오버 오버레이를 켜고 끄는 메서드
    public void ToggleGameOverOverlay()
    {
        if (gameOverOverlay != null)
        {
            gameOverOverlay.SetActive(true);
        }
    }

    // 일시정지 오버레이를 켜고 끄는 메서드
    public void TogglePauseOverlay()
    {
        GameManager.Vibrate();

        if (pauseOverlay != null && gameOverOverlay.activeSelf == false)
        {
            pauseOverlay.SetActive(!pauseOverlay.activeSelf);
        }
    }

    // 튜토리얼 오버레이를 켜고 끄는 메서드
    public void ToggleTutorialOverlay()
    {
        if (tutorialOverlay != null && GameManager.instance.isTutorialEnabled == true)
        {
            tutorialOverlay.SetActive(true);
        }
    }

    // 뒤로가기 버튼을 눌렀을 때 모든 오버레이를 끄는 메서드
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

    // 게임 종료 버튼을 눌렀을 때 게임을 종료하는 메서드
    public void ToggleExitButton()
    {
        GameManager.Vibrate();

        // 모바일 디바이스에서는 백 버튼을 눌렀을 때의 기본 동작을 따름
        #if UNITY_ANDROID || UNITY_IOS
        // 모바일 플랫폼에서는 애플리케이션을 백그라운드로 보내는 것을 권장
        Application.runInBackground = true;
        #else
        // PC, 맥, 리눅스에서는 게임을 종료
        Application.Quit();
        #endif
    }

    // 게임 오버 오버레이에서 다시하기 버튼을 눌렀을 때 메서드
    public void ToggleGoToReTryButton()
    {
        GameManager.Vibrate();
        SceneManager.LoadScene(1);
    }

    // 게임 오버 오버레이에서 메인으로 버튼을 눌렀을 때 메서드
    public void ToggleGoToIntroButton()
    {
        GameManager.Vibrate();
        SceneManager.LoadScene(0);
    }

    // 옵션 오버레이 또는 상점 오버레이가 활성화되어 있는지 확인하는 메서드
    public bool IsAnyOverlayActive()
    {
        return (optionsOverlay != null && optionsOverlay.activeSelf) || (shopOverlay != null && shopOverlay.activeSelf);
    }

    // 일시정지 오버레이가 활성화되어 있는지 확인하는 메서드
    public bool IsPauseOverlayActive()
    {
        return (pauseOverlay != null && pauseOverlay.activeSelf);
    }

    // 튜토리얼 오버레이가 활성화되어 있는지 확인하는 메서드
    public bool IsTutorialOverlayActive()
    {
        return (tutorialOverlay != null && tutorialOverlay.activeSelf);
    }
}