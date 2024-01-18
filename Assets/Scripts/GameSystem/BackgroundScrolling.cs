using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public GameObject cam;

    [SerializeField]
    private float parallaxEffect;
    private float xPosition;
    private float length;

    void Start()
    {
        cam = GameObject.Find("VC Follow Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    private void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        if (distanceMoved > xPosition + length)
        {
            xPosition += length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition -= length;
        }
    }

}