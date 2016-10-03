using UnityEngine;
using System.Collections.Generic;

public class FliersMove : MonoBehaviour {

    public int currentPos = 0;

    public List<Transform> listArrow, listPos;
    public Transform lastNode;

    public void onclickArrow (Transform arrow)
    {
        transform.parent.GetComponent<FliersClick>().inFrontOfBoard = false;
        if (arrow.name[5] == 'L')
        {
            if (currentPos == 0)
            {
                currentPos = 1;                
            }
            else if (currentPos == 1)
            {
                currentPos = 0;
            }
        }
        else if (arrow.name[5] == 'R')
        {
            if (currentPos == 1)
            {
                currentPos = 2;
            }
            else if (currentPos == 2)
            {
                currentPos = 1;
            }
        }
        Camera.main.transform.parent.GetComponent<MoveCameraToRail>().finalRotation = listPos[currentPos].eulerAngles;
        lastNode.position = listPos[currentPos].position;
    }
}
