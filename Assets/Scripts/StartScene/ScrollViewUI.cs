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

            // GameManager에서 보유 상태 확인
            bool isOwned = GameManager.instance.ownedSnowMans.ContainsKey(snowManProfile.snowManName.text)
                           && GameManager.instance.ownedSnowMans[snowManProfile.snowManName.text];

            // 보유 상태에 따른 UI 업데이트
            snowManProfile.priceButton.gameObject.SetActive(!isOwned);
            snowManProfile.useButton.gameObject.SetActive(isOwned);

            // 현재 사용 중인 눈사람 확인 및 텍스트 업데이트
            if (GameManager.instance.currentlyUsingSnowMan == snowManProfile.snowManName.text)
            {
                snowManProfile.useText.text = "사용중";
            }
            else
            {
                snowManProfile.useText.text = isOwned ? "선택" : "";
            }

            snowManProfile.useButton.onClick.AddListener(() =>
            {
                // 현재 사용 중인 눈사람을 선택한 눈사람으로 변경
                GameManager.instance.currentlyUsingSnowMan = snowManProfile.snowManName.text;

                // 필요한 경우, UI 업데이트나 추가 로직 수행
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

                    // UI 업데이트
                }
                else
                {
                    Debug.Log("아이스크림이 부족합니다.");
                    // 필요한 경우, 부족한 아이스크림을 알리는 UI 업데이트
                }
            });

        }
    }

}
