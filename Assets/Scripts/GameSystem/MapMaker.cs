using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public GameObject startPrefab; // ������ �ʱ� �� ������
    public List<GameObject> randomPrefabs; // ������ �� �����յ��� ����Ʈ
    public GameObject icecreamPrefab; // ������ ���̽�ũ�� ������

    public float spawnInterval = 2f; // �� ������ ���̽�ũ���� �����Ǵ� ����(��)

    private GameObject lastPrefab;
    private GameObject ground;

    void Start()
    {
        lastPrefab = Instantiate(startPrefab, Vector3.zero, Quaternion.identity);
        StartCoroutine(SpawnMapPieces());
    }

    IEnumerator SpawnMapPieces()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            AddRandomMapPiece();
        }
    }

    void AddRandomMapPiece()
    {
        GameObject prefabToSpawn = randomPrefabs[Random.Range(0, randomPrefabs.Count)];
        float newXPosition;

        newXPosition = GetRightmostX(lastPrefab) + 60f;

        Vector3 spawnPosition = new Vector3(newXPosition, 0, 0);

        lastPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        PlaceIceCream(newXPosition);
    }

    float GetRightmostX(GameObject prefab)
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

    void PlaceIceCream(float xPosition)
    {
        Vector3 iceCreamPosition = new Vector3(xPosition, 1.5f, 0);
        Instantiate(icecreamPrefab, iceCreamPosition, Quaternion.identity);
    }
}
