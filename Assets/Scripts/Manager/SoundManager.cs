using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject pauseOverlay;
    public Toggle tutorialToggle;
    public Toggle soundToggle;
    public Toggle hapticToggle;
    public GameObject soundButtonImage;
    public GameObject hapticButtonImage;
    public GameObject soundOnIcon;
    public GameObject soundOffIcon;
    public GameObject hapticOnIcon;
    public GameObject hapticOffIcon;
    public List<AudioSource> audioBGM;

    public bool isTutorialEnabled;
    public bool isSoundOn;
    public bool isHapticOn;

    private void Start()
    {
        GameUI = GameObject.Find("Game UI");
        pauseOverlay = GameUI.transform.Find("Option Overlay").gameObject;
        tutorialToggle = pauseOverlay.transform.Find("Tutorial Button").GetComponent<Toggle>();
        soundToggle = pauseOverlay.transform.Find("Sound Button").GetComponent<Toggle>();
        hapticToggle = pauseOverlay.transform.Find("Haptic Button").GetComponent<Toggle>();
        soundButtonImage = soundToggle.transform.Find("Sound Button Image").gameObject;
        hapticButtonImage = hapticToggle.transform.Find("Haptic Button Image").gameObject;
        soundOnIcon = soundButtonImage.transform.Find("Sound On Icon").gameObject;
        soundOffIcon = soundButtonImage.transform.Find("Sound Off Icon").gameObject;
        hapticOnIcon = hapticButtonImage.transform.Find("Haptic On Icon").gameObject;
        hapticOffIcon = hapticButtonImage.transform.Find("Haptic Off Icon").gameObject;

        GameManager.instance.LoadTutorialState();
        GameManager.instance.LoadSoundState();
        GameManager.instance.LoadHapticState();
        isTutorialEnabled = GameManager.instance.isTutorialEnabled;
        isSoundOn = GameManager.instance.isSoundOn;
        isHapticOn = GameManager.instance.isHapticOn;

        if (tutorialToggle != null)
        {
            tutorialToggle.isOn = isTutorialEnabled;
        }

        if (soundToggle != null)
        {
            soundToggle.isOn = isSoundOn;
        }

        if(soundOffIcon != null)
        {
            soundOffIcon.SetActive(!isSoundOn);
        }

        if (hapticToggle != null)
        {
            hapticToggle.isOn = isHapticOn;
        }

        if (hapticOffIcon != null)
        {
            hapticOffIcon.SetActive(!isHapticOn);
        }
    }

    private void Update()
    {
        if (soundOffIcon != null)
        {
            soundOffIcon.SetActive(!isSoundOn);
        }

        if (hapticOffIcon != null)
        {
            hapticOffIcon.SetActive(!isHapticOn);
        }
    }

    // 튜토리얼 on / off 상태를 저장하는 메서드
    public void ToggleTutorialButton(bool value)
    {
        GameManager.Vibrate();
        GameManager.instance.SaveTutorialState(value); // GameManager를 통해 상태 저장
        isTutorialEnabled = value;
    }

    public void ToggleSoundButton(bool value)
    {
        GameManager.Vibrate();
        GameManager.instance.SaveSoundState(value); // GameManager를 통해 상태 저장
        isSoundOn = value;

        // 사운드 볼륨 업데이트
        AudioListener.volume = isSoundOn ? 1 : 0;
    }

    public void ToggleHapticButton(bool value)
    {
        GameManager.Vibrate();
        GameManager.instance.SaveHapticState(value); // GameManager를 통해 상태 저장
        isHapticOn = value;
    }
}