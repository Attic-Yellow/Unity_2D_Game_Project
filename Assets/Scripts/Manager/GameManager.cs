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
        눈사람,
        섬뜩한눈사람,
        Temp,
        // 눈사람 추가 시 여기에 추가
    }

    [Header("플레이어 데이터")]

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

        // PlayerPrefs.DeleteAll(); // 데이터 삭제용
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

    // 플레이어 데이터가 없을 시 기본값으로 초기화
    public void InitSnowMan()
    {
        isFirst = true;
        // 기본 눈사람 잠금 해제
        ownedSnowMans[SnowManType.눈사람] = true;
        usingSnowMan[SnowManType.눈사람] = true;
        SelectSnowMan(SnowManType.눈사람);
    }

    // 게임 재화 증가 메서드
    public void AddIcecream(int amount)
    {
        icecream += amount;
        SaveData();
    }

    // 게임 재화 감소 메서드
    public void RemoveIcecream(int amount)
    {
        icecream -= amount;
        icecream = Mathf.Max(icecream, 0);
        SaveData();
    }

    // 점수를 더하는 메서드
    public void AddScore(int score)
    {
        stageManager.IncreaseScore(score);
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

    // 선택된 눈사람 타입을 설정하는 메서드
    public void SelectSnowMan(SnowManType snowManType)
    {
        selectedSnowManType = snowManType;
    }

    // 선택된 눈사람 타입을 가져오는 메서드
    public SnowManType GetSelectedSnowMan()
    {
        return selectedSnowManType;
    }

    // 튜토리얼 on / off 상태를 저장하는 메서드
    public void SaveTutorialState(bool state)
    {
        isTutorialEnabled = state;
        PlayerPrefs.SetInt("TutorialEnabled", state ? 1 : 0);
        PlayerPrefs.Save();
    }

    // 튜토리얼 on / off 상태를 불러오는 메서드
    public bool LoadTutorialState()
    {
        // PlayerPrefs에 저장된 값이 없을 경우 기본값으로 true를 반환합니다.
        isTutorialEnabled = PlayerPrefs.GetInt("TutorialEnabled", 1) == 1;
        return isTutorialEnabled;
    }

    // 사운드 on / off 상태를 저장하는 메서드
    public void SaveSoundState(bool state)
    {
        isSoundOn = state;
        PlayerPrefs.SetInt("SoundOn", state ? 1 : 0);
        PlayerPrefs.Save();
    }

    // 사운드 on / off 상태를 불러오는 메서드
    public bool LoadSoundState()
    {
        // PlayerPrefs에 저장된 값이 없을 경우 기본값으로 true를 반환합니다.
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        return isSoundOn;
    }

    // 햅틱 on / off 상태를 저장하는 메서드
    public void SaveHapticState(bool state)
    {
        isHapticOn = state;
        PlayerPrefs.SetInt("HapticOn", state ? 1 : 0);
        PlayerPrefs.Save();
    }

    // 햅틱 on / off 상태를 불러오는 메서드
    public bool LoadHapticState()
    {
        // PlayerPrefs에 저장된 값이 없을 경우 기본값으로 true를 반환합니다.
        isHapticOn = PlayerPrefs.GetInt("HapticOn", 1) == 1;
        return isHapticOn;
    }


    // 플레이어 데이터 불러오는 메서드
    private void LoadData()
    {
        icecream = PlayerPrefs.GetInt("Icecream", 150);
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // 잠금 해제된 눈사람들
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

        // 현재 사용 중인 눈사람
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

    // 플레이어 데이터 저장하는 메서드
    private void SaveData()
    {
        PlayerPrefs.SetInt("Icecream", icecream);
        PlayerPrefs.SetInt("BestScore", bestScore);

        // 잠금 해제된 눈사람들
        string ownedSnowMansString = string.Join(",", ownedSnowMans.Keys.Select(k => k.ToString()).ToArray());
        PlayerPrefs.SetString("OwnedSnowMans", ownedSnowMansString);

        // 현재 사용 중인 눈사람
        var usingSnowManItems = usingSnowMan.Select(kv => $"{kv.Key}:{kv.Value}").ToArray();
        string usingSnowManString = string.Join(",", usingSnowManItems);
        PlayerPrefs.SetString("UsingSnowMan", usingSnowManString);

        PlayerPrefs.Save();
    }
}