using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static GameManager;

public class StageManager : MonoBehaviour
{
    public Transform player;
    public List<TextMeshProUGUI> distanceText;
    public List<TextMeshProUGUI> scoreText;
    public GameObject[] characterPrefabs;

    public static event Action<int> OnScoreIncreased;
    public int score;

    public OverlayManager overlayManager;

    public void Awake()
    {
        score = 0;

        if(GameManager.instance != null)
        {
            GameManager.instance.stageManager = this;
        }

        overlayManager = FindObjectOfType<OverlayManager>();
    }

    private void Start()
    {
        InstantiateSelectedCharacter();

    }

    private void Update()
    {
        UpdateDistance();
        UpdateScore();
    }

    private void InstantiateSelectedCharacter()
    {
        SnowManType selectedType = GameManager.instance.GetSelectedSnowMan();
        int index = (int)selectedType; // SnowManType�� �迭 �ε����� ��ȯ

        if (index >= 0 && index < characterPrefabs.Length)
        {
            GameObject characterInstance = Instantiate(characterPrefabs[index]);
            player = characterInstance.transform; // player ���� ������Ʈ
        }
        else
        {
            Debug.LogError("���õ� ������� �����ϴ�.");
        }
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
