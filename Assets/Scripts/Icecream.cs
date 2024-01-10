using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icecream : MonoBehaviour
{
    public PlayerController playerController;

    private void Start()
    {

    }

    private void Update()
    {
        if (playerController.animator.GetBool("IsDead"))
        {
            // surface effecter is not working

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ice Cream")
        {
            Destroy(collision.gameObject);
        }
    }
}
