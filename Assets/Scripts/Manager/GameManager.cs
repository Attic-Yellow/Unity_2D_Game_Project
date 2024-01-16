using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public enum SnowManType
    {
        �����,
        �����Ѵ����,
        Temp,
        // ����� �߰� �� ���⿡ �߰�
    }

    [Header("�÷��̾� ������")]

    public int icecream;
    public int bestScore;
    public Dictionary<SnowManType, bool> ownedSnowMans = new Dictionary<SnowManType, bool>();
    public Dictionary<SnowManType, bool> usingSnowMan = new Dictionary<SnowManType, bool>();
    public SnowManType selectedSnowManType;
    public StageManager stageManager;

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
        

        // PlayerPrefs.DeleteAll(); // ������ ������

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

    public void AddScore(int score)
    {
        stageManager.IncreaseScore(score);
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

    public void SelectSnowMan(SnowManType snowManType)
    {
        selectedSnowManType = snowManType;
    }

    // ���õ� ����� Ÿ���� �������� �޼���
    public SnowManType GetSelectedSnowMan()
    {
        return selectedSnowManType;
    }

    private void LoadData()
    {
        icecream = PlayerPrefs.GetInt("Icecream", 150);
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // ��� ������ �������
        string ownedSnowMansString = PlayerPrefs.GetString("OwnedSnowMans", "");
        if (!string.IsNullOrEmpty(ownedSnowMansString))
        {
            string[] ownedSnowMansArray = ownedSnowMansString.Split(',');
            foreach (var ownedSnowMan in ownedSnowMansArray)
            {
                if (!string.IsNullOrEmpty(ownedSnowMan))
                {
                    SnowManType snowManType = (SnowManType)Enum.Parse(typeof(SnowManType), ownedSnowMan);
                    ownedSnowMans[snowManType] = true;
                }
            }
        }

        // ���� ��� ���� �����
        string usingSnowManString = PlayerPrefs.GetString("UsingSnowMan", "");
        if (!string.IsNullOrEmpty(usingSnowManString))
        {
            string[] usingSnowManArray = usingSnowManString.Split(',');
            foreach (var item in usingSnowManArray)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] keyValue = item.Split(':');
                    if (keyValue.Length == 2 && !string.IsNullOrEmpty(keyValue[0]))
                    {
                        SnowManType snowManType = (SnowManType)Enum.Parse(typeof(SnowManType), keyValue[0]);
                        usingSnowMan[snowManType] = bool.Parse(keyValue[1]);
                    }
                }
            }
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Icecream", icecream);
        PlayerPrefs.SetInt("BestScore", bestScore);

        // ��� ������ �������
        string ownedSnowMansString = string.Join(",", ownedSnowMans.Keys.Select(k => k.ToString()).ToArray());
        PlayerPrefs.SetString("OwnedSnowMans", ownedSnowMansString);

        // ���� ��� ���� �����
        var usingSnowManItems = usingSnowMan.Select(kv => $"{kv.Key}:{kv.Value}").ToArray();
        string usingSnowManString = string.Join(",", usingSnowManItems);
        PlayerPrefs.SetString("UsingSnowMan", usingSnowManString);

        PlayerPrefs.Save();
    }

}
