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

        if (lastPrefab == startPrefab)
        {
            newXPosition = GetRightmostX(lastPrefab) + 110f;
        }
        else
        {
            newXPosition = GetRightmostX(lastPrefab) + 90f;
        }
        Vector3 spawnPosition = new Vector3(newXPosition, 0, 0);

        lastPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        PlaceIceCream(newXPosition);
    }

    float GetRightmostX(GameObject prefab)
    {
        EdgeCollider2D rightmostCollider = null;
        float rightmostX = 0f;

        // �������� ��� �ڽ� ������Ʈ ��ȸ
        foreach (Transform child in prefab.transform)
        {
            EdgeCollider2D collider = child.GetComponent<EdgeCollider2D>();
            if (collider != null)
            {
                // �� ������Ʈ�� �ݶ��̴� üũ
                foreach (Vector2 point in collider.points)
                {
                    // ���� �����ʿ� �ִ� �ݶ��̴��� ã��
                    if (point.x > rightmostX)
                    {
                        rightmostX = point.x;
                        rightmostCollider = collider;
                    }
                }
            }
        }

        // ���� �����ʿ� �ִ� �ݶ��̴��� x ��ġ�� ��ȯ
        return (rightmostCollider != null) ? prefab.transform.position.x + rightmostX : 0f;
    }


    void PlaceIceCream(float xPosition)
    {
        Vector3 iceCreamPosition = new Vector3(xPosition, 1.5f, 0);
        Instantiate(icecreamPrefab, iceCreamPosition, Quaternion.identity);
    }
}
