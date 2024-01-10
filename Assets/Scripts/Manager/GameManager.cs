using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("플레이어 데이터")]

    public int icecream;
    public int bestScore;
    public Dictionary<string, bool> ownedSnowMans = new Dictionary<string, bool>();
    public string currentlyUsingSnowMan;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        LoadData();
    }

    // 게임 재화 증가
    public void AddIcecream(int amount)
    {
        icecream += amount;
        SaveData();
    }

    // 게임 재화 감소
    public void RemoveIcecream(int amount)
    {
        icecream -= amount;
        icecream = Mathf.Max(icecream, 0);
        SaveData();
    }

    // 최고 점수 비교 후 갱신
    public void UpdateBestScore(int newScore)
    {
        if (newScore > bestScore)
        {
            bestScore = newScore;
            SaveData();
        }
    }

    private void LoadData()
    {
        icecream = PlayerPrefs.GetInt("Icecream", 150);
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // 잠금 해제된 눈사람들
        string ownedSnowMansString = PlayerPrefs.GetString("OwnedSnowMans", "");
        string[] ownedSnowMansArray = ownedSnowMansString.Split(',');

        foreach (var ownedSnowMan in ownedSnowMansArray)
        {
            ownedSnowMans.Add(ownedSnowMan, true);
        }
    }


    private void SaveData()
    {
        PlayerPrefs.SetInt("Icecream", icecream);
        PlayerPrefs.SetInt("BestScore", bestScore);

        // 잠금 해제된 눈사람들
        string ownedSnowMansString = string.Join(",", ownedSnowMans.Keys.ToArray());
        PlayerPrefs.SetString("OwnedSnowMans", ownedSnowMansString);

        PlayerPrefs.Save();
    }
}
