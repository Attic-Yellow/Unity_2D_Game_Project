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
    public Toggle tutorialToggle;

    public bool isTutorialEnabled = true;

    private void Start()
    {
        isTutorialEnabled = LoadTutorialState();

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

        if(tutorialOverlay != null)
        {
            tutorialOverlay.SetActive(isTutorialEnabled);
        }

        if(tutorialToggle != null)
        {
            tutorialToggle.isOn = isTutorialEnabled;
        }
    }

    public void ToggleOptionsOverlay()
    {
        if(optionsOverlay != null)
        {
            
            optionsOverlay.SetActive(!optionsOverlay.activeSelf);
        }

        if(shopOverlay != null && shopOverlay.activeSelf)
        {
            shopOverlay.SetActive(false);
        }
    }

    public void ToggleShopOverlay()
    {
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

    public void ToggleGameOverOverlay()
    {
        if (gameOverOverlay != null)
        {
            gameOverOverlay.SetActive(true);
        }
    }

    public void TogglePauseOverlay()
    {
        if (pauseOverlay != null)
        {
            pauseOverlay.SetActive(!pauseOverlay.activeSelf);
        }
    }

    public void ToggleTutorialOverlay()
    {
        if (tutorialOverlay != null && isTutorialEnabled == true)
        {
            tutorialOverlay.SetActive(true);
        }
    }

    public void ToggleTutorialToggle(bool value)
    {
        isTutorialEnabled = value;
        SaveTutorialState(value);
        PlayerPrefs.Save();
    }

    public void ToggleBackButton()
    {
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

    public void ToggleGoToReTryButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleGoToIntroButton()
    {
        SceneManager.LoadScene(0);
    }

    public bool IsAnyOverlayActive()
    {
        return (optionsOverlay != null && optionsOverlay.activeSelf) || (shopOverlay != null && shopOverlay.activeSelf);
    }

    public bool IsPauseOverlayActive()
    {
        return pauseOverlay != null && pauseOverlay.activeSelf;
    }

    private void SaveTutorialState(bool state)
    {
        PlayerPrefs.SetInt("TutorialEnabled", state ? 1 : 0);
        
    }

    private bool LoadTutorialState()
    {
        // PlayerPrefs에 저장된 값이 없을 경우 기본값으로 true를 반환합니다.
        return PlayerPrefs.GetInt("TutorialEnabled", 1) == 1;
    }
}
