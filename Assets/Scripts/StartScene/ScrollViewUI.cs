using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
            // ToDo : ����, �̺��� ���� Ȯ�ο� ���� ��ư Ȱ��ȭ
            // ���� �� : Use ��ư Active �� True, Price ��ư Active �� False

            string snowManName = snowManProfile.snowManName.text;

            bool isOwned = GameManager.instance.ownedSnowMans.ContainsKey(snowManName);
            snowManProfile.useButton.gameObject.SetActive(isOwned);
            snowManProfile.priceButton.gameObject.SetActive(!isOwned);

            ChangeSnowMan();

            snowManProfile.useButton.onClick.AddListener(() =>
            {
                // ToDo : ���� ������� ����� ����
                // ���� ������� ����� Use ��ư Text ��������� ���� �ٸ� ����� Use ��ư Text �������� ����
                // ���� �Ŵ����� currentlyUsingSnowMan ������Ʈ

                ChangeUseSnowMan(snowManProfile);
                ChangeSnowMan();
            });


            snowManProfile.priceButton.onClick.AddListener(() =>
            {
                // ����� ���� Ȯ��
                int snowManPrice = int.Parse(snowManProfile.priceText.text);

                // ����ڰ� ����� ���̽�ũ���� ������ �ִ��� Ȯ��
                if (GameManager.instance.icecream >= snowManPrice)
                {
                    // ���̽�ũ�� ���� �� ����� ���� ���� ������Ʈ
                    GameManager.instance.RemoveIcecream(snowManPrice);
                    GameManager.instance.ownedSnowMans[snowManProfile.snowManName.text] = true;

                    // ToDo : Use ��ư Active �� True, Price ��ư Active �� False
                    // Use ��ư Text ��������� ���� �ٸ� ����� Use ��ư Text �������� ����
                    // ���� �Ŵ����� ownedSnowMans ������Ʈ
                    // ���� �Ŵ����� currentlyUsingSnowMan ������Ʈ

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
        string currentSnowManName = snowManProfile.snowManName.text;
        GameManager.instance.usingSnowMan[currentSnowManName] = true;

        Debug.Log($"{currentSnowManName} to true");

        foreach (var otherPrefab in snowManProfileUI)
        {
            var otherProfile = otherPrefab.GetComponent<SnowManNormalProfile>();
            if (otherProfile.snowManName.text != currentSnowManName)
            {
                GameManager.instance.usingSnowMan[otherProfile.snowManName.text] = false;
                Debug.Log($"{otherProfile.snowManName.text} to false");
            }
        }
    }

    public void ChangeSnowMan()
    {
        foreach (var otherPrefab in snowManProfileUI)
        {
            var otherProfile = otherPrefab.GetComponent<SnowManNormalProfile>();
            string otherSnowManName = otherProfile.snowManName.text;
            bool isCurrent = GameManager.instance.usingSnowMan.ContainsKey(otherSnowManName) && GameManager.instance.usingSnowMan[otherSnowManName];

            Debug.Log($"{otherSnowManName} is {(isCurrent ? "�����" : "����")}");

            //otherProfile.useButton.GetComponentInChildren<TextMeshProUGUI>().text = isCurrent ? "�����" : "����";
            otherProfile.useText.text = isCurrent ? "�����" : "����";
        }
        Canvas.ForceUpdateCanvases();
    }
}
