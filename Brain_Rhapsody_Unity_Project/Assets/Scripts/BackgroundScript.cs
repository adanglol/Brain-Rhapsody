using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            rend.color = new Color(0.0f, .06f, .1f, 0.64f);
        }
        if (Input.GetKeyDown("2"))
        {
            rend.color = new Color(0.0f, 0.1f, .33f, 0.64f);
        }
        if (Input.GetKeyDown("3"))
        {
            rend.color = new Color(0.48f, 0.48f, 0.48f, 0.64f);
        }
        if (Input.GetKeyDown("4"))
        {
            rend.color = new Color(0.75f, 0.43f, 0.0f, 0.64f);
        }
    }
}
