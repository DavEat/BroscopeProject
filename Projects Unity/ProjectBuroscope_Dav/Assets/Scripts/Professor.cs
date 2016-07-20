using UnityEngine;
using System.Collections;

public class Professor : MonoBehaviour
{

    public float distanceToTalking = 3;
    public Transform player;

    void Update()
    {
        if (player.GetComponent<VRMove>() != null)
        {
            if (player.GetComponent<VRMove>().professor)
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
        else if (player.GetComponent<MovePerso>() != null)
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
        else Debug.LogError("Perso don't have a element to move, if you are in vr put the script : 'VRMove' else put 'MovePerso'");
    }
}
