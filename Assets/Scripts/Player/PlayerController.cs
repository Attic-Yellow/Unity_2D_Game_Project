using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float torqueForce;
    public TMP_Text countDownText;
    public float savedAngularVelocity;
    public GameObject gameUIObject;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private Vector2 savedVelocity;

    public Animator animator;
    private OverlayManager overlay;
    private bool isGravityChangeStarted = false;
    private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        overlay = FindObjectOfType<OverlayManager>();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>(); 
        gameUIObject = GameObject.Find("Game UI");
        countDownText = gameUIObject.transform.Find("Game Start Count Down").GetComponent<TMP_Text>();
        virtualCamera.Follow = this.transform;
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (!animator.GetBool("IsDead") && !overlay.IsPauseOverlayActive())
        {
            if (!isGravityChangeStarted && rb.gravityScale == 0)
            {
                if ((Input.anyKeyDown || Input.touchCount > 0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    StartCoroutine(ChangeGravityAfterDelay(3f));
                }
            }

            // 중력 값이 0이 아닐 때만 HandleInput 메서드를 호출
            if (rb.gravityScale != 0)
            {
                HandleInput();
            }
        }
        else if (overlay.IsPauseOverlayActive())
        {
            StopMovement();
        }
        else
        {
            Movement();
        }

        if (animator.GetBool("IsDead"))
        {
            StopMovement();
            virtualCamera.Follow = null;
        }

        SaveAngularVelocity();
    }

    private void FixedUpdate()
    {
        if (animator.GetBool("IsDead"))
        {
            rb.gravityScale = 30f;
        }

        Vector2 direction = transform.up * - 3f;
        Debug.DrawRay(transform.position, direction, new Color(0, 1, 1));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 3f);
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(torqueForce);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(-torqueForce);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                rb.AddTorque(torqueForce);
            }
            else if (touch.position.x > Screen.width / 2)
            {
                rb.AddTorque(-torqueForce);
            }
        }
    }

    void StopMovement()
    {
        savedVelocity = rb.velocity;
        savedAngularVelocity = rb.angularVelocity;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    void Movement()
    {
        rb.angularVelocity = savedAngularVelocity;
    }

    void SaveAngularVelocity()
    {
        savedAngularVelocity = rb.angularVelocity;
    }

    IEnumerator ChangeGravityAfterDelay(float delay)
    {
        print("시작");

        isGravityChangeStarted = true;
        float timeLeft = delay;

        while (timeLeft > 0)
        {
            if (overlay.IsPauseOverlayActive())
            {
                countDownText.gameObject.SetActive(false);
                yield return null;
                continue;
            }

            countDownText.gameObject.SetActive(true);
            countDownText.text = timeLeft.ToString("F0"); // 소수점 없는 단일 숫자로 표시
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        countDownText.gameObject.SetActive(false);
        rb.gravityScale = 3.5f;
        isGravityChangeStarted = false;
    }
}
