using UnityEngine;
using System.Collections;

public class MovePerso : MonoBehaviour {

    public float angleOfView;

    public bool professor;

    private NavMeshAgent navA;

    void Awake()
    {
        navA = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
