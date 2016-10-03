using UnityEngine;

public class VRinfo : MonoBehaviour {

    public static bool firstPersonCamera, VRactive;
    public static float speed, speedCamera;

    [SerializeField]
    private bool firstPersonCameraU, VRactiveU;
    [SerializeField]
    private float speedU, speedCameraU;

    void Awake()
    {
        if (firstPersonCamera != firstPersonCameraU)
            firstPersonCamera = firstPersonCameraU;
        if (VRactive != VRactiveU)
            VRactive = VRactiveU;
        if (speed != speedU)
            speed = speedU;
        if (speedCamera != speedCameraU)
            speedCamera = speedCameraU;
    }

    #if (UNITY_EDITOR)
    void Update()
    {
        if (firstPersonCamera != firstPersonCameraU)
            firstPersonCamera = firstPersonCameraU;
        if (VRactive != VRactiveU)
            VRactive = VRactiveU;
        if (speed != speedU)
            speed = speedU;
        if (speedCamera != speedCameraU)
            speedCamera = speedCameraU;
    }
    #endif
}
