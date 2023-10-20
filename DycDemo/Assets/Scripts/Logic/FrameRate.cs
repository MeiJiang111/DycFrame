using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    float updateInterval = 0.5f;
    private float accum = 0.0f;
    private float frames = 0;
    private float timeleft;
    private string faram;


    void Start()
    {
        timeleft = updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0.0)
        {
            int fps = (int)(accum / frames);
            faram = $"FPS: {fps}";
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }

    void OnGUI()
    {
        GUIStyle bb = new GUIStyle();
        bb.normal.textColor = Color.green;
        bb.fontSize = 35;
        GUI.Label(new Rect(0, 0, 200, 200), faram, bb);
    }
}
