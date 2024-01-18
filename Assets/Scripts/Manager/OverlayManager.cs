using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OverlayManager : MonoBehaviour
{
    public GameObject optionsOverlay;
    public GameObject shopOverlay;
    public GameObject gameOverOverlay;
    public GameObject pauseOverlay;
    public GameObject tutorialOverlay;

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

        shopOverlay.SetActive(false);
        optionsOverlay.SetActive(false);

        if (pauseOverlay != null) 
        {
            pauseOverlay.SetActive(false);
        }

        if(tutorialOverlay != null)
        {
            tutorialOverlay.SetActive(false);
        }
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

    // 일시정지 오버레이와 튜토리얼 오버레이가 활성화되어 있는지 확인하는 메서드
    public bool IsPauseOverlayActive()
    {
        return (pauseOverlay != null && pauseOverlay.activeSelf);
    }

    public bool IsTutorialOverlayActive()
    {
        return (tutorialOverlay != null && tutorialOverlay.activeSelf);
    }
}
