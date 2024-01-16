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
        // ���� �����ӿ����� ȸ�� ������ �߰�
        rotationDegree += Mathf.Abs(transform.localEulerAngles.z);

        // �� ������ ���Ҵ��� Ȯ�� (360��)
        if (rotationDegree >= 360f)
        {
            // ���� �߰�
            Debug.Log("�߰�");

            // ȸ�� ���� �ʱ�ȭ
            rotationDegree = 0;
        }
    }
}