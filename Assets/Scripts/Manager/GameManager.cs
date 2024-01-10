using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("�÷��̾� ������")]

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

    // ���� ��ȭ ����
    public void AddIcecream(int amount)
    {
        icecream += amount;
        SaveData();
    }

    // ���� ��ȭ ����
    public void RemoveIcecream(int amount)
    {
        icecream -= amount;
        icecream = Mathf.Max(icecream, 0);
        SaveData();
    }

    // �ְ� ���� �� �� ����
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

        // ��� ������ �������
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

        // ��� ������ �������
        string ownedSnowMansString = string.Join(",", ownedSnowMans.Keys.ToArray());
        PlayerPrefs.SetString("OwnedSnowMans", ownedSnowMansString);

        PlayerPrefs.Save();
    }
}
