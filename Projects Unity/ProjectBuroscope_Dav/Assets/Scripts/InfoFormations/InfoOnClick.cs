using UnityEngine;
using System.Collections.Generic;

public class InfoOnClick : MonoBehaviour
{

    public bool inFrontOfBoard;
    public int currentViewId;
    public List<Transform> listButton;

    public Transform listPNJ;

    public XmlReader xml;

    private Transform currentElemActive;

    public GameObject player;

    void Update()
    {
        if (inFrontOfBoard)
        {
            #if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                checkClick(ray);
            #elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
                if (Input.touchCount > 0)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    checkClick(ray);
                }
            #endif
        }
    }

    private void checkClick(Ray ray)
    {
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);
        if (Physics.Raycast(ray, out hit, 100))
        {
            foreach (Transform t in listButton)
            {
                if (t == hit.transform)
                    CheckClicked(t);
            }
        }
        CheckClicked(null);
    }

    private void CheckClicked(Transform elem)  //-----Check if elem is clicked of touched-----
    {
        #if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
            if (Input.GetMouseButtonUp(0) && elem != null)
            {
                OnLClickActions(elem);
            }
            else if (Input.GetMouseButtonUp(1) && elem == null)
            {
                OnRClickActions();
            }
        #elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
            if (Input.touchCount > 0 && elem != null) {
                if (Input.touches[0].phase == TouchPhase.Ended)
                    OnLClickActions(elem);
            }
            else if (Input.touchCount >= 2 && elem == null) {
                if (Input.touches[0].phase == TouchPhase.Ended && Input.touches[1].phase == TouchPhase.Ended)
                    OnRClickActions();
            }
        #endif
    }

    private void OnLClickActions(Transform elem)  //-----Do action on left click-----
    {
        if (elem.GetComponent<ButtonStat>().id > 1000)
        {
            elem.parent.parent.GetComponent<AnimPlace2>().SetElementValue(elem.GetComponent<ButtonStat>().id - 1000);
            elem.parent.parent.GetComponent<AnimPlace2>().animIndex = elem.GetComponent<ButtonStat>().id - 1000;
        }
        else if (elem.GetComponent<ButtonStat>().id > 10)
        {
            elem.parent.parent.GetComponent<AnimPlace1>().SetElementValue(elem.GetComponent<ButtonStat>().id - 10);
            elem.parent.parent.GetComponent<AnimPlace1>().animIndex = elem.GetComponent<ButtonStat>().id - 10;
        }
    }

    private void OnRClickActions()  //-----Do action on right click-----
    {
        inFrontOfBoard = false;
        Camera.main.transform.parent.GetComponent<CameraMove>().enabled = true;
        Camera.main.transform.parent.GetComponent<MoveCameraToRail>().enabled = false;
        player.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().Resume();
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovePerso>().canMove = true;
        listPNJ.gameObject.SetActive(true);
    }
}
