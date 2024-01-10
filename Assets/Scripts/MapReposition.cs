using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReposition : MonoBehaviour
{
    public GameObject startPrefab; // ������ ������
    public List<GameObject> randomPrefabs; // ������ �����յ��� ����Ʈ
    public GameObject icecreamPrefab;

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
            GameObject createdPrefab = CreatePrefab(prefabToCreate);
            CreateIcecreamAbovePrefab(createdPrefab); // ���̽�ũ�� ����
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
