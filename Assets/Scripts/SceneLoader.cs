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
        // �������̰� ��Ȱ��ȭ�� �������� Ȯ��
        if (optionsOverlay != null && !optionsOverlay.IsAnyOverlayActive())
        {
            // ����� ȯ�濡�� ù ��° ��ġ �̺�Ʈ�� üũ
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    GameSceneLoad();
                    return;
                }
            }
            // PC �Ǵ� ��Ÿ ȯ�濡���� �Է� üũ
            else if (Input.anyKeyDown)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    GameSceneLoad();
                    return;
                }
            }
        }
    }

    public void GameSceneLoad()
    {
        SceneManager.LoadScene(1);
    }
}