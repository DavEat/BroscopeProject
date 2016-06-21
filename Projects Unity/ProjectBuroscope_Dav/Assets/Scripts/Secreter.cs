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
            if (player.GetComponent<MovePerso>().professor)
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
            if (Input.GetMouseButtonDown(1) || Input.touchCount >= 2)
            {
                bool checkOK = true;
                if (canvasElement.childCount > 0)
                    for (int i = 0; i < canvasElement.childCount; i++)
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
    }
}
