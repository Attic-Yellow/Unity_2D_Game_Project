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
    public bool isTutorialEnabled;
    public bool isSoundOn;
    public bool isHapticOn;
    public StageManager stageManager;

    [SerializeField]
    private bool isFirst = false;

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
        Time.timeScale = 1;

        if(isFirst == false)
        {
            InitSnowMan();
        }

        LoadTutorialState();
        LoadSoundState();
        LoadHapticState();
        LoadData();
    }

    // �÷��̾� �����Ͱ� ���� �� �⺻������ �ʱ�ȭ
    public void InitSnowMan()
    {
        isFirst = true;
        // �⺻ ����� ��� ����
        ownedSnowMans[SnowManType.�����] = true;
        usingSnowMan[SnowManType.�����] = true;
        SelectSnowMan(SnowManType.�����);
    }

    // ���� ��ȭ ���� �޼���
    public void AddIcecream(int amount)
    {
        icecream += amount;
        SaveData();
    }

    // ���� ��ȭ ���� �޼���
    public void RemoveIcecream(int amount)
    {
        icecream -= amount;
        icecream = Mathf.Max(icecream, 0);
        SaveData();
    }

    // ������ ���ϴ� �޼���
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

    // ���õ� ����� Ÿ���� �����ϴ� �޼���
    public void SelectSnowMan(SnowManType snowManType)
    {
        selectedSnowManType = snowManType;
    }

    // ���õ� ����� Ÿ���� �������� �޼���
    public SnowManType GetSelectedSnowMan()
    {
        return selectedSnowManType;
    }

    // Ʃ�丮�� on / off ���¸� �����ϴ� �޼���
    public void SaveTutorialState(bool state)
    {
        isTutorialEnabled = state;
        PlayerPrefs.SetInt("TutorialEnabled", state ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Ʃ�丮�� on / off ���¸� �ҷ����� �޼���
    public bool LoadTutorialState()
    {
        // PlayerPrefs�� ����� ���� ���� ��� �⺻������ true�� ��ȯ�մϴ�.
        isTutorialEnabled = PlayerPrefs.GetInt("TutorialEnabled", 1) == 1;
        return isTutorialEnabled;
    }

    // ���� on / off ���¸� �����ϴ� �޼���
    public void SaveSoundState(bool state)
    {
        isSoundOn = state;
        PlayerPrefs.SetInt("SoundOn", state ? 1 : 0);
        PlayerPrefs.Save();
    }

    // ���� on / off ���¸� �ҷ����� �޼���
    public bool LoadSoundState()
    {
        // PlayerPrefs�� ����� ���� ���� ��� �⺻������ true�� ��ȯ�մϴ�.
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        return isSoundOn;
    }

    // ��ƽ on / off ���¸� �����ϴ� �޼���
    public void SaveHapticState(bool state)
    {
        isHapticOn = state;
        PlayerPrefs.SetInt("HapticOn", state ? 1 : 0);
        PlayerPrefs.Save();
    }

    // ��ƽ on / off ���¸� �ҷ����� �޼���
    public bool LoadHapticState()
    {
        // PlayerPrefs�� ����� ���� ���� ��� �⺻������ true�� ��ȯ�մϴ�.
        isHapticOn = PlayerPrefs.GetInt("HapticOn", 1) == 1;
        return isHapticOn;
    }


    // �÷��̾� ������ �ҷ����� �޼���
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

    // �÷��̾� ������ �����ϴ� �޼���
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