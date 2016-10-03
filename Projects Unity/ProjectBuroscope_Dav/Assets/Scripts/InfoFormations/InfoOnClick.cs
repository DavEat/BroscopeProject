using UnityEngine;
using System.Collections.Generic;

public class InfoOnClick : MonoBehaviour
{

    public bool inFrontOfBoard;
    public int currentViewId;
    public List<Transform> listButton;

    public List<SlideBarManagement> listSliderBarMan;

    public Transform listPNJ;

    public XmlReader xml;

    private Transform currentElemActive, lastClickedElem;

    public GameObject player;

    public bool Intrigger;

    void Update()
    {
        if (Intrigger)//(VRinfo.firstPersonCamera)
        {
            foreach (Transform t in listButton)
            {
                if (t == PointPos.hit.transform)
                    CheckClicked(t);
            }
            CheckClicked(null);
            /*if (true)
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
            }*/
        }
        /*else if (Intrigger)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward);
            checkClick(ray);
        }*/
    }

    public void OnTriggerEnterrr()
    {
        foreach (SlideBarManagement slide in listSliderBarMan)
            slide.SetSlideBar();
    }

    /*private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
            Intrigger = false;
    }*/

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
            else if (Input.GetButtonDown("Click") && elem != null)
                lastClickedElem = elem;
            else if (Input.GetButtonUp("Click") && elem != null)
            {
                if (lastClickedElem == elem)
                {
                    OnLClickActions(elem);
                    lastClickedElem = null;
                }
            }
            else if (Input.GetButtonUp("ClickReturn") && elem == null)
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
        if (elem.GetComponent<ButtonStat>() != null)
        if (elem.GetComponent<ButtonStat>().id == -10)  //---It's return button
        {
            currentElemActive.GetComponent<BoxCollider>().enabled = true;
            currentElemActive.gameObject.layer = 11;
            currentElemActive.GetComponent<AnimHexagone>().startlPos = currentElemActive.localPosition;
            currentElemActive.GetComponent<AnimHexagone>().targetPos = currentElemActive.GetComponent<AnimHexagone>().initialPos;
            currentElemActive.GetComponent<AnimHexagone>().targetScale = currentElemActive.GetComponent<AnimHexagone>().initialScale;
            currentElemActive.GetComponent<AnimHexagone>().displayContent = true;
            currentElemActive.GetComponent<AnimHexagone>().inAnim = true;
            currentElemActive = currentElemActive.parent.parent;
        }
        else
        {
            currentElemActive = elem;
            elem.GetComponent<BoxCollider>().enabled = false;
            if (elem.GetComponent<ButtonStat>().id > 1000)
                elem.gameObject.layer = 14;
            elem.GetComponent<AnimHexagone>().startlPos = elem.localPosition;
            elem.GetComponent<AnimHexagone>().targetPos = new Vector3(0, 0, -0.03f);
            elem.GetComponent<AnimHexagone>().targetScale = elem.GetComponent<AnimHexagone>().maxScale;
            elem.GetComponent<AnimHexagone>().displayContent = true;
            elem.GetComponent<AnimHexagone>().inAnim = true;
        }


    }

    private void OnRClickActions()  //-----Do action on right click-----
    {
        inFrontOfBoard = false;
        Camera.main.transform.parent.GetComponent<CameraMove>().enabled = true;
        Camera.main.transform.parent.GetComponent<MoveCameraToRail>().enabled = false;
        player.SetActive(true);
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().enabled)
            GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().Resume();
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovePerso>().canMove = true;
        listPNJ.gameObject.SetActive(true);
    }
}
