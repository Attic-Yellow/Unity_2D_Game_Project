using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;

public class ScrollViewUI : MonoBehaviour
{
    public Transform contentTrans;
    public TextMeshProUGUI icecreamText;
    public GameObject snowManADProfilePrefab; 
    public List<GameObject> snowManProfilePrefab;
    private List<GameObject> snowManProfileUI = new();

    public void Init()
    {
        // AD ������ ����
        AddSnowManAD();

        AddSnowMan();

    }

    public void Update()
    {
        UpdateIcecreamDisplay();
    }

    public void UpdateIcecreamDisplay()
    {
        if (GameManager.instance != null)
        {
            icecreamText.text = GameManager.instance.icecream.ToString();
        }
    }

    public void AddSnowManAD()
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
            Debug.Log("���� ����� ��� ����");
        });
    }

    public void AddSnowMan()
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
                GameManager.instance.usingSnowMan[snowManProfile.snowManType] = true;
                GameManager.instance.SelectSnowMan(snowManType);
                ChangeUseSnowMan(snowManProfile);
                ChangeSnowMan();
            });

            snowManProfile.priceButton.onClick.AddListener(() =>
            {
                int snowManPrice = int.Parse(snowManProfile.priceText.text);
                if (GameManager.instance.icecream >= snowManPrice)
                {
                    GameManager.instance.RemoveIcecream(snowManPrice);
                    GameManager.instance.ownedSnowMans[snowManType] = true;
                    GameManager.instance.usingSnowMan[snowManProfile.snowManType] = true;

                    snowManProfile.useButton.gameObject.SetActive(true);
                    snowManProfile.priceButton.gameObject.SetActive(false);

                    ChangeUseSnowMan(snowManProfile);
                    ChangeSnowMan();
                }
                else
                {
                    Debug.Log("���̽�ũ���� �����մϴ�.");
                    // �ʿ��� ���, ������ ���̽�ũ���� �˸��� UI ������Ʈ
                }
            });
        }
    }

    public void ChangeUseSnowMan(SnowManNormalProfile snowManProfile)
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

    public void ChangeSnowMan()
    {
        foreach (var otherPrefab in snowManProfileUI)
        {
            var otherProfile = otherPrefab.GetComponent<SnowManNormalProfile>();
            SnowManType otherSnowManType = otherProfile.snowManType;
            bool isUsing = GameManager.instance.usingSnowMan.ContainsKey(otherSnowManType) && GameManager.instance.usingSnowMan[otherSnowManType];

            otherProfile.useText.text = isUsing ? "�����" : "����";
        }
    }
}
