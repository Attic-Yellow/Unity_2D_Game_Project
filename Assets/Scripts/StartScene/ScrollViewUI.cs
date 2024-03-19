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
        // AD ������ ����
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

    // ���̽�ũ�� ����
    private void UpdateIcecreamDisplay()
    {
        if (GameManager.instance != null)
        {
            icecreamText.text = GameManager.instance.icecream.ToString();
        }
    }

    // ���� ����� ������ ����
    private void AddSnowManAD()
    {
        // ������ �ν��Ͻ� ����, contentTrans �ڽ����� ����
        var go = Instantiate(this.snowManADProfilePrefab, this.contentTrans);
        SnowManProfileAD snowManProfileAD = go.GetComponent<SnowManProfileAD>();

        snowManProfileAD.buttonAD.onClick.AddListener(() =>
        {
            Debug.Log("���� ����");
        });

        snowManProfileAD.unlockButton.onClick.AddListener(() =>
        {
            SnowManLockAlarm();
        });
    }

    // ����� ������ ����
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

    // ����� ��� ����
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

    // ����� ����� ����
    private void ChangeSnowMan()
    {
        foreach (var otherPrefab in snowManProfileUI)
        {
            var otherProfile = otherPrefab.GetComponent<SnowManNormalProfile>();
            SnowManType otherSnowManType = otherProfile.snowManType;
            bool isUsing = GameManager.instance.usingSnowMan.ContainsKey(otherSnowManType) && GameManager.instance.usingSnowMan[otherSnowManType];

            otherProfile.useText.text = isUsing ? "�����" : "����";
        }
    }

    // ���̽�ũ�� ���� �˸�
    private void IcecreamLackAlarm()
    {
        if(!isFade)
        {
            StartCoroutine(FadeTextToZeroAlpha(icecreamLackText, 0.5f)); // 2�� ���� ���̵� �ƿ�
        }
    }

    // ����� ��� �˸�
    private void SnowManLockAlarm()
    {
        if (!isFade)
        {
            StartCoroutine(FadeTextToZeroAlpha(snowManLockText, 0.5f)); // 2�� ���� ���̵� �ƿ�
        }
    }

    // �ؽ�Ʈ�� ������ ������ ������Ű�� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
    private IEnumerator FadeTextToZeroAlpha(TMP_Text text, float duration)
    {
        isFade = true;
        text.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0); // ������ 0���� �ʱ�ȭ

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

        text.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ
        isFade = false;
    }
}
