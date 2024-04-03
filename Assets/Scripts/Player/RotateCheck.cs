using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RotateCheck : MonoBehaviour
{
    public bool isRightRotate;
    public bool isLeftRotate;
    public bool isUpRotate;
    int count = 0;

    void FixedUpdate()
    {
        Vector2 rightRay = Vector2.right * 3f;
        Vector2 rightRayStart = transform.position + Vector3.right * 5.5f;
        Debug.DrawRay(rightRayStart, rightRay, new Color(1, 0, 1));
        RaycastHit2D rightRayHit = Physics2D.Raycast(rightRayStart, rightRay, 3f);

        Vector2 leftRay = Vector2.left * 3f;
        Vector2 leftRayStart = transform.position + Vector3.left * 5.5f;
        Debug.DrawRay(leftRayStart, leftRay, new Color(1, 0, 1));
        RaycastHit2D leftRayHit = Physics2D.Raycast(leftRayStart, leftRay, 3f);

        Vector2 upRay = Vector2.up * 3f;
        Vector2 upRayStart = transform.position + Vector3.up * 5.5f;
        Debug.DrawRay(upRayStart, upRay, new Color(1, 0, 1));
        RaycastHit2D upRayHit = Physics2D.Raycast(upRayStart, upRay, 3f);

        Vector2 downRay = Vector2.down * 1f;
        Vector2 downRayStart = transform.position + Vector3.down * 5.5f;
        Debug.DrawRay(downRayStart, downRay, new Color(1, 0, 1));
        RaycastHit2D downRayHit = Physics2D.Raycast(downRayStart, downRay, 1f);

        if(rightRayHit.collider != null)
        {
            isRightRotate = true;
        }

        if(leftRayHit.collider != null)
        {
            isLeftRotate = true;
        }

        if(upRayHit.collider != null)
        {
            isUpRotate = true;
        }

        if(downRayHit.collider != null)
        {
            isRightRotate = false;
            isLeftRotate = false;
            isUpRotate = false;
        }

        if(isRightRotate && isLeftRotate && isUpRotate)
        {
            count++;
            print($"{count}번째 회전 : 점수 증가\n");
            GameManager.instance.AddIcecream(3);
            GameManager.instance.AddScore(300);
            isRightRotate = false;
            isLeftRotate = false;
            isUpRotate = false;
        }
    }
}