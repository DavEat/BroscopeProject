using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public Transform player;
    public Vector3 camPos;

	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x + camPos.x, player.position.y + camPos.y, player.position.z + camPos.z), 1);
	}
}
