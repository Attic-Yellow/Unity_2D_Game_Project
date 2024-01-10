using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Transform player;
    public List<TextMeshProUGUI> distanceText;

    private void Update()
    {
        UpdateDistance();
    }

    // 현재 거리 갱신
    void UpdateDistance()
    {
        float distanceTravelled = player.position.x;
        distanceText[0].text = distanceTravelled.ToString("F0") + " m";
        distanceText[1].text = distanceTravelled.ToString("F0") + "m";
    }
}
