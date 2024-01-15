using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpeedConcierge : MonoBehaviour
{
    public float speed = 20f;
    private SurfaceEffector2D surfaceEffector2D;

    // ����ĳ��Ʈ�� ���� ����
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
            // ��� ���� ���
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            // ��� ������ ���� �ӵ� ����
            speed = CalculateSpeedBasedOnSlope(slopeAngle);

            // ������ �ӵ� ����
            surfaceEffector2D.speed = speed;
        }
    }

    float CalculateSpeedBasedOnSlope(float slopeAngle)
    {
        // ��� ������ ���� �ӵ��� �����ϴ� ������ ���⿡ �ۼ��մϴ�.
        // ����: ��簡 ���ĸ����� �ӵ� ����
        // �� �κ��� ������ �����ΰ� �÷��� ��Ŀ� ���� �޶��� �� �ֽ��ϴ�.
        return 20f + slopeAngle * 0.5f; // ���� ����
    }

    public void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }
}
