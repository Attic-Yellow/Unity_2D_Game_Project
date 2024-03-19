using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReposition : MonoBehaviour
{
    public GameObject startPrefab; // 생성할 프리팹
    public List<GameObject> randomPrefabs; // 생성할 프리팹들의 리스트
    public GameObject icecreamPrefab;

    public float initialXPosition = 0f; // 초기 X 좌표
    public float xPositionIncrement = 200f; // X 좌표 증가량
    public float creationInterval = 2f; // 생성 간격 (초)

    private float currentXPosition; // 현재 X 좌표

    private void Start()
    {
        currentXPosition = initialXPosition;
        CreatePrefab(startPrefab); // 초기 프리팹 생성
        StartCoroutine(CreatePrefabsContinuously()); // 코루틴 시작
    }

    private IEnumerator CreatePrefabsContinuously()
    {
        while (true)
        {
            // 랜덤 프리팹 생성
            GameObject prefabToCreate = ChooseRandomPrefab();
            GameObject createdPrefab = CreatePrefab(prefabToCreate);
            CreateIcecreamAbovePrefab(createdPrefab); // 아이스크림 생성
            yield return new WaitForSeconds(creationInterval); // 일정 시간 대기
        }
    }

    private GameObject ChooseRandomPrefab()
    {
        if (randomPrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, randomPrefabs.Count);
            return randomPrefabs[randomIndex];
        }
        else
        {
            return null;
        }
    }

    private GameObject CreatePrefab(GameObject prefab)
    {
        if (prefab != null)
        {
            Vector3 creationPosition = new Vector3(currentXPosition, 0f, transform.position.z);
            GameObject createdPrefab = Instantiate(prefab, creationPosition, Quaternion.identity);
            currentXPosition += xPositionIncrement;
            return createdPrefab;
        }
        return null;
    }

    private void CreateIcecreamAbovePrefab(GameObject prefab)
    {
        if (prefab != null && icecreamPrefab != null)
        {
            Vector3 icecreamPosition = new Vector3(prefab.transform.position.x, 2f, prefab.transform.position.z);
            Instantiate(icecreamPrefab, icecreamPosition, Quaternion.identity);
        }
    }
}
