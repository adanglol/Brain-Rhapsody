using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransitions : MonoBehaviour
{

    [SerializeField] private AudioSource[] transitions;

    // Start is called before the first frame update
    void Start()
    {
        /*
        transitions[0].enabled = true;
        transitions[1].enabled = true;
        transitions[2].enabled = true;
        transitions[3].enabled = true;
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0 )
        {
            if (Input.GetKeyDown("1"))
            {
                transitions[0].Play();
            }
            if (Input.GetKeyDown("2"))
            {
                transitions[1].Play();   
            }
            if (Input.GetKeyDown("3"))
            {
                transitions[2].Play();
            }
            if (Input.GetKeyDown("4"))
            {
                transitions[3].Play();
            }

        }
    }
}
