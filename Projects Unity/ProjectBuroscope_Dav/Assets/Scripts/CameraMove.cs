using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public Transform player;
    public Vector3 camPos, camAngle;

    void Awake ()
    {
        camAngle = transform.GetChild(0).eulerAngles;
        transform.GetChild(0).localEulerAngles = player.eulerAngles;
    }

	void Update ()
    {
        if (VRinfo.firstPersonCamera)
        {
            transform.GetChild(0).eulerAngles = Vector3.Lerp(transform.GetChild(0).eulerAngles, camAngle, Time.deltaTime * 5f);
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x + camPos.x, player.position.y + camPos.y, player.position.z + camPos.z), Time.deltaTime * 1f);
        }
        else
        {
            transform.position = player.GetChild(0).position;
            if (Mathf.Abs(Input.GetAxis("HorizontalHead")) > 0.2f || Mathf.Abs(Input.GetAxis("VerticalHead")) > 0.2f)
            {
                transform.GetChild(0).Rotate(new Vector3(Input.GetAxis("VerticalHead"), Input.GetAxis("HorizontalHead"), 0).normalized * VRinfo.speedCamera);
                transform.GetChild(0).localEulerAngles = new Vector3(transform.GetChild(0).localEulerAngles.x, transform.GetChild(0).localEulerAngles.y, 0);
                player.eulerAngles = new Vector3(0, transform.GetChild(0).localEulerAngles.y, 0);
            }
        }
	}
}
