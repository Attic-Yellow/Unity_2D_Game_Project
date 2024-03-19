using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAudioPlay : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeInTime = 2.0f;

    private void Start()
    {
        // �ʱ� ������ 0���� ����
        audioSource.volume = 0.1f;
        // ����� ��� ����
        audioSource.Play();
        // ������ ������ ������Ű�� �ڷ�ƾ ����
        StartCoroutine(FadeIn(audioSource, fadeInTime));
    }

    // ������ ������ ������Ű�� �ڷ�ƾ
    private IEnumerator FadeIn(AudioSource audioSource, float fadeInTime)
    {
        // ��� �ð��� �����ϴ� ����
        float timeElapsed = 0;

        while (timeElapsed < fadeInTime)
        {
            // ������ ���������� ����
            audioSource.volume = timeElapsed / fadeInTime * 0.3f;
            // �ð� ������Ʈ
            timeElapsed += Time.deltaTime;
            // ���� �����ӱ��� ��ٸ�
            yield return null;
        }

        // ���� ������ 1�� ����
        audioSource.volume = 0.3f;
    }
}
