using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music _instance;
    private int musicLength = 1;
    private int musicStart = 0;

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

    public AudioSource adSource;
    public AudioClip[] adClips;

    IEnumerator playAudioSequentially()
    {
        yield return null;

        while(true)
        {
            for (int i = musicStart; i < musicLength; ++i)
            {
                adSource.clip = adClips[i];
                adSource.Play();
                while (adSource.isPlaying)
                {
                    yield return null;
                }
            }
        }
    }

    void Start()
    {
        StartCoroutine(playAudioSequentially());
    }

    public void StartGame()
    {
        musicStart = 1;
        musicLength = adClips.Length;
    }
}
