using UnityEngine;
using System.Collections;

public class VRMove : MonoBehaviour {

    //public float angleOfView;
    public bool professor, canMove = true;

    public Vector2 acc, minMove;

    private NavMeshAgent navA;

    void Awake()
    {
        navA = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {

        CamMove();

        Move();
    }

    private void CamMove()
    {
        float x, y;
        acc = Input.acceleration;
        if (Mathf.Abs(acc.x) > minMove.x)
            x = acc.x;
        else x = 0;
        if (Mathf.Abs(acc.y) > minMove.y)
            y = acc.y;
        else y = 0;

        transform.GetChild(0).Rotate(x, y, 0);
    }

    private void Move ()
    {
        if (canMove)

            if (Input.GetMouseButton(0) || Input.touchCount > 0)
            {
                Debug.Log("Cursur");

                RaycastHit hit;
                Ray ray = new Ray();
                #if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
                    ray.origin = transform.GetChild(0).position;
                    ray.direction = transform.GetChild(0).rotation * Vector3.forward;
                //for touch device
                #elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
                   ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                #elif UNITY_WEBPLAYER
                    if (Input.touchCount > 0)
                        ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    else ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                #endif                

                Debug.DrawRay(transform.GetChild(0).position, transform.GetChild(0).rotation * Vector3.forward * 100, Color.red);
                if (Physics.Raycast(ray, out hit, 100))
                {
                    Debug.Log("HIT with" + hit.transform.name);
                    navA.SetDestination(hit.point);

                    if (hit.transform.tag == "Professor")
                        professor = true;
                    else if (professor)
                        professor = false;
                }            
            }
    }
}
