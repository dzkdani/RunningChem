using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soalTimer : MonoBehaviour
{
    public Image timerImg;
    [SerializeField] float maxTime;
    const float minTime = 0f;
    float currentTime;

    void OnEnable() 
    {
        //maxTime  = soalManager.Instance.getPopUpDuration();
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
