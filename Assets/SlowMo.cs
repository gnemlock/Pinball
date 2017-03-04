using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo : MonoBehaviour {
    public AudioSource music;
    public AudioEchoFilter echoFilter;
    public AudioHighPassFilter highPass;
    public AudioDistortionFilter distortion;
    public AudioReverbFilter reverb;
    public AudioLowPassFilter lowPass;
    public AudioChorusFilter chorusFilter;
    public FilterMode filterMode;
    [Range(0f, 2.0f)]public float slowMotionTime;
    [Range(0f, 2.0f)]public float pitch;

    public void Start()
    {
        Time.timeScale = 1.0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = slowMotionTime;
        GameManager.instance.timeText.text = slowMotionTime.ToString();

        if(music)
        {
            music.pitch = pitch;
        }
        if(echoFilter)
        {
            echoFilter.enabled = true;
        }
        if(chorusFilter)
        {
            chorusFilter.enabled = true;
        }
        if(reverb)
        {
            reverb.enabled = true;
        }
        if(distortion)
        {
            distortion.enabled = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Time.timeScale = 1.0f;

        GameManager.instance.timeText.text = "normal";
        if(music)
        {
            music.pitch = 1.0f;
        }
        if(echoFilter)
        {
            echoFilter.enabled = false;
        }
        if(chorusFilter)
        {
            chorusFilter.enabled = false;
        }
        if(reverb)
        {
            reverb.enabled = false;
        }
        if(distortion)
        {
            distortion.enabled = false;
        }
    }
}

//TODO:Force Feedback
//TODO:Local music library
//TODO:finishline
//TODO:high scores
