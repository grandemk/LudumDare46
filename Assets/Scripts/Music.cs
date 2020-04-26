using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Music>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake() 
    {
        if(_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if(this != _instance)
                Destroy(this.gameObject);
        }
    }

    private static Music _instance;
    private double nextEventTime;

    public AudioClip[] clips = new AudioClip[2];
    public AudioSource[] audioSources = new AudioSource[2];
    private int flip = 0;
    private int minClip = 0;
    private int curClip = 0;
    private int maxClip = 1;

    void Start()
    {
        nextEventTime = AudioSettings.dspTime;
    }

    void Update()
    {
        double time = AudioSettings.dspTime;
        if(time + 1.0f > nextEventTime)
        {
            audioSources[flip].clip = clips[minClip + curClip];
            audioSources[flip].PlayScheduled(nextEventTime);
            nextEventTime += audioSources[flip].clip.length;
            flip = 1 - flip;
            curClip = (curClip + 1) % maxClip;
        }
    }

    public void StartGame()
    {
        minClip = 1;
    }
}
