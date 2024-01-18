using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private Toggle tutorialToggle;
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle hapticToggle;
    [SerializeField] private GameObject soundButtonImage;
    [SerializeField] private GameObject hapticButtonImage;
    [SerializeField] private GameObject soundOffIcon;
    [SerializeField] private GameObject hapticOffIcon;
    [SerializeField] private TMP_Text tutorialControllText;
    [SerializeField] private TMP_Text soundControllText;
    [SerializeField] private TMP_Text hapticControllText;


    [SerializeField] private bool isTutorialEnabled;
    [SerializeField] private bool isSoundOn;
    [SerializeField] private bool isHapticOn;

    public List<AudioSource> audioBGM;

    private void Start()
    {
        GameUI = GameObject.Find("Game UI");
        pauseOverlay = GameUI.transform.Find("Option Overlay").gameObject;
        tutorialToggle = pauseOverlay.transform.Find("Tutorial Button").GetComponent<Toggle>();
        soundToggle = pauseOverlay.transform.Find("Sound Button").GetComponent<Toggle>();
        hapticToggle = pauseOverlay.transform.Find("Haptic Button").GetComponent<Toggle>();
        soundButtonImage = soundToggle.transform.Find("Sound Button Image").gameObject;
        hapticButtonImage = hapticToggle.transform.Find("Haptic Button Image").gameObject;
        soundOffIcon = soundButtonImage.transform.Find("Sound Off Icon").gameObject;
        hapticOffIcon = hapticButtonImage.transform.Find("Haptic Off Icon").gameObject;
        tutorialControllText = tutorialToggle.transform.Find("Tutorial Controll Text").GetComponent<TMP_Text>();
        soundControllText = soundToggle.transform.Find("Sound Controll Text").GetComponent<TMP_Text>();
        hapticControllText = hapticToggle.transform.Find("Haptic Controll Text").GetComponent<TMP_Text>();

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

        if (tutorialControllText != null)
        {
            tutorialControllText.SetText(isTutorialEnabled ? "튜토리얼 On" : "튜토리얼 Off");
        }

        if (soundControllText != null)
        {
            soundControllText.SetText(isSoundOn ? "사운드 On" : "사운드 Off");
        }

        if (hapticControllText != null)
        {
            hapticControllText.SetText(isHapticOn ? "진동 On" : "진동 Off");
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

        if (tutorialControllText != null)
        {
            tutorialControllText.SetText(isTutorialEnabled ? "튜토리얼 On" : "튜토리얼 Off");
        }

        if (soundControllText != null)
        {
            soundControllText.SetText(isSoundOn ? "사운드 On" : "사운드 Off");
        }

        if (hapticControllText != null)
        {
            hapticControllText.SetText(isHapticOn ? "진동 On" : "진동 Off");
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