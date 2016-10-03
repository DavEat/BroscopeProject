using UnityEngine;
using System.Collections;

public class PlayerEnter : MonoBehaviour {

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (transform.parent.GetChild(0).GetComponent<LogoBoardClick>() != null)
                transform.parent.GetChild(0).GetComponent<LogoBoardClick>().Intrigger = true;
            else if (transform.parent.GetChild(0).GetComponent<InfoOnClick>() != null)
            {
                transform.parent.GetChild(0).GetComponent<InfoOnClick>().Intrigger = true;
                transform.parent.GetChild(0).GetComponent<InfoOnClick>().OnTriggerEnterrr();
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (transform.parent.GetChild(0).GetComponent<LogoBoardClick>() != null)
                transform.parent.GetChild(0).GetComponent<LogoBoardClick>().Intrigger = false;
            else if (transform.parent.GetChild(0).GetComponent<InfoOnClick>() != null)
            {
                transform.parent.GetChild(0).GetComponent<InfoOnClick>().Intrigger = false;
            }
        }
    }
}
