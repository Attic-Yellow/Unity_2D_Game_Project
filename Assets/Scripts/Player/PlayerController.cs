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
    public GameObject gameUIObject;
    public float savedVelocityX;

    private float inputDelay = 2.0f; // �Է��� ������ �ð� (��)
    private float timeSinceSceneLoaded=0f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;

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
        timeSinceSceneLoaded = 0f; // �� �ε� �ð� �ʱ�ȭ
        rb.gravityScale = 0;
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        timeSinceSceneLoaded += Time.deltaTime; // �� �ε� �� ��� �ð� ����

        if (timeSinceSceneLoaded < inputDelay)
        {
            return;
        }

        if (!animator.GetBool("IsDead") && !overlay.IsPauseOverlayActive() && !overlay.IsTutorialOverlayActive())
        {
            Time.timeScale = 1;

            if (!isGravityChangeStarted && rb.gravityScale == 0)
            {
                if ((Input.anyKeyDown || Input.touchCount > 0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    StartCoroutine(ChangeGravityAfterDelay(3f));
                }
            }

            // �߷� ���� 0�� �ƴ� ���� HandleInput �޼��带 ȣ��
            if (rb.gravityScale != 0)
            {
                HandleInput();
            }
        }
        else if (overlay.IsPauseOverlayActive())
        {
            Time.timeScale = 0;
        }
        else if (overlay.IsTutorialOverlayActive())
        {
            Time.timeScale = 1;
        }

        if (animator.GetBool("IsDead"))
        {
            StopMovement();
            virtualCamera.Follow = null;
        }
    }

    private void FixedUpdate()
    {
        if (animator.GetBool("IsDead"))
        {
            rb.gravityScale = 5f;
            rb.velocity = new Vector2(0, 1f);
            rb.rotation = 0f;
            rb.angularVelocity = 0f;
        }
    }

    void HandleInput()
    {
        float toraue = torqueForce * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(toraue);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(-toraue);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                rb.AddTorque(toraue);
            }
            else if (touch.position.x > Screen.width / 2)
            {
                rb.AddTorque(-toraue);
            }
        }
    }

    void StopMovement()
    {
        rb.velocity = new Vector2(0, 1);
        rb.gravityScale = 0;
    }

    IEnumerator ChangeGravityAfterDelay(float delay)
    {
        print("����");

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
            countDownText.text = timeLeft.ToString("F0"); // �Ҽ��� ���� ���� ���ڷ� ǥ��
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        countDownText.gameObject.SetActive(false);
        rb.gravityScale = 3.5f;
        isGravityChangeStarted = false;
    }
}