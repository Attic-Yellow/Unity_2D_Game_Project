using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReposition : MonoBehaviour
{
    public GameObject startPrefab; // ������ ������
    public List<GameObject> randomPrefabs; // ������ �����յ��� ����Ʈ

    public float initialXPosition = 0f; // �ʱ� X ��ǥ
    public float xPositionIncrement = 200f; // X ��ǥ ������
    public float creationInterval = 2f; // ���� ���� (��)

    private float currentXPosition; // ���� X ��ǥ

    private void Start()
    {
        currentXPosition = initialXPosition;
        CreatePrefab(startPrefab); // �ʱ� ������ ����
        StartCoroutine(CreatePrefabsContinuously()); // �ڷ�ƾ ����
    }

    private IEnumerator CreatePrefabsContinuously()
    {
        while (true)
        {
            // ���� ������ ����
            GameObject prefabToCreate = ChooseRandomPrefab();
            CreatePrefab(prefabToCreate);
            yield return new WaitForSeconds(creationInterval); // ���� �ð� ���
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

    private void CreatePrefab(GameObject prefab)
    {
        if (prefab != null)
        {
            Vector3 creationPosition = new Vector3(currentXPosition, 0f, transform.position.z);
            Instantiate(prefab, creationPosition, Quaternion.identity);
            currentXPosition += xPositionIncrement;
        }
    }
}
