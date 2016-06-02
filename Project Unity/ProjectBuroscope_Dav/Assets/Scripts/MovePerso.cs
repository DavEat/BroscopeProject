using UnityEngine;
using System.Collections;

public class MovePerso : MonoBehaviour {

    public float angleOfView;

    public bool professor, canMove = true;

    private NavMeshAgent navA;

    void Awake()
    {
        navA = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        if (canMove)
            if (Input.GetMouseButton(0) || Input.touchCount > 0)
            {
                RaycastHit hit;
                Ray ray;
                #if (UNITY_EDITOR)
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //for touch device
                #elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
                   ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                #elif UNITY_WEBPLAYER
                    if (Input.touchCount > 0)
                        ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    else ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                #endif

                //Debug.DrawRay(mousePos, Vector3.back*100, Color.blue);
                if (Physics.Raycast(ray, out hit, 100))
                {
                    //Debug.Log("HIT with" + hit.transform.name);
                    navA.SetDestination(hit.point);

                    if (hit.transform.tag == "Professor")
                        professor = true;
                    else if (professor)
                        professor = false;
                }            
            }
    }
}
