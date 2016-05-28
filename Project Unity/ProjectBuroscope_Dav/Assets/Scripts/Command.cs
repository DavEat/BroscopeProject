using UnityEngine;
using System.Collections;

public class Command : MonoBehaviour {

	void Update ()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
