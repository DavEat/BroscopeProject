using UnityEngine;
using System.Collections.Generic;

public class LogoBoardClick : MonoBehaviour {

    public bool inFrontOfBoard;
    public int currentViewId;
    public List<Transform> listView;

    private Transform currentElemActive;

    void Awake()  //-----Initialisation-----
    {
        foreach (Transform t in listView)
            t.GetComponent<listElemInView>().Instentiate();
    }

	void Update () {
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
            #elif UNITY_WEBPLAYER
                Ray ray;
                if (Input.touchCount > 0)                        
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                else ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                checkClick(ray);
            #endif
        }

        CheckFade();
    }

    private void checkClick(Ray ray)
    {
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);
        if (Physics.Raycast(ray, out hit, 100))
        {
            foreach (Transform t in listView)
            {
                List<Transform> listButton, listTextButton;
                if (t.gameObject.activeSelf)
                {
                    listButton = t.GetComponent<listElemInView>().listButton;
                    listTextButton = t.GetComponent<listElemInView>().listTextButton;
                    for (int i = 0; i < listButton.Count; i++)
                    {
                        if (listButton[i] == hit.collider.transform)
                        {
                            ButtonInteraction.ButtonChangeSize(listButton[i], listButton[i].GetComponent<LogoStat>().maxSize);
                            ButtonInteraction.ButtonChangeSize(listTextButton[i], listTextButton[i].GetComponent<LogoStat>().maxSize);
                            CheckClick(listButton[i]);
                        }
                        else if ((Vector2)listButton[i].localScale != listButton[i].GetComponent<LogoStat>().initialSize)
                        {
                            ButtonInteraction.ButtonChangeSize(listButton[i], listButton[i].GetComponent<LogoStat>().initialSize);
                            ButtonInteraction.ButtonChangeSize(listTextButton[i], listTextButton[i].GetComponent<LogoStat>().initialSize);
                        }
                    }                    
                }
            }            
        }
        CheckClick(null);
    }

    private void CheckClick(Transform elem)  //-----Check if elem is clicked of touched-----
    {
        #if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
            if (Input.GetMouseButtonUp(0) && elem != null)
            {
                OnLClickActions(elem);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                OnRClickActions();
            }
        #elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
             if (Input.touchCount > 0)
                if (Input.touches[0].phase == TouchPhase.Ended)
                    OnLClickActions(elem);
            if (Input.touchCount >= 2)
                if (Input.touches[0].phase == TouchPhase.Ended && Input.touches[1].phase == TouchPhase.Ended)
                    OnRClickActions();
        #endif
    }

    private void OnLClickActions(Transform elem)  //-----Do action on left click-----
    {
        switch (elem.name)
        {
            case "word-logo":
                if (!elem.GetComponent<LogoStat>().active)
                {
                    for (int i = 0; i < transform.GetChild(0).GetComponent<listElemInView>().listButton.Count; i++)
                        transform.GetChild(0).GetComponent<listElemInView>().listButton[i].GetComponent<LogoStat>().fadeOut = true;
                    for (int i = 0; i < transform.GetChild(0).GetComponent<listElemInView>().listTextButton.Count; i++)
                        transform.GetChild(0).GetComponent<listElemInView>().listTextButton[i].GetComponent<LogoStat>().fadeOut = true;

                    currentElemActive = transform.GetChild(1);
                    currentElemActive.gameObject.SetActive(true);
                    for (int i = 0; i < currentElemActive.GetComponent<listElemInView>().listButton.Count; i++)
                        currentElemActive.GetComponent<listElemInView>().listButton[i].GetComponent<LogoStat>().fadeIn = true;
                    for (int i = 0; i < currentElemActive.GetComponent<listElemInView>().listTextButton.Count; i++)
                        currentElemActive.GetComponent<listElemInView>().listTextButton[i].GetComponent<LogoStat>().fadeIn = true;
                }
                break;
            default: Debug.Log("red");
                break;
        }


    }

    private void OnRClickActions()  //-----Do action on right click-----
    {
        if (transform.GetChild(0).gameObject.activeSelf && !transform.GetChild(0).GetComponent<listElemInView>().listButton[0].GetComponent<LogoStat>().fadeIn && !transform.GetChild(0).GetComponent<listElemInView>().listButton[0].GetComponent<LogoStat>().fadeOut)
        {
            inFrontOfBoard = false;
            Camera.main.GetComponent<CameraMove>().enabled = true;
            Camera.main.GetComponent<MoveCameraToRail>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovePerso>().canMove = true;
        }
        //if (!transform.GetChild(0).GetComponent<listElemInView>().listButton[0].GetComponent<LogoStat>().fadeIn && !transform.GetChild(0).GetComponent<listElemInView>().listButton[0].GetComponent<LogoStat>().fadeOut)
        {
            for (int i = 0; i < currentElemActive.GetComponent<listElemInView>().listButton.Count; i++)
                currentElemActive.GetComponent<listElemInView>().listButton[i].GetComponent<LogoStat>().fadeOut = true;
            for (int i = 0; i < currentElemActive.GetComponent<listElemInView>().listTextButton.Count; i++)
                currentElemActive.GetComponent<listElemInView>().listTextButton[i].GetComponent<LogoStat>().fadeOut = true;

            transform.GetChild(0).gameObject.SetActive(true);
            for (int i = 0; i < transform.GetChild(0).GetComponent<listElemInView>().listButton.Count; i++)
                transform.GetChild(0).GetComponent<listElemInView>().listButton[i].GetComponent<LogoStat>().fadeIn = true;
            for (int i = 0; i < transform.GetChild(0).GetComponent<listElemInView>().listTextButton.Count; i++)
                transform.GetChild(0).GetComponent<listElemInView>().listTextButton[i].GetComponent<LogoStat>().fadeIn = true;
        }
    }

    private void CheckFade()
    {
        float speed = 0f; // time to do the fade
        foreach (Transform t in listView)
        {
            for (int i = 0; i < t.GetComponent<listElemInView>().listButton.Count; i++)
            {
                if (t.GetComponent<listElemInView>().listButton[i].GetComponent<LogoStat>().fadeIn)
                    t.GetComponent<listElemInView>().listButton[i].GetComponent<LogoStat>().fadeTime = fade(t.GetComponent<listElemInView>().listButton[i], true, t.GetComponent<listElemInView>().listButton[i].GetComponent<LogoStat>().fadeTime, speed, 255);
                else if (t.GetComponent<listElemInView>().listButton[i].GetComponent<LogoStat>().fadeOut)
                    t.GetComponent<listElemInView>().listButton[i].GetComponent<LogoStat>().fadeTime = fade(t.GetComponent<listElemInView>().listButton[i], true, t.GetComponent<listElemInView>().listButton[i].GetComponent<LogoStat>().fadeTime, speed, 0);
            }

            for (int i = 0; i < t.GetComponent<listElemInView>().listTextButton.Count; i++)
            {
                if (t.GetComponent<listElemInView>().listTextButton[i].GetComponent<LogoStat>().fadeIn)
                    t.GetComponent<listElemInView>().listTextButton[i].GetComponent<LogoStat>().fadeTime = fade(t.GetComponent<listElemInView>().listTextButton[i], false, t.GetComponent<listElemInView>().listTextButton[i].GetComponent<LogoStat>().fadeTime, speed, 255);
                else if (t.GetComponent<listElemInView>().listTextButton[i].GetComponent<LogoStat>().fadeOut)
                    t.GetComponent<listElemInView>().listTextButton[i].GetComponent<LogoStat>().fadeTime = fade(t.GetComponent<listElemInView>().listTextButton[i], false, t.GetComponent<listElemInView>().listTextButton[i].GetComponent<LogoStat>().fadeTime, speed, 0);
            }

        }
    }

    private float fade(Transform t, bool sprite, float time, float duration, float alpha)
    {
        float timee = time, a;

        if (sprite)
        {
            a = Mathf.Lerp(t.GetComponent<SpriteRenderer>().color.a, alpha, timee);
            t.GetComponent<SpriteRenderer>().color = new Color(t.GetComponent<SpriteRenderer>().color.r, t.GetComponent<SpriteRenderer>().color.g, t.GetComponent<SpriteRenderer>().color.b, a);
        }
        else
        {
            a = Mathf.Lerp(t.GetComponent<TextMesh>().color.a, alpha, timee);
            t.GetComponent<TextMesh>().color = new Color(t.GetComponent<TextMesh>().color.r, t.GetComponent<TextMesh>().color.g, t.GetComponent<TextMesh>().color.b, a);
        }


        if (timee < 1)
            timee += Time.deltaTime / duration;
        else
        {
            if (t.GetComponent<LogoStat>().fadeIn)
            {
                t.GetComponent<LogoStat>().fadeIn = false;
            }
            if (t.GetComponent<LogoStat>().fadeOut)
            {
                t.GetComponent<LogoStat>().fadeOut = false;
                if (t.parent.parent.gameObject.activeSelf)
                    t.parent.parent.gameObject.SetActive(false);
            }
            timee = 0;
        }
        return timee;
    }
}
