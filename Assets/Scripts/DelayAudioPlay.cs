using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAudioPlay : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeInTime = 2.0f;

    private void Start()
    {
        // 초기 볼륨을 0으로 설정
        audioSource.volume = 0.1f;
        // 오디오 재생 시작
        audioSource.Play();
        // 볼륨을 서서히 증가시키는 코루틴 시작
        StartCoroutine(FadeIn(audioSource, fadeInTime));
    }

    // 볼륨을 서서히 증가시키는 코루틴
    private IEnumerator FadeIn(AudioSource audioSource, float fadeInTime)
    {
        // 경과 시간을 추적하는 변수
        float timeElapsed = 0;

        while (timeElapsed < fadeInTime)
        {
            // 볼륨을 점진적으로 증가
            audioSource.volume = timeElapsed / fadeInTime * 0.3f;
            // 시간 업데이트
            timeElapsed += Time.deltaTime;
            // 다음 프레임까지 기다림
            yield return null;
        }

        // 최종 볼륨을 1로 설정
        audioSource.volume = 0.3f;
    }
}
