using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public Transform player;
    public Vector3 camPos, camAngle;

    void Awake ()
    {
        camAngle = transform.eulerAngles;
    }

	void Update ()
    {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, camAngle, Time.deltaTime * 5f);
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x + camPos.x, player.position.y + camPos.y, player.position.z + camPos.z), Time.deltaTime * 1f);
	}
}
