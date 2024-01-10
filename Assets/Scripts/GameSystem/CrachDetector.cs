using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrachDetector : MonoBehaviour
{

    public PlayerController playerController;
    public OverlayManager gameOverOverlay;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameOverOverlay = GameObject.FindGameObjectWithTag("OverlayManager").GetComponent<OverlayManager>();
    }

    private void FixedUpdate()
    { 
        if (playerController.animator.GetBool("IsDead"))
        {
            gameOverOverlay.ToggleGameOverOverlay();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SnowMan Body")
        {
            playerController.animator.SetBool("IsDead", true);
            
        }
    }
}
