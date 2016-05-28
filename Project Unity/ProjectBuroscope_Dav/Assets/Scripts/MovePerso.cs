using UnityEngine;
using System.Collections;

public class MovePerso : MonoBehaviour {

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
            }
        }
    }
}
