using UnityEngine;
using System.Collections;

public class Command : MonoBehaviour {

    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    void Update ()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
