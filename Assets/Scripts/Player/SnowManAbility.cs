using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManAbility : MonoBehaviour
{
    [SerializeField] protected StageManager stageManager;

    void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
    }
}
