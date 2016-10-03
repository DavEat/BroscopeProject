using UnityEngine;
using System.Collections;

public class MovePerso : MonoBehaviour {

    public float angleOfView;
    public bool professor, canMove = true;

    public Transform professorT;

    private NavMeshAgent navA;
    private CharacterController rb;

    void Awake()
    {
        navA = GetComponent<NavMeshAgent>();
        rb = GetComponent<CharacterController>();
    }

    void Update ()
    {
        if (canMove)
        {
            if (VRinfo.firstPersonCamera)
            {
                if (!navA.enabled)
                    navA.enabled = true;
                if (rb.enabled)
                    rb.enabled = false;
                if (!transform.GetChild(1).gameObject.activeSelf)
                    transform.GetChild(1).gameObject.SetActive(true);

                if (Input.GetMouseButton(0) || Input.touchCount > 0)
                {
                    RaycastHit hit;
                    Ray ray;
                    #if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
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
                        {
                            professor = true;
                            professorT = hit.transform;
                        }
                        else if (professor)
                        {
                            professor = false;
                            professorT = null;
                        }
                    }
                }
            }
            else if (!VRinfo.VRactive)
            {
                if (!rb.enabled)
                    rb.enabled = true;
                if (navA.enabled)
                    navA.enabled = false;
                if (transform.GetChild(1).gameObject.activeSelf)
                    transform.GetChild(1).gameObject.SetActive(false);

                Vector3 h = Vector3.zero, v = Vector3.zero;

                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
                    h = Vector3.right * Input.GetAxis("Horizontal");
                if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f)
                    v = Vector3.forward * Input.GetAxis("Vertical");
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f)
                    rb.Move(transform.rotation * (h + v) * VRinfo.speed);
                /*
                if (Input.GetButton("Click"))
                {
                    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        if (!hit.transform.CompareTag("BoardElem"))
                            navA.SetDestination(hit.point);
                    }
                }
                else if (navA.velocity.x > 0 || navA.velocity.z > 0)
                    navA.SetDestination(transform.position);*/

                if (Input.GetButton("Click"))
                {
                    rb.Move(transform.rotation * Vector3.forward * VRinfo.speed);
                }
                else if (rb.velocity.x > 0 || rb.velocity.z > 0)
                    rb.Move(Vector3.zero);

            }
            else //---Oculus is active---
            {

            }
        }
    }
}
