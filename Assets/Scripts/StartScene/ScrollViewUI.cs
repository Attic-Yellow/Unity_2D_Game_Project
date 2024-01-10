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

            // GameManager���� ���� ���� Ȯ��
            bool isOwned = GameManager.instance.ownedSnowMans.ContainsKey(snowManProfile.snowManName.text)
                           && GameManager.instance.ownedSnowMans[snowManProfile.snowManName.text];

            // ���� ���¿� ���� UI ������Ʈ
            snowManProfile.priceButton.gameObject.SetActive(!isOwned);
            snowManProfile.useButton.gameObject.SetActive(isOwned);

            // ���� ��� ���� ����� Ȯ�� �� �ؽ�Ʈ ������Ʈ
            if (GameManager.instance.currentlyUsingSnowMan == snowManProfile.snowManName.text)
            {
                snowManProfile.useText.text = "�����";
            }
            else
            {
                snowManProfile.useText.text = isOwned ? "����" : "";
            }

            snowManProfile.useButton.onClick.AddListener(() =>
            {
                // ���� ��� ���� ������� ������ ��������� ����
                GameManager.instance.currentlyUsingSnowMan = snowManProfile.snowManName.text;

                // �ʿ��� ���, UI ������Ʈ�� �߰� ���� ����
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

                    // UI ������Ʈ
                }
                else
                {
                    Debug.Log("���̽�ũ���� �����մϴ�.");
                    // �ʿ��� ���, ������ ���̽�ũ���� �˸��� UI ������Ʈ
                }
            });

        }
    }

}
