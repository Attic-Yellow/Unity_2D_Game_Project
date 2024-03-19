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
            // 레이캐스트가 아무것도 충돌하지 않았을 경우
            transform.position = new Vector3(transform.position.x, 10f, transform.position.z);
        }
    }

    // 눈사람과 충돌 시 아이스크림과 점수 증가
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
