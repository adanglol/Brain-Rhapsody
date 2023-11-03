using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] musicTracks;

    // Start is called before the first frame update
    void Start()
    {
        musicTracks[0].enabled = true;
        musicTracks[1].enabled = true;
        musicTracks[2].enabled = true;
        musicTracks[3].enabled = true;

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
            musicTracks[0].mute = false;
            musicTracks[1].mute = true;
            musicTracks[2].mute = true;
            musicTracks[3].mute = true;
        }
        if (Input.GetKeyDown("2"))
        {

            musicTracks[0].mute = true;
            musicTracks[1].mute = false;
            musicTracks[2].mute = true;
            musicTracks[3].mute = true;
        }
        if (Input.GetKeyDown("3"))
        {
            musicTracks[0].mute = true;
            musicTracks[1].mute = true;
            musicTracks[2].mute = false;
            musicTracks[3].mute = true;
        }
        if (Input.GetKeyDown("4"))
        {
            musicTracks[0].mute = true;
            musicTracks[1].mute = true;
            musicTracks[2].mute = true;
            musicTracks[3].mute = false;
        }
    }
}
