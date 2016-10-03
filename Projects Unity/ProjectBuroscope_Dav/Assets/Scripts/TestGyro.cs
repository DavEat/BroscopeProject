using UnityEngine;
using UnityEngine.UI;

public class TestGyro : MonoBehaviour {

    public Vector3 vec;

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
        vec = Input.gyro.rotationRate;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.gyro.enabled);

        vec = Input.gyro.rotationRate;
        transform.GetComponent<Text>().text = "x : " + vec.x + " y : " + vec.y + " z : " + vec.z;
    }
}
