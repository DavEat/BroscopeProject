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

    private Transform lastClickedElem;

    public bool Intrigger;

    void Awake()  //-----Initialisation-----
    {
        foreach (Transform t in listView)
            t.GetComponent<listElemInView>().Instentiate();
        currentElemActive = listView[0];

        player = GameObject.FindGameObjectWithTag("Player");
    }

	void Update () {

        if (Intrigger)//(VRinfo.firstPersonCamera)
        {

            foreach (Transform t in listView)
            {
                List<Transform> listButton;
                if (t.gameObject.activeSelf)
                {
                    listButton = t.GetComponent<listElemInView>().listButton;

                    if (PointPos.hit.transform.GetComponent<LogoStat>() != null)
                    {
                        ButtonInteraction.ButtonChangeSize(PointPos.hit.collider.transform, PointPos.hit.collider.transform.GetComponent<LogoStat>().maxSize);
                        //CheckClicked(listButton[i]);
                    }

                    for (int i = 0; i < listButton.Count; i++)
                        if (listButton[i] != PointPos.hit.transform)
                            if ((Vector2)listButton[i].localScale != listButton[i].GetComponent<LogoStat>().initialSize)
                                ButtonInteraction.ButtonChangeSize(listButton[i], listButton[i].GetComponent<LogoStat>().initialSize);
                    //ButtonInteraction.ButtonChangeSize(listTextButton[i], listTextButton[i].GetComponent<LogoStat>().initialSize);             
                }
            }
            CheckClicked(PointPos.hit.transform);
        }
            /*if (true)//(inFrontOfBoard)
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
        else if (Intrigger)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward);
            checkClick(ray);
        }*/
    }

    /*private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
            Intrigger = true;
    }

    private void OnTriggerExit(Collider col)
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
            foreach (Transform t in listView)
            {
                List<Transform> listButton;
                if (t.gameObject.activeSelf)
                {
                    listButton = t.GetComponent<listElemInView>().listButton;
                    
                    if (hit.transform.GetComponent<LogoStat>() != null)
                    {
                        ButtonInteraction.ButtonChangeSize(hit.collider.transform, hit.collider.transform.GetComponent<LogoStat>().maxSize);
                        //CheckClicked(listButton[i]);
                    }

                    for (int i = 0; i < listButton.Count; i++)
                        if (listButton[i] != hit.transform)
                        if ((Vector2)listButton[i].localScale != listButton[i].GetComponent<LogoStat>().initialSize)
                            ButtonInteraction.ButtonChangeSize(listButton[i], listButton[i].GetComponent<LogoStat>().initialSize);
                            //ButtonInteraction.ButtonChangeSize(listTextButton[i], listTextButton[i].GetComponent<LogoStat>().initialSize);             
                }
            }            
        }
        CheckClicked(hit.transform);
    }

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
            else if (Input.GetMouseButtonUp(1))
                OnRClickActions();
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
            else if (Input.GetButtonUp("ClickReturn"))
            {
                OnRClickActions();
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

    private void OnLClickActions(Transform elem)  //-----Do action on left click-----
    {
        if (elem.GetComponent<CheckBoxManagement>() != null)
        {
            elem.GetComponent<CheckBoxManagement>().Mangement();
        }
        else if (elem.GetComponent<LogoStat>() != null && elem.GetComponent<LogoStat>().id == -42)
        {
            LoadXmlValueQCM(currentViewId - 20200, 4, 1);
            currentElemActive.gameObject.SetActive(false);
            listView[4].gameObject.SetActive(true);
            currentElemActive = listView[4];            
        }
        else if (elem.GetComponent<LogoStat>() != null && elem.GetComponent<LogoStat>().id == -43)
        {
            if (!elem.GetComponent<LogoStat>().active)
            {
                int score = CountScore();
                listView[4].GetComponent<listElemInView>().listTextButton[listView[4].GetComponent<listElemInView>().listTextButton.Count - 1].GetComponent<UnityEngine.UI.Text>().text = score + " sur 3";
                //listView[4].GetComponent<listElemInView>().listTextToChange[listView[4].GetComponent<listElemInView>().listTextToChange.Count - 1].GetComponent<UnityEngine.UI.Text>().text = "Votre score à ce qcm est";

                SaveGame.WriteInText(score,  currentViewId - 20201, PlayerInformation.path);

                ToggleScoreDisplayQCM();
                elem.GetComponent<LogoStat>().active = true;
            }
            else
            {
                ToggleScoreDisplayQCM();
                elem.GetComponent<LogoStat>().active = false;
                currentViewId = 0;
                currentElemActive.gameObject.SetActive(false);
                listView[0].gameObject.SetActive(true);
                currentElemActive = listView[0];
            }         
        }
        else if (elem.GetComponent<LogoStat>() != null)
        {
            currentViewId = elem.GetComponent<LogoStat>().id;
            if (elem.GetComponent<LogoStat>().id < 100)
            {
                LoadXmlValueBoard(currentViewId, 1);
                currentViewId += 100;

                currentElemActive.gameObject.SetActive(false);
                listView[1].gameObject.SetActive(true);
                currentElemActive = listView[1];

                listView[1].GetChild(0).GetChild(listView[1].GetChild(0).childCount - 2).GetComponent<LogoStat>().id = 100 + elem.GetComponent<LogoStat>().id;
                listView[1].GetChild(0).GetChild(listView[1].GetChild(0).childCount - 1).GetComponent<LogoStat>().id = 200 + elem.GetComponent<LogoStat>().id;
            }
            else if (elem.GetComponent<LogoStat>().id < 10000 && elem.GetComponent<LogoStat>().id > 200)
            {                
                LoadXmlValueQCMPreText(currentViewId - 200, 3, 1);
                currentViewId += 20000;
                currentElemActive.gameObject.SetActive(false);
                listView[3].gameObject.SetActive(true);
                currentElemActive = listView[3];
            }
            else if (elem.GetComponent<LogoStat>().id < 10000)
            {
                LoadXmlValueProgramme(currentViewId - 100, 2, 0);
                currentViewId += 10000;
                currentElemActive.gameObject.SetActive(false);
                listView[2].gameObject.SetActive(true);
                currentElemActive = listView[2];
            }
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
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().enabled)
                GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().Resume();
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovePerso>().canMove = true;
            listPNJ.gameObject.SetActive(true);
        }
        else if (currentViewId < 200)
        {
            currentViewId = currentViewId - 100;
            currentElemActive.gameObject.SetActive(false);
            listView[0].gameObject.SetActive(true);
            currentElemActive = listView[0];
        }
        else if (currentViewId < 1000)
        {
            currentViewId = currentViewId - 200;
            currentElemActive.gameObject.SetActive(false);
            listView[0].gameObject.SetActive(true);
            currentElemActive = listView[0];
        }
        else if (currentViewId < 20000)
        {
            currentViewId = currentViewId - 10000;
            currentElemActive.gameObject.SetActive(false);
            listView[1].gameObject.SetActive(true);
            currentElemActive = listView[1];
        }
        else if (currentViewId < 100000)
        {
            currentViewId = currentViewId - 20000;
            currentElemActive.gameObject.SetActive(false);
            listView[1].gameObject.SetActive(true);
            currentElemActive = listView[1];
        }
    }

    /// <summary>
    /// Use to get value for the bord
    /// </summary>
    /// <param name="_id">id of the cours</param>
    /// <param name="idView">id of the view in the list of view</param>
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

    /// <summary>
    /// Use to load xml value for programme view
    /// </summary>
    /// <param name="_id">id of the cours</param>
    /// <param name="idView">id of the view in the list of view</param>
    /// <param name="section">section in the cours that you want</param>
    private void LoadXmlValueProgramme(int _id, int idView, int section)
    {
        List<Transform> listTB = listView[idView].GetComponent<listElemInView>().listTextButton;
        List<Transform> listT = listView[idView].GetComponent<listElemInView>().listTextToChange;

        for (int i = 0; i < xml.programmeQCM[_id][section].Count; i++)
        {
            listTB[i].GetComponent<UnityEngine.UI.Text>().text = xml.getXmlValueProg(_id, section, i, 0);  //title

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

    /// <summary>
    /// Use to get qcm value in the xml
    /// </summary>
    /// <param name="_id">id of the cours</param>
    /// <param name="idView">id of the view in the list of view</param>
    /// <param name="section">section in the cours that you want</param>
    private void LoadXmlValueQCM(int _id, int idView, int section)
    {
        List<Transform> listAnswers = listView[idView].GetComponent<listElemInView>().listTextButton;
        List<Transform> listQuestions = listView[idView].GetComponent<listElemInView>().listTextToChange;
        int n = 0;
        for (int i = 1; i < xml.programmeQCM[_id][section].Count; i++)
        {
            listQuestions[i - 1].GetComponent<UnityEngine.UI.Text>().text = xml.getXmlValueProg(_id, section, i, 0);  //question

            List<string> listAnswer = new List<string>();
            for (int j = 1; j < xml.programmeQCM[_id][section][i].Count; j++)
                listAnswer.Add(xml.getXmlValueProg(_id, section, i, j));

            bool alrGood = false; // allready assign good answer
            
            for (int j = 1; j < xml.programmeQCM[_id][section][i].Count; j++)
            {
                int rand = (int)Random.Range(0, listAnswer.Count);
                if (!alrGood && rand == 0)
                {
                    alrGood = true;
                    listQuestions[i - 1].GetComponent<CheckBoxGoodAnswer>().goodAnswer = n % 3;
                    if (i - 1 == 1)
                        listQuestions[i - 1].GetComponent<CheckBoxGoodAnswer>().goodAnswer = 2 - listQuestions[i - 1].GetComponent<CheckBoxGoodAnswer>().goodAnswer;
                }
                    
                listAnswers[n].GetComponent<UnityEngine.UI.Text>().text = listAnswer[rand];
                listAnswer.RemoveAt(rand);
                n++;
            }               
        }
    }

    /// <summary>
    /// Use to get qcm pre text in the xml
    /// </summary>
    /// <param name="_id">id of the cours</param>
    /// <param name="idView">id of the view in the list of view</param>
    /// <param name="section">section in the cours that you want</param>
    private void LoadXmlValueQCMPreText(int _id, int idView, int section)
    {
        List<Transform> listQuestions = listView[idView].GetComponent<listElemInView>().listTextToChange;

        listQuestions[0].GetComponent<UnityEngine.UI.Text>().text = "A propos de " + xml.getXmlValueCours(_id, 0, 0);
        listQuestions[1].GetComponent<UnityEngine.UI.Text>().text = xml.getXmlValueProg(_id, section, 0, 0);
        
    }

    private int CountScore()
    {
        int score = 0;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j <3; j++)
            {
                if (listView[4].GetComponent<listElemInView>().listButton[3 * i + j].GetComponent<CheckBoxManagement>().checkedd)
                {
                    if (listView[4].GetComponent<listElemInView>().listTextToChange[i].GetComponent<CheckBoxGoodAnswer>().goodAnswer == j)
                        score++;
                    listView[4].GetComponent<listElemInView>().listButton[3 * i + j].GetComponent<CheckBoxManagement>().checkedd = false;
                    listView[4].GetComponent<listElemInView>().listButton[3 * i + j].GetComponent<UnityEngine.UI.Image>().sprite = listView[4].GetComponent<listElemInView>().listButton[3 * i + j].GetComponent<CheckBoxManagement>().uncheckSprite;
                }
                    
            }
        return score;
    }

    private void ToggleScoreDisplayQCM()
    {
        for (int i = 0; i < listView[4].GetComponent<listElemInView>().listButton.Count - 1; i++)
            listView[4].GetComponent<listElemInView>().listButton[i].gameObject.SetActive(!listView[4].GetComponent<listElemInView>().listButton[i].gameObject.activeSelf);

        for (int i = 0; i < listView[4].GetComponent<listElemInView>().listTextButton.Count - 1; i++)
            listView[4].GetComponent<listElemInView>().listTextButton[i].gameObject.SetActive(!listView[4].GetComponent<listElemInView>().listTextButton[i].gameObject.activeSelf);

        for (int i = 0; i < listView[4].GetComponent<listElemInView>().listTextToChange.Count - 1; i++)
            listView[4].GetComponent<listElemInView>().listTextToChange[i].gameObject.SetActive(!listView[4].GetComponent<listElemInView>().listTextToChange[i].gameObject.activeSelf);

        listView[4].GetComponent<listElemInView>().listTextButton[listView[4].GetComponent<listElemInView>().listTextButton.Count - 1].gameObject.SetActive(!listView[4].GetComponent<listElemInView>().listTextButton[listView[4].GetComponent<listElemInView>().listTextButton.Count - 1].gameObject.activeSelf);
        listView[4].GetComponent<listElemInView>().listTextToChange[listView[4].GetComponent<listElemInView>().listTextToChange.Count - 1].gameObject.SetActive(!listView[4].GetComponent<listElemInView>().listTextToChange[listView[4].GetComponent<listElemInView>().listTextToChange.Count - 1].gameObject.activeSelf);
    }
}
