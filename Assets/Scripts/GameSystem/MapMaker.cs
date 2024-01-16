using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public GameObject startPrefab; // 생성할 초기 맵 프리팹
    public List<GameObject> randomPrefabs; // 생성할 맵 프리팹들의 리스트
    public GameObject icecreamPrefab; // 생성할 아이스크림 프리팹

    public float spawnInterval = 2f; // 맵 조각과 아이스크림이 생성되는 간격(초)

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

        // 프리팹의 모든 자식 오브젝트 순회
        foreach (Transform child in prefab.transform)
        {
            EdgeCollider2D collider = child.GetComponent<EdgeCollider2D>();
            if (collider != null)
            {
                // 엣지 콜라이더의 모든 포인트를 검사하여 x 좌표가 가장 큰 값을 찾음
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

        // 가장 큰 x 값을 반환
        return rightmostX;
    }

    void PlaceIceCream(float xPosition)
    {
        Vector3 iceCreamPosition = new Vector3(xPosition, 1.5f, 0);
        Instantiate(icecreamPrefab, iceCreamPosition, Quaternion.identity);
    }
}
