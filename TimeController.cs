using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public int seconds;
    private float elapsedTime;

    void Start()
    {
        seconds = 180;
        elapsedTime = 0f;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f)
        {
            seconds--;
            elapsedTime = 0f;
        }

        if (seconds <= 0)
        {
            seconds = 0;
        }

    }
}
