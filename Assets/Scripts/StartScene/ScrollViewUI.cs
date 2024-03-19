using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;

public class ScrollViewUI : MonoBehaviour
{
    [SerializeField] private Transform contentTrans;
    [SerializeField] private TextMeshProUGUI icecreamText;
    [SerializeField] private GameObject snowManADProfilePrefab;
    [SerializeField] private List<GameObject> snowManProfilePrefab;
    [SerializeField] private List<GameObject> snowManProfileUI = new();
    [SerializeField] private TMP_Text icecreamLackText;
    [SerializeField] private TMP_Text snowManLockText;
    [SerializeField] private bool isFade = false;

    public void Init()
    {
        // AD 프로필 생성
        AddSnowManAD();

        AddSnowMan();

        for(int i = 0; i < 3; i++)
        {
            AddSnowManAD();
        }
    }

    private void Update()
    {
        UpdateIcecreamDisplay();
    }

    // 아이스크림 갱신
    private void UpdateIcecreamDisplay()
    {
        if (GameManager.instance != null)
        {
            icecreamText.text = GameManager.instance.icecream.ToString();
        }
    }

    // 광고 눈사람 프로필 생성
    private void AddSnowManAD()
    {
        // 프리팹 인스턴스 생성, contentTrans 자식으로 부착
        var go = Instantiate(this.snowManADProfilePrefab, this.contentTrans);
        SnowManProfileAD snowManProfileAD = go.GetComponent<SnowManProfileAD>();

        snowManProfileAD.buttonAD.onClick.AddListener(() =>
        {
            Debug.Log("광고 보기");
        });

        snowManProfileAD.unlockButton.onClick.AddListener(() =>
        {
            SnowManLockAlarm();
        });
    }

    // 눈사람 프로필 생성
    private void AddSnowMan()
    {
        foreach (var prefab in snowManProfilePrefab)
        {

            var go = Instantiate(prefab, contentTrans);
            SnowManNormalProfile snowManProfile = go.GetComponent<SnowManNormalProfile>();
            snowManProfileUI.Add(go);

            SnowManType snowManType = snowManProfile.snowManType;

            bool isOwned = GameManager.instance.ownedSnowMans.ContainsKey(snowManType);
            snowManProfile.useButton.gameObject.SetActive(isOwned);
            snowManProfile.priceButton.gameObject.SetActive(!isOwned);

            ChangeSnowMan();

            snowManProfile.useButton.onClick.AddListener(() =>
            {
                GameManager.instance.usingSnowMan[snowManType] = true;
                GameManager.instance.SelectSnowMan(snowManType);
                ChangeUseSnowMan(snowManProfile);
                ChangeSnowMan();
                GameManager.instance.DeputySave();
            });

            snowManProfile.priceButton.onClick.AddListener(() =>
            {
                int snowManPrice = int.Parse(snowManProfile.priceText.text);
                if (GameManager.instance.icecream >= snowManPrice)
                {
                    GameManager.instance.RemoveIcecream(snowManPrice);
                    GameManager.instance.SelectSnowMan(snowManType);
                    GameManager.instance.ownedSnowMans[snowManType] = true;
                    GameManager.instance.usingSnowMan[snowManType] = true;

                    snowManProfile.useButton.gameObject.SetActive(true);
                    snowManProfile.priceButton.gameObject.SetActive(false);

                    ChangeUseSnowMan(snowManProfile);
                    ChangeSnowMan();
                    GameManager.instance.DeputySave();
                }
                else
                {
                    IcecreamLackAlarm();
                }
            });
        }
    }

    // 눈사람 사용 변경
    private void ChangeUseSnowMan(SnowManNormalProfile snowManProfile)
    {
        foreach (var otherPrefab in snowManProfileUI)
        {
            var otherProfile = otherPrefab.GetComponent<SnowManNormalProfile>();

            if (otherProfile.snowManType != snowManProfile.snowManType)
            {
                GameManager.instance.usingSnowMan[otherProfile.snowManType] = false;
            }
        }
    }

    // 눈사람 사용중 변경
    private void ChangeSnowMan()
    {
        foreach (var otherPrefab in snowManProfileUI)
        {
            var otherProfile = otherPrefab.GetComponent<SnowManNormalProfile>();
            SnowManType otherSnowManType = otherProfile.snowManType;
            bool isUsing = GameManager.instance.usingSnowMan.ContainsKey(otherSnowManType) && GameManager.instance.usingSnowMan[otherSnowManType];

            otherProfile.useText.text = isUsing ? "사용중" : "선택";
        }
    }

    // 아이스크림 부족 알림
    private void IcecreamLackAlarm()
    {
        if(!isFade)
        {
            StartCoroutine(FadeTextToZeroAlpha(icecreamLackText, 0.5f)); // 2초 동안 페이드 아웃
        }
    }

    // 눈사람 잠김 알림
    private void SnowManLockAlarm()
    {
        if (!isFade)
        {
            StartCoroutine(FadeTextToZeroAlpha(snowManLockText, 0.5f)); // 2초 동안 페이드 아웃
        }
    }

    // 텍스트의 투명도를 서서히 증가시키고 비활성화하는 코루틴
    private IEnumerator FadeTextToZeroAlpha(TMP_Text text, float duration)
    {
        isFade = true;
        text.gameObject.SetActive(true); // 텍스트 활성화
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0); // 투명도를 0으로 초기화

        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / duration));
            yield return null;
        }

        while(text.color.a > 0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / (duration + 0.25f)));
            yield return null;
        }

        text.gameObject.SetActive(false); // 텍스트 비활성화
        isFade = false;
    }
}
