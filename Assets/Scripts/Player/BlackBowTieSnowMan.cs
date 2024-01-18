using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBowTieSnowMan : SnowManAbility
{
    private void FixedUpdate()
    {
        Vector2 downRay = Vector2.right * 1f;
        Vector2 downRayStart = transform.position + Vector3.down * 5.5f;
        Debug.DrawRay(downRayStart, downRay, new Color(1, 0, 1));
        RaycastHit2D downRayHit = Physics2D.Raycast(downRayStart, downRay, 1f);

        if(GameManager.instance.selectedSnowManType == GameManager.SnowManType.검은나비)
        {
            if (downRayHit.collider != null)
            {
                Time.timeScale = 1.5f;
                print($"{Time.timeScale} : 1.1배 적용");
            }
            else
            {
                Time.timeScale = 1f;
                print($"{Time.timeScale} : 원상복귀");
            }
        }
        
    }
}
