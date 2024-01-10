using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBlink : MonoBehaviour
{
    public TMP_Text fadingText;
    public float fadeDuration = 1f; //페이드 인/아웃에 걸리는 시간

    void Start()
    {
        StartCoroutine(FadeTextToFullAlpha());
    }

    public IEnumerator FadeTextToFullAlpha()
    {
        fadingText.color = new Color(fadingText.color.r, fadingText.color.g, fadingText.color.b, 0);

        while (fadingText.color.a < 1.0f)
        {
            fadingText.color = new Color(fadingText.color.r, fadingText.color.g, fadingText.color.b, fadingText.color.a + (Time.deltaTime / fadeDuration));
            yield return null;
        }

        StartCoroutine(FadeTextToZeroAlpha());
    }

    public IEnumerator FadeTextToZeroAlpha()
    {
        while (fadingText.color.a > 0.0f)
        {
            fadingText.color = new Color(fadingText.color.r, fadingText.color.g, fadingText.color.b, fadingText.color.a - (Time.deltaTime / fadeDuration));
            yield return null;
        }
        StartCoroutine(FadeTextToFullAlpha());
    }
}