using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public GameObject backgroundPrefab; // 배경 프리팹
    public float spawnInterval = 2.0f; // 생성 간격 (초 단위)
    public float destroyPositionX = -10.0f; // 소멸 위치 (X 좌표)

    void Start()
    {
        // 코루틴 시작
        StartCoroutine(SpawnAndDestroyCoroutine());
    }

    IEnumerator SpawnAndDestroyCoroutine()
    {
        while (true)
        {
            // 프리팹 생성
            GameObject spawned = Instantiate(backgroundPrefab, transform.position, Quaternion.identity);

            // 다음 생성까지 대기
            yield return new WaitForSeconds(spawnInterval);

            // 생성된 오브젝트가 화면 밖으로 나가면 소멸
            if (spawned.transform.position.x <= destroyPositionX)
            {
                Destroy(spawned);
            }
        }
    }
}