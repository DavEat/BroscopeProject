using UnityEngine;
using System.Collections;

public class ExitBord : MonoBehaviour {

	void Update ()
    {
	    if (!transform.GetComponent<CameraMove>().enabled)
        {
            if (Input.GetMouseButtonDown(1) || Input.touchCount >= 2)
            {
                Camera.main.GetComponent<CameraMove>().enabled = true;
                Camera.main.GetComponent<MoveCameraToRail>().enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<MovePerso>().canMove = true;
            }
        }
	}
}
