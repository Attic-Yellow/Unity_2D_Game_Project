using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneLoader : MonoBehaviour
{
    private OverlayManager optionsOverlay;

    private void Start()
    {
        optionsOverlay = FindObjectOfType<OverlayManager>();
    }

    private void Update()
    {
        // 오버레이가 비활성화된 상태인지 확인
        if (optionsOverlay != null && !optionsOverlay.IsAnyOverlayActive())
        {
            // 모바일 환경에서 첫 번째 터치 이벤트를 체크
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    GameSceneLoad();
                }
            }
            // PC 또는 기타 환경에서의 입력 체크
            else if (Input.anyKeyDown)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    GameSceneLoad();
                }
            }
        }
    }

    public void GameSceneLoad()
    {
        SceneManager.LoadScene(1);
    }
}