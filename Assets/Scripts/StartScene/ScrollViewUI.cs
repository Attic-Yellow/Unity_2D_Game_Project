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
        // AD 프로필 생성
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
        // 프리팹 인스턴스 생성, contentTrans 자식으로 부착
        var go = Instantiate(this.snowManADProfilePrefab, this.contentTrans);
        SnowManProfileAD snowManProfileAD = go.GetComponent<SnowManProfileAD>();

        snowManProfileAD.buttonAD.onClick.AddListener(() =>
        {
            Debug.Log("광고 보기");
        });

        snowManProfileAD.unlockButton.onClick.AddListener(() =>
        {
            Debug.Log("광고 눈사람 잠금 해제");
        });
    }

    public void AddSnowMan()
    {
        foreach (var prefab in snowManProfilePrefab)
        {
            var go = Instantiate(prefab, contentTrans);
            SnowManNormalProfile snowManProfile = go.GetComponent<SnowManNormalProfile>();
            snowManProfileUI.Add(go);
            // ToDo : 보유, 미보유 상태 확인에 따른 버튼 활성화
            // 보유 시 : Use 버튼 Active 값 True, Price 버튼 Active 값 False

            string snowManName = snowManProfile.snowManName.text;

            bool isOwned = GameManager.instance.ownedSnowMans.ContainsKey(snowManName);
            snowManProfile.useButton.gameObject.SetActive(isOwned);
            snowManProfile.priceButton.gameObject.SetActive(!isOwned);

            ChangeSnowMan();

            snowManProfile.useButton.onClick.AddListener(() =>
            {
                // ToDo : 현재 사용중인 눈사람 변경
                // 현재 사용중인 눈사람 Use 버튼 Text 상용중으로 변경 다른 눈사람 Use 버튼 Text 선택으로 변경
                // 게임 매니저에 currentlyUsingSnowMan 업데이트

                ChangeUseSnowMan(snowManProfile);
                ChangeSnowMan();
            });


            snowManProfile.priceButton.onClick.AddListener(() =>
            {
                // 눈사람 가격 확인
                int snowManPrice = int.Parse(snowManProfile.priceText.text);

                // 사용자가 충분한 아이스크림을 가지고 있는지 확인
                if (GameManager.instance.icecream >= snowManPrice)
                {
                    // 아이스크림 차감 및 눈사람 보유 상태 업데이트
                    GameManager.instance.RemoveIcecream(snowManPrice);
                    GameManager.instance.ownedSnowMans[snowManProfile.snowManName.text] = true;

                    // ToDo : Use 버튼 Active 값 True, Price 버튼 Active 값 False
                    // Use 버튼 Text 상용중으로 변경 다른 눈사람 Use 버튼 Text 선택으로 변경
                    // 게임 매니저에 ownedSnowMans 업데이트
                    // 게임 매니저에 currentlyUsingSnowMan 업데이트

                    snowManProfile.useButton.gameObject.SetActive(true);
                    snowManProfile.priceButton.gameObject.SetActive(false);
                    ChangeUseSnowMan(snowManProfile);
                    ChangeSnowMan();
                }
                else
                {
                    Debug.Log("아이스크림이 부족합니다.");
                    // 필요한 경우, 부족한 아이스크림을 알리는 UI 업데이트
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

            Debug.Log($"{otherSnowManName} is {(isCurrent ? "사용중" : "선택")}");

            //otherProfile.useButton.GetComponentInChildren<TextMeshProUGUI>().text = isCurrent ? "사용중" : "선택";
            otherProfile.useText.text = isCurrent ? "사용중" : "선택";
        }
        Canvas.ForceUpdateCanvases();
    }
}
