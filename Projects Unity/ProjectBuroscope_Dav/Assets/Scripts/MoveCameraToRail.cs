using UnityEngine;
using System.Collections.Generic;

public class MoveCameraToRail : MonoBehaviour {
        
    public float speedMove, speedRotation, speedRotAcceuil;

    [HideInInspector]
    public int idListNodes, currentNode;
    //[HideInInspector]
    public Vector3 finalRotation;
    //[SerializeField]
    public List<Transform> ListListNodes;

	void Update ()
    {
        if (Vector3.Distance(transform.position, ListListNodes[idListNodes].GetChild(currentNode).position) < Time.deltaTime * speedMove)
        {
            if (currentNode < ListListNodes[idListNodes].childCount - 1)
                currentNode++;
            else if (ListListNodes[idListNodes].parent.GetComponentInChildren<LogoBoardClick>() != null)
            {
                if (!ListListNodes[idListNodes].parent.GetComponentInChildren<LogoBoardClick>().inFrontOfBoard)
                {
                    ListListNodes[idListNodes].parent.GetComponentInChildren<LogoBoardClick>().inFrontOfBoard = true;
                    ListListNodes[idListNodes].parent.GetComponentInChildren<LogoBoardClick>().listPNJ.gameObject.SetActive(false);
                }
            }
            else if (ListListNodes[idListNodes].parent.GetComponentInChildren<FliersClick>() != null)
            {
                if (!ListListNodes[idListNodes].parent.GetComponentInChildren<FliersClick>().inFrontOfBoard)
                {
                    ListListNodes[idListNodes].parent.GetComponentInChildren<FliersClick>().inFrontOfBoard = true;
                    transform.GetChild(0).eulerAngles = finalRotation;
                    ListListNodes[idListNodes].parent.GetComponentInChildren<FliersClick>().listPNJ.gameObject.SetActive(false);
                }
            }
            else if (ListListNodes[idListNodes].parent.GetComponentInChildren<InfoOnClick>() != null)
            {
                if (!ListListNodes[idListNodes].parent.GetComponentInChildren<InfoOnClick>().inFrontOfBoard)
                {
                    ListListNodes[idListNodes].parent.GetComponentInChildren<InfoOnClick>().inFrontOfBoard = true;
                    foreach (SlideBarManagement slide in ListListNodes[idListNodes].parent.GetComponentInChildren<InfoOnClick>().listSliderBarMan)
                        slide.SetSlideBar();
                    transform.GetChild(0).eulerAngles = finalRotation;
                    ListListNodes[idListNodes].parent.GetComponentInChildren<InfoOnClick>().listPNJ.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Vector3 direction = transform.position - ListListNodes[idListNodes].GetChild(currentNode).position;
            direction = new Vector3 (-direction.x, -direction.y, -direction.z).normalized;
            transform.Translate(direction * Time.deltaTime * speedMove);
            if (idListNodes == 0)
                transform.GetChild(0).eulerAngles = Vector3.Lerp(transform.GetChild(0).eulerAngles, finalRotation, Time.deltaTime * speedRotAcceuil);
            else transform.GetChild(0).eulerAngles = Vector3.Lerp(transform.GetChild(0).eulerAngles, finalRotation, Time.deltaTime * speedRotation);
        }
	}
}
