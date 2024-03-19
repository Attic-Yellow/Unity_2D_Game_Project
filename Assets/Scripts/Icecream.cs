using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Icecream : MonoBehaviour
{

    private void Update()
    {
        Vector2 rayStart = transform.position + Vector3.down * 1.5f;
        Debug.DrawRay(rayStart, Vector2.down * 5f, new Color(1, 0, 1));
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 5f);

        if (hit.collider == null)
        {
            // ����ĳ��Ʈ�� �ƹ��͵� �浹���� �ʾ��� ���
            transform.position = new Vector3(transform.position.x, 10f, transform.position.z);
        }
    }

    // ������� �浹 �� ���̽�ũ���� ���� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SnowMan Body"))
        {
            GameManager.instance.AddIcecream(1);
            GameManager.instance.AddScore(100);
            Destroy(gameObject);
        }
    }
}
