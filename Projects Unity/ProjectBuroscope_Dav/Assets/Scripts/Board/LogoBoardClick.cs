using UnityEngine;
using System.Collections.Generic;

public class LogoBoardClick : MonoBehaviour {

    public bool inFrontOfBoard;
    public int currentViewId;
    public List<Transform> listView;

    public Transform listPNJ;

    public XmlReader xml;

    private Transform currentElemActive;

    public GameObject player;

    void Awake()  //-----Initialisation-----
    {
        foreach (Transform t in listView)
            t.GetComponent<listElemInView>().Instentiate();

        player = GameObject.FindGameObjectWithTag("Player");
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
            #endif
        }
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
                            CheckClicked(listButton[i]);
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
        currentViewId = elem.GetComponent<LogoStat>().id;
        if (elem.GetComponent<LogoStat>().id < 100)
        {      
            LoadXmlValueBoard(currentViewId, 1);
            currentViewId += 100;

            listView[0].gameObject.SetActive(false);
            listView[1].gameObject.SetActive(true);

            listView[1].GetChild(0).GetChild(listView[1].GetChild(0).childCount - 1).GetComponent<LogoStat>().id = 100 + elem.GetComponent<LogoStat>().id;
        }
        else if (elem.GetComponent<LogoStat>().id < 10000)
        {
            LoadXmlValueProgramme(currentViewId - 100, 2, 0);
            currentViewId += 10000;
            listView[1].gameObject.SetActive(false);
            listView[2].gameObject.SetActive(true);

            //elem.GetChild(0).GetChild(elem.GetChild(0).childCount - 1).GetComponent<LogoStat>().id = 100 + elem.GetComponent<LogoStat>().id;
        }
    }

    private void OnRClickActions()  //-----Do action on right click-----
    {
        if (currentViewId < 10)
        {
            inFrontOfBoard = false;
            Camera.main.transform.parent.GetComponent<CameraMove>().enabled = true;
            Camera.main.transform.parent.GetComponent<MoveCameraToRail>().enabled = false;
            player.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().Resume();
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovePerso>().canMove = true;
            listPNJ.gameObject.SetActive(true);
        }
        else if (currentViewId < 1000)
        {
            currentViewId = currentViewId - 100;
            listView[1].gameObject.SetActive(false);
            listView[0].gameObject.SetActive(true);
        }
        else if (currentViewId < 100000)
        {
            currentViewId = currentViewId - 10000;
            listView[2].gameObject.SetActive(false);
            listView[1].gameObject.SetActive(true);
        }
    }

    private void LoadXmlValueBoard(int _id, int idView)
    {        
        List<Transform> listT = listView[idView].GetComponent<listElemInView>().listTextToChange;

        for (int i = 0; i < listT.Count; i++)
            listT[i].GetComponent<UnityEngine.UI.Text>().text = xml.getXmlValueCours(_id, 2, i);
        string s = "";
        for (int i = 0; i < xml.cours[_id][3].Count; i++)
            s += xml.getXmlValueCours(_id, 3, i) + "\n";
        listView[idView].GetComponent<listElemInView>().objectif.GetComponent<UnityEngine.UI.Text>().text = s;
    }

    private void LoadXmlValueProgramme(int _id, int idView, int section)
    {
        List<Transform> listTB = listView[idView].GetComponent<listElemInView>().listTextButton;
        List<Transform> listT = listView[idView].GetComponent<listElemInView>().listTextToChange;

        for (int i = 0; i < xml.programmeQCM[_id][section].Count; i++)
        {
            listTB[i].GetComponent<UnityEngine.UI.Text>().text = xml.getXmlValueProg(_id, section, i, 0);

            string s = "";
            for (int j = 1; j < xml.programmeQCM[_id][section][i].Count; j++)
                s += xml.getXmlValueProg(_id, section, i, j) + "\n";
            listT[i].GetComponent<UnityEngine.UI.Text>().text = s;
        }

        if (xml.programmeQCM[_id][section].Count < 6)
            for (int i = xml.programmeQCM[_id][section].Count; i < 6; i++)
            {
                listTB[i].GetComponent<UnityEngine.UI.Text>().text = "";
                listT[i].GetComponent<UnityEngine.UI.Text>().text = "";
            }
    }

    /*private void CheckFade()
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
    }*/

    /*private float fade(Transform t, bool sprite, float time, float duration, float alpha)
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
    }*/
}
