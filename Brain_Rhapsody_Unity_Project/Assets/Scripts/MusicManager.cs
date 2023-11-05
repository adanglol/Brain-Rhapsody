using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] musicTracks;
    [SerializeField] private int currentTrack;

    // Start is called before the first frame update
    void Start()
    {
        musicTracks[0].enabled = true;
        musicTracks[1].enabled = true;
        musicTracks[2].enabled = true;
        musicTracks[3].enabled = true;

        Debug.Log("in musicv manager");
        musicTracks[0].Play();

        musicTracks[1].Play();
        musicTracks[1].mute = true;

        musicTracks[2].Play();
        musicTracks[2].mute = true;

        musicTracks[3].Play();
        musicTracks[3].mute = true;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("1"))
        {
            currentTrack = 0;
            musicTracks[0].volume -= 1;
            musicTracks[0].mute = false;
            musicTracks[1].mute = true;
            musicTracks[2].mute = true;
            musicTracks[3].mute = true;
        }
        if (Input.GetKeyDown("2"))
        {
            currentTrack = 1;
            musicTracks[1].volume -= 1;
            musicTracks[0].mute = true;
            musicTracks[1].mute = false;
            musicTracks[2].mute = true;
            musicTracks[3].mute = true;
        }
        if (Input.GetKeyDown("3"))
        {
            currentTrack = 2;
            musicTracks[2].volume -= 1;
            musicTracks[0].mute = true;
            musicTracks[1].mute = true;
            musicTracks[2].mute = false;
            musicTracks[3].mute = true;
        }
        if (Input.GetKeyDown("4"))
        {
            currentTrack = 3;
            musicTracks[3].volume -= 1;
            musicTracks[0].mute = true;
            musicTracks[1].mute = true;
            musicTracks[2].mute = true;
            musicTracks[3].mute = false;
        }

        pauseCheck();
    }
    void FixedUpdate()
    {
        fadeIn();
    }

    void pauseCheck()
    {
        if (Time.timeScale == 0)
        {
            musicTracks[0].Pause();
            musicTracks[1].Pause();
            musicTracks[2].Pause();
            musicTracks[3].Pause();
        }
        else
        {
            musicTracks[0].UnPause();
            musicTracks[1].UnPause();
            musicTracks[2].UnPause();
            musicTracks[3].UnPause();
        }
    }

    void fadeIn()
    {
        if (musicTracks[currentTrack].volume < 1)
        {
            musicTracks[currentTrack].volume += 0.07f;
        }
    }
}
