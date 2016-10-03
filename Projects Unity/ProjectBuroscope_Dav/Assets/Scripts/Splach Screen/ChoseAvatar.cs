using UnityEngine;
using System.Collections.Generic;

public class ChoseAvatar : MonoBehaviour {

    private int choseavatarIndex = 1;
    [SerializeField]
    private List<GameObject> listHallow;
    [SerializeField]
    private FindAllSaveFiles files;

	void Update ()
    {
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
                if (hit.transform.CompareTag("ChoseAvatar"))
                {
                    choseavatarIndex = int.Parse(hit.transform.name[0].ToString()) - 1;
                    if (!hit.transform.GetChild(2).gameObject.activeSelf)
                        hit.transform.GetChild(2).gameObject.SetActive(true);

                    for (int i = 0; i < listHallow.Count; i++)
                        if (i != choseavatarIndex)
                            listHallow[i].SetActive(false);
                }
            }            
        }
	}

    public void Validation()
    {
        files.Finalisation(choseavatarIndex);
        gameObject.SetActive(false);
    }
}
