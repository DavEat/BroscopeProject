using UnityEngine;
using System.Collections;

public class Secreter : MonoBehaviour {

    public bool active;

    public float distanceToTalking = 3;
    public Transform player, canvasElement;

    void Update()
    {
        if (!active)
        {
            if (player.GetComponent<VRMove>().professor)
                if ((Vector3.Distance(player.position, transform.position)) < distanceToTalking)
                {
                    canvasElement.gameObject.SetActive(true);
                    player.GetComponent<MovePerso>().professor = false;
                    player.GetComponent<MovePerso>().canMove = false;
                    active = true;
                }
        }
        else
        {
            #if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
                if (Input.GetMouseButtonDown(1))
                    exitMenu();
            #elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
                if (Input.touchCount >= 2)                
                    if (Input.touches[0].phase == TouchPhase.Ended && Input.touches[1].phase == TouchPhase.Ended)
                        exitMenu();
            #endif
        }
    }

    private void exitMenu()
    {
        bool checkOK = true;
           if (canvasElement.GetChild(0).GetComponent<OnClick>().grow)
            checkOK = false;

        if (checkOK)
        {
            canvasElement.gameObject.SetActive(false);
            player.GetComponent<MovePerso>().canMove = true;
            active = false;
        }
    }
}
