using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpslimter : MonoBehaviour
{
    public enum FrameRateLimit
    {
        NoLimit = 0,
        Limit30 = 30,
        Limit60 = 60
    }

    public FrameRateLimit frameRateLimit = FrameRateLimit.NoLimit;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        Application.targetFrameRate = (int)frameRateLimit;
    }
}
