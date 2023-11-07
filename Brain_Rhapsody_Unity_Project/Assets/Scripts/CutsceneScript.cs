using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CutsceneScript : MonoBehaviour
{
    [SerializeField] private AudioSource crowd;
    [SerializeField] private AudioSource startSound;
    [SerializeField] private float pitchChangeSpeed;

    private bool pitchingUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (crowd.pitch <= 0.1)
        {
            pitchingUp = true;
        }
        if (crowd.pitch >= 1)
        {
            pitchingUp = false;
        }

        if (crowd.pitch <= 1 && pitchingUp == true)
        {
            crowd.pitch += pitchChangeSpeed * Time.deltaTime;
        }
        else if (crowd.pitch >= 0.1 && pitchingUp == false)
        {
            crowd.pitch -= pitchChangeSpeed * Time.deltaTime;
        }

        if(Input.GetMouseButtonDown(0)){
            StartCoroutine(SwitchScene());
        }
    }
    private IEnumerator SwitchScene(){
        startSound.Play();
        crowd.Stop();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainGame");
    }
    private void PitchDown()
    {

    }
    private void PitchUp()
    {

    }
}
