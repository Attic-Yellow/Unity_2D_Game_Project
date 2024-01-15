using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public GameObject backgroundPrefab; // ��� ������
    public float spawnInterval = 2.0f; // ���� ���� (�� ����)
    public float destroyPositionX = -10.0f; // �Ҹ� ��ġ (X ��ǥ)

    void Start()
    {
        // �ڷ�ƾ ����
        StartCoroutine(SpawnAndDestroyCoroutine());
    }

    IEnumerator SpawnAndDestroyCoroutine()
    {
        while (true)
        {
            // ������ ����
            GameObject spawned = Instantiate(backgroundPrefab, transform.position, Quaternion.identity);

            // ���� �������� ���
            yield return new WaitForSeconds(spawnInterval);

            // ������ ������Ʈ�� ȭ�� ������ ������ �Ҹ�
            if (spawned.transform.position.x <= destroyPositionX)
            {
                Destroy(spawned);
            }
        }
    }
}