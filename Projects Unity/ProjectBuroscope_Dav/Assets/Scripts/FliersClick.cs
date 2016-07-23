using UnityEngine;
using System.Collections.Generic;

public class FliersClick : MonoBehaviour {

    public bool inFrontOfBoard;
    public int currentViewId;
    public List<Transform> listFliers;

    public Transform listPNJ, listArrow;

    private Transform currentElemActive;

    void Awake()  //-----Initialisation-----
    {
        /*foreach (Transform t in listView)
            t.GetComponent<listElemInView>().Instentiate();*/
    }

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
            foreach(Transform t in listFliers)
            {
                if (t.name[0] == 'a')
                {
                    if (t.GetChild(0) == hit.transform)
                        CheckClicked(t);
                }
                else if (t.GetChild(0).GetChild(0) == hit.transform || t.GetChild(1).GetChild(0) == hit.transform)
                    CheckClicked(t);
            }
        }
        CheckClicked(null);
    }

    private void CheckClicked(Transform elem)  //-----Check if elem is clicked of touched-----
    {
        #if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
            if (Input.GetMouseButtonDown(0) && elem != null)
            {
                OnLClickActions(elem);
            }
            else if (Input.GetMouseButtonDown(1) && elem == null)
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
        if (elem.name[0] == 'a')
        {
            elem.parent.GetComponent<FliersMove>().onclickArrow(elem);
        }
        else SetValueAfterClick(elem, elem.GetComponent<FliersInfo>().id);
        /*switch (elem.GetComponent<FliersInfo>().id)
        {
            case 1:
                SetValueAfterClick(elem, 1);
                break;
            case 2:
                if (currentViewId != 2)
                {
                    SetValueAfterClick(elem, 2);
                }
                break;
            default:
                Debug.LogError("No Repetoried Object");
                break;
        }*/


    }

    private void SetValueAfterClick(Transform elem, int _id)
    {
        if (currentViewId != _id)
        {
            if (currentViewId != 0)
            {
                listFliers[currentViewId - 1].GetComponent<Animator>().Play("FliersBack");
                listFliers[currentViewId - 1].GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            elem.GetComponent<Animator>().Play("Fliers");
            elem.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
            currentViewId = _id;
        }
    }

    private void OnRClickActions()  //-----Do action on right click-----
    {
        switch (currentViewId)
        {
            case 0:
                inFrontOfBoard = false;
                Camera.main.transform.parent.GetComponent<CameraMove>().enabled = true;
                Camera.main.transform.parent.GetComponent<MoveCameraToRail>().enabled = false;
                //GameObject.FindGameObjectWithTag("Player").gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().Resume();
                GameObject.FindGameObjectWithTag("Player").GetComponent<MovePerso>().canMove = true;
                listPNJ.gameObject.SetActive(true);

                listArrow.GetComponent<FliersMove>().currentPos = 0;
                listArrow.GetComponent<FliersMove>().lastNode.position = listArrow.GetComponent<FliersMove>().listPos[0].position;
                break;
            default:                
                listFliers[currentViewId - 1].GetComponent<Animator>().Play("FliersBack");
                currentViewId = 0;
                break;
        }
    }
}
