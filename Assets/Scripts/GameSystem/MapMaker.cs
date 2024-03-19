using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    [SerializeField] private GameObject startPrefab; // ������ �ʱ� �� ������
    [SerializeField] private List<GameObject> randomPrefabs; // ������ �� �����յ��� ����Ʈ
    [SerializeField] private GameObject icecreamPrefab; // ������ ���̽�ũ�� ������

    [SerializeField] private float spawnInterval = 0.5f; // �� ������ ���̽�ũ���� �����Ǵ� ����(��)
    [SerializeField] private float iceCreamOffsetRange = 70f; // ���̽�ũ���� �����Ǵ� x ��ǥ�� ����

    [SerializeField] private GameObject lastPrefab;
    [SerializeField] private GameObject ground;

    private void Start()
    {
        lastPrefab = Instantiate(startPrefab, Vector3.zero, Quaternion.identity);
        StartCoroutine(SpawnMapPieces());
    }

    // �� ������ ���̽�ũ���� �����ϴ� �ڷ�ƾ
    IEnumerator SpawnMapPieces()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            AddRandomMapPiece();
        }
    }

    // ������ �� ������ �����ϴ� �Լ�
    private void AddRandomMapPiece()
    {
        GameObject prefabToSpawn = randomPrefabs[Random.Range(0, randomPrefabs.Count)];
        float newXPosition;

        newXPosition = GetRightmostX(lastPrefab) + 60f;

        Vector3 spawnPosition = new Vector3(newXPosition, 0, 0);

        lastPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        PlaceIceCream(newXPosition);
    }

    // �������� ��� ���� �ݶ��̴��� x ��ǥ �� ���� ū ���� ��ȯ�ϴ� �Լ�
    private float GetRightmostX(GameObject prefab)
    {
        float rightmostX = 0f;

        // �������� ��� �ڽ� ������Ʈ ��ȸ
        foreach (Transform child in prefab.transform)
        {
            EdgeCollider2D collider = child.GetComponent<EdgeCollider2D>();
            if (collider != null)
            {
                // ���� �ݶ��̴��� ��� ����Ʈ�� �˻��Ͽ� x ��ǥ�� ���� ū ���� ã��
                foreach (Vector2 point in collider.points)
                {
                    float worldPointX = child.transform.position.x + point.x;
                    if (worldPointX > rightmostX)
                    {
                        rightmostX = worldPointX;
                    }
                }
            }
        }

        // ���� ū x ���� ��ȯ
        return rightmostX;
    }

    // ���̽�ũ���� �����ϴ� �Լ�
    private void PlaceIceCream(float xPosition)
    {
        float randomOffset = Random.Range(-iceCreamOffsetRange, iceCreamOffsetRange);
        Vector3 iceCreamPosition = new Vector3(xPosition + randomOffset, 1.5f, 0);
        Instantiate(icecreamPrefab, iceCreamPosition, Quaternion.identity);
    }
}
