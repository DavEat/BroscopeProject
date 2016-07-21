using UnityEngine;
using System.Collections;

public class Professor : MonoBehaviour
{
    public int iDListNodes;
    public Vector3 finalRotation;
    public float distanceToTalking = 3;
    public Transform player;

    void Update()
    {
        if (player.GetComponent<VRMove>() != null)
        {
            if (player.GetComponent<VRMove>().professor)
                if ((Vector3.Distance(player.position, transform.position)) < distanceToTalking)
                {
                    if (Camera.main.transform.parent.GetComponent<CameraMove>().enabled)
                    {
                        Camera.main.transform.parent.GetComponent<CameraMove>().enabled = false;
                        Camera.main.transform.parent.GetComponent<MoveCameraToRail>().enabled = true;
                        Camera.main.transform.parent.GetComponent<MoveCameraToRail>().currentNode = 0;
                        Camera.main.transform.parent.GetComponent<MoveCameraToRail>().idListNodes = iDListNodes;
                        Camera.main.transform.parent.GetComponent<MoveCameraToRail>().finalRotation = finalRotation;
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
                    if (Camera.main.transform.parent.GetComponent<CameraMove>().enabled)
                    {
                        Camera.main.transform.parent.GetComponent<CameraMove>().enabled = false;
                        Camera.main.transform.parent.GetComponent<MoveCameraToRail>().enabled = true;
                        Camera.main.transform.parent.GetComponent<MoveCameraToRail>().currentNode = 0;
                        Camera.main.transform.parent.GetComponent<MoveCameraToRail>().idListNodes = iDListNodes;
                        Camera.main.transform.parent.GetComponent<MoveCameraToRail>().finalRotation = finalRotation;
                        player.GetComponent<MovePerso>().professor = false;
                        player.GetComponent<MovePerso>().canMove = false;
                    }
                }
        }
        else Debug.LogError("Perso don't have a element to move, if you are in vr put the script : 'VRMove' else put 'MovePerso'");
    }
}
