using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarySnowMan : SnowManAbility
{
    private void FixedUpdate()
    {
        if(GameManager.instance.selectedSnowManType == GameManager.SnowManType.섬뜩한눈사람)
        {
            if (stageManager.score % 1000 == 0 && stageManager.score != 0)
            {
                GameManager.instance.AddIcecream(1);
                GameManager.instance.AddScore(100);
                print("1000의 배수 달성 점수 100점 추가\n");
            }
        }
        
    }
}
