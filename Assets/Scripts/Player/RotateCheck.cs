using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RotateCheck : MonoBehaviour
{
    private float rotationDegree = 0;

    void Update()
    {
        // 현재 프레임에서의 회전 각도를 추가
        rotationDegree += Mathf.Abs(transform.localEulerAngles.z);

        // 한 바퀴를 돌았는지 확인 (360도)
        if (rotationDegree >= 360f)
        {
            // 점수 추가
            Debug.Log("추가");

            // 회전 각도 초기화
            rotationDegree = 0;
        }
    }
}