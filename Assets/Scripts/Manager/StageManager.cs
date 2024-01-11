using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class StageManager : MonoBehaviour
{
    public Transform player;
    public List<TextMeshProUGUI> distanceText;
    public List<TextMeshProUGUI> scoreText;

    public static event Action<int> OnScoreIncreased;
    public int score;

    public void Awake()
    {
        score = 0;
        GameManager.instance.stageManager = this;
    }

    private void Update()
    {
        UpdateDistance();
        UpdateScore();
    }

    // ���� �Ÿ� ����
    void UpdateDistance()
    {
        float distanceTravelled = player.position.x;
        distanceText[0].text = distanceTravelled.ToString("F0") + " m";
        distanceText[1].text = distanceTravelled.ToString("F0") + "m";
    }

    void UpdateScore()
    {
        scoreText[0].text = score.ToString();
        scoreText[1].text = score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        // ���� ���� ����
        score += amount;
        // UI ������Ʈ ���� �߰�

        // �̺�Ʈ �߻�
        OnScoreIncreased?.Invoke(score);
    }

}
