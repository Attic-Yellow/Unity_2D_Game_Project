using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float torqueForce;
    public TMP_Text countDownText;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private Vector3 lastVelocity;
    public Animator animator;
    private OverlayManager optionsOverlay;
    private bool isGravityChangeStarted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        optionsOverlay = FindObjectOfType<OverlayManager>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (!animator.GetBool("IsDead") && !optionsOverlay.IsAnyOverlayActive() && optionsOverlay != null)
        {
            if (optionsOverlay != null && !isGravityChangeStarted && rb.gravityScale == 0)
            {
                if (Input.anyKey || Input.touchCount > 0)
                {
                    StartCoroutine(ChangeGravityAfterDelay(3f));
                }
            }

            // 중력 값이 0이 아닐 때만 HandleInput 메서드를 호출
            if (rb.gravityScale != 0)
            {
                HandleInput();
            }

            lastVelocity = rb.velocity;

        }
        else
        {
            StopMovement();
        }
    }

    private void FixedUpdate()
    {
        if (animator.GetBool("IsDead"))
        {
            rb.gravityScale = 25f;
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
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    IEnumerator ChangeGravityAfterDelay(float delay)
    {
        isGravityChangeStarted = true;
        countDownText.gameObject.SetActive(true);
        float timeLeft = delay;

        while (timeLeft > 0)
        {
            countDownText.text = timeLeft.ToString("F0"); // 소수점 없는 단일 숫자로 표시
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        countDownText.gameObject.SetActive(false);
        rb.gravityScale = 2.5f;
        isGravityChangeStarted = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float speed = lastVelocity.magnitude;
        Vector3 direction = lastVelocity.normalized;
        rb.velocity = direction * speed;
    }
}
