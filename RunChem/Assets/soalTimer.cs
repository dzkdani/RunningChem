using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soalTimer : MonoBehaviour
{
    public Image timerImg;
    float maxTime = 10.5f;
    float minTime = 0f;
    [SerializeField] float currentTime;

    void OnEnable() 
    {
        currentTime = maxTime;
        timerImg.fillAmount = maxTime / maxTime;
    }

    void LateUpdate()
    {
        countDownTimer();
    }

    void countDownTimer()
    {
        if (currentTime >= minTime)
        {
            currentTime -= Time.deltaTime;
            timerImg.fillAmount = currentTime / maxTime;
        }

        if (currentTime == minTime) { currentTime = maxTime; }
    }
}
