/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicController : MonoBehaviour
{

    //Player Ability Script Call

    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodEvent;

    [SerializeField] [Range(0f, 1f)]
    private float trackPlaying;

    public int playerForm;

    // Start is called before the first frame update
    void Start()
    {
        playerForm = 0;
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
        
    }

    // Update is called once per frame
    void Update()
    {

        instance.setParameterByName("Track Playing", trackPlaying);

        if(playerForm == 0)
        {
            trackPlaying = 0.05f;
        }
        else if(playerForm == 1)
        {
            trackPlaying = 0.21f;
        }
        else if(playerForm == 2)
        {
            trackPlaying = 0.41f;
        }
         else if(playerForm == 3)
        {
            trackPlaying = 0.61f;
        }
    }
}
*/