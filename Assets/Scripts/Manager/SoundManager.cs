using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSliderBGM;
    public List<AudioSource> audioBGM;

    private void Start()
    {
        volumeSliderBGM.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        foreach(var audioSource in audioBGM)
        {
            if(audioSource != null)
            {
                audioSource.volume = volume;
            }
        }
    }

}
