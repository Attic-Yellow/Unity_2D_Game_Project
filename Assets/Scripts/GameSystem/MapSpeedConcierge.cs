using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpeedConcierge : MonoBehaviour
{
    public float speed = 20f;
    private SurfaceEffector2D surfaceEffector2D;

    // 레이캐스트를 위한 설정
    public LayerMask groundLayer;
    public float rayLength = 2f;

    void Start()
    {
        surfaceEffector2D = GetComponent<SurfaceEffector2D>();
    }

    void Update()
    {
        AdjustSpeedBasedOnSlope();
    }

    void AdjustSpeedBasedOnSlope()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);

        if (hit.collider != null)
        {
            // 경사 각도 계산
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            // 경사 각도에 따른 속도 조절
            speed = CalculateSpeedBasedOnSlope(slopeAngle);

            // 조절된 속도 적용
            surfaceEffector2D.speed = speed;
        }
    }

    float CalculateSpeedBasedOnSlope(float slopeAngle)
    {
        // 경사 각도에 따라 속도를 조절하는 로직을 여기에 작성합니다.
        // 예시: 경사가 가파를수록 속도 증가
        // 이 부분은 게임의 디자인과 플레이 방식에 따라 달라질 수 있습니다.
        return 20f + slopeAngle * 0.5f; // 예시 계산식
    }

    public void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }
}
