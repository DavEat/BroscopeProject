using UnityEngine;
using System.Collections;

public class Professor : MonoBehaviour {

    public float distanceToTalking = 3;
    public Transform player;

    void Update()
    {
        if (player.GetComponent<MovePerso>().professor)
            if ((Vector3.Distance(player.position, transform.position)) < distanceToTalking)
            {
                if (Camera.main.GetComponent<CameraMove>().enabled)
                {
                    Camera.main.GetComponent<CameraMove>().enabled = false;
                    Camera.main.GetComponent<MoveCameraToRail>().enabled = true;
                    Camera.main.GetComponent<MoveCameraToRail>().currentNode = 0;
                    player.GetComponent<MovePerso>().professor = false;
                    player.GetComponent<MovePerso>().canMove = false;
                }
            }
    }
}
