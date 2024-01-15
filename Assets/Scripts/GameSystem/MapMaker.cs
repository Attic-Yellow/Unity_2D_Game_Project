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

        // 프리팹의 모든 자식 오브젝트 순회
        foreach (Transform child in prefab.transform)
        {
            EdgeCollider2D collider = child.GetComponent<EdgeCollider2D>();
            if (collider != null)
            {
                // 각 오브젝트의 콜라이더 체크
                foreach (Vector2 point in collider.points)
                {
                    // 가장 오른쪽에 있는 콜라이더를 찾기
                    if (point.x > rightmostX)
                    {
                        rightmostX = point.x;
                        rightmostCollider = collider;
                    }
                }
            }
        }

        // 가장 오른쪽에 있는 콜라이더의 x 위치를 반환
        return (rightmostCollider != null) ? prefab.transform.position.x + rightmostX : 0f;
    }


    void PlaceIceCream(float xPosition)
    {
        Vector3 iceCreamPosition = new Vector3(xPosition, 1.5f, 0);
        Instantiate(icecreamPrefab, iceCreamPosition, Quaternion.identity);
    }
}
