using UnityEngine;
using System.Collections;

public class MoveCameraToRail : MonoBehaviour {
        
    public float speed;
    public int currentNode;

    public Transform listNodes;

	void Update ()
    {
        if (Vector3.Distance(transform.position, listNodes.GetChild(currentNode).position) < Time.deltaTime * speed)
        {
            if (currentNode < listNodes.childCount - 1)
                currentNode++;
        }
        else
        {
            Vector3 direction = transform.position - listNodes.GetChild(currentNode).position;
            direction = new Vector3 (direction.x, -direction.y, direction.z).normalized;
            transform.Translate(direction * Time.deltaTime * speed);

            transform.eulerAngles = Vector3.LerpUnclamped(transform.eulerAngles, new Vector3(0, 180, 0), Time.deltaTime * speed);
        }
	}
}
