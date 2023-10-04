using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public Text FpsTxt;

    public float updateInterval = 0.5F;
    
    private float accum = 0; 	// FPS accumulated over the interval
    private int frames = 0; 	// Frames drawn over the interval
    private float timeleft; 	// Left time for current interval
    float fps;

    void Start()
    {
        timeleft = updateInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        // display two fractional digits (f2 format)

        if (timeleft < 0)
        {
            fps = accum / frames;
            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
        string format = System.String.Format("{0:F2} FPS", fps);
        FpsTxt.text = format;
    }
}
