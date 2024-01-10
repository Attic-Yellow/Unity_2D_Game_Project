using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlayManager : MonoBehaviour
{
    public GameObject optionsOverlay;
    public GameObject shopOverlay;
    public GameObject gameOverOverlay;

    private void Start()
    {

        if(optionsOverlay != null)
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

    public void ToggleBackButton()
    {
        shopOverlay.SetActive(false);
        optionsOverlay.SetActive(false);
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
}
