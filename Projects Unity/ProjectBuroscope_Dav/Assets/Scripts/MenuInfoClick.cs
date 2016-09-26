using UnityEngine;
using System.Collections;

public class MenuInfoClick : MonoBehaviour {

    void Update ()
    {

        if (PointPos.hit.transform.GetComponent<MenuButtonStat>() != null)
            CheckClicked(PointPos.hit.transform);
        /*
        if (true)//(VRinfo.firstPersonCamera)
        {
            if (true)//(inFrontOfBoard)
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
        else if (true)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward);
            checkClick(ray);
        }*/
    }

    private void checkClick(Ray ray)
    {
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.GetComponent<MenuButtonStat>() != null)
            {
                CheckClicked(hit.transform);
                Debug.Log("hit : " + hit.transform.name);
            }

        }
    }

    private Transform lastClickedElem;
    private void CheckClicked(Transform elem)  //-----Check if elem is clicked of touched-----
    {
        #if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
            if (Input.GetMouseButtonDown(0) && elem != null)
                lastClickedElem = elem;
            else if (Input.GetMouseButtonUp(0) && elem != null)
            {
                if (lastClickedElem == elem)
                {
                    OnLClickActions(elem);
                    lastClickedElem = null;
                }                    
            }
            else if (Input.GetButtonDown("Click"))
                lastClickedElem = elem;
            else if (Input.GetButtonUp("Click"))
            {
                if (lastClickedElem == elem)
                {
                    OnLClickActions(elem);
                    lastClickedElem = null;
                }
            }
        #elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
            if (Input.touchCount == 1)
            {
                if (Input.touches[0].phase == TouchPhase.Began && elem != null)
                    lastClickedElem = elem;
                else if (Input.touches[0].phase == TouchPhase.Ended && elem != null)
                    if (lastClickedElem = elem)
                        OnLClickActions(elem);
            }
            else if (Input.touchCount == 2)
                if (Input.touches[0].phase == TouchPhase.Ended && Input.touches[1].phase == TouchPhase.Ended)
                    OnRClickActions();
        #endif
    }

    private void OnLClickActions(Transform elem)
    {
        if (elem.GetComponent<MenuButtonStat>().type == 1)
        {
            switch (elem.GetComponent<MenuButtonStat>().value)
            {
                case 1:
                    elem.parent.GetComponent<MainMenu>().Play();
                    break;
                case 2:
                    elem.parent.GetComponent<MainMenu>().Credit();
                    break;
                case 3:
                    elem.parent.GetComponent<MainMenu>().Quit();
                    break;
                case 4:
                    elem.GetComponent<MenuButtonStat>().script.GetComponent<MainMenu>().MainMenuForCredit();
                    break;
            }
        }
        else if (elem.GetComponent<MenuButtonStat>().type == 2)
        {
            switch (elem.GetComponent<MenuButtonStat>().value)
            {
                case 1:
                    elem.parent.GetComponent<FindAllSaveFiles>().Play();
                    break;
                case 2:
                    elem.parent.GetComponent<FindAllSaveFiles>().ReturnMainMenu();
                    break;
                case 3:
                    elem.parent.GetComponent<FindAllSaveFiles>().Validation();
                    break;
                case 4:
                    elem.GetComponent<KeyboardKeyInfo>().Writte();
                    break;
                case 5:
                    elem.parent.GetComponent<Keyboard>().Shifting();
                    //elem.GetComponent<UnityEngine.UI.Dropdown>().
                    break;
                case 6:
                    elem.parent.GetComponent<Keyboard>().Eraise();
                    break;
                case 7:
                    elem.parent.GetComponent<ChoseAvatar>().Validation();
                    break;
                case 8:
                    elem.parent.GetComponent<FindAllSaveFiles>().ReturnChoseProfile();
                    break;

            }
        }
    }
}
