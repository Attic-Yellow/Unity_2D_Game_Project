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
        int index = (int)selectedType; // SnowManType을 배열 인덱스로 변환

        if (index >= 0 && index < characterPrefabs.Length)
        {
            GameObject characterInstance = Instantiate(characterPrefabs[index]);
            player = characterInstance.transform; // player 참조 업데이트
        }
        else
        {
            Debug.LogError("선택된 눈사람이 없습니다.");
        }
    }

    // 현재 거리 갱신
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
        // 점수 증가 로직
        score += amount;
        // UI 업데이트 로직 추가

        // 이벤트 발생
        OnScoreIncreased?.Invoke(score);
    }

}
