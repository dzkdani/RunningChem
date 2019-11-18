﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraResolution : MonoBehaviour
{
    public SpriteRenderer background;
    void Awake()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = background.bounds.size.x / background.bounds.size.y;

        if(screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = background.bounds.size.y / 2;
        } else {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = background.bounds.size.y / 2 * differenceInSize;
        }
    }
}
