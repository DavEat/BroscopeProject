using UnityEngine;
using System.Collections;

public class ExitBord : MonoBehaviour {

	void Update ()
    {
	    if (!transform.GetComponent<CameraMove>().enabled)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Camera.main.GetComponent<CameraMove>().enabled = true;
                Camera.main.GetComponent<MoveCameraToRail>().enabled = false;
            }
        }
	}
}
