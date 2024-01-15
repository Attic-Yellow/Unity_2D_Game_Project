using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Icecream : MonoBehaviour
{

    private void Start()
    {
    }

    private void Update()
    {

    }

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
