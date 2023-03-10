using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Screen.SetResolution(2560, 1440, false);
            Debug.Log("Screen width: " + Screen.width);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Screen.SetResolution(1920, 1200, false);
            Debug.Log("Screen width: " + Screen.width);
        }
    }
}
