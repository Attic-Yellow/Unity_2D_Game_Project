using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrachDetector : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    [SerializeField] private OverlayManager gameOverOverlay;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameOverOverlay = FindObjectOfType<OverlayManager>();
    }

    private void FixedUpdate()
    { 
        if (playerController.animator.GetBool("IsDead"))
        {
            gameOverOverlay.ToggleGameOverOverlay();
        }
    }

    // 눈사람과 충돌시 사망 애니메이션 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SnowMan Body")
        {
            playerController.animator.SetBool("IsDead", true);
        }
    }
}
