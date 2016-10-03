using UnityEngine;

public class PointPos : MonoBehaviour {

    [SerializeField]
    private LayerMask layer;

    public Vector3 pos;

    public float x, ix, y, iy;

    public static RaycastHit hit;

    void Update ()
    {

        ix = Input.GetAxis("Mouse X");
        iy = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(x) < Screen.width || (Mathf.Abs(x + ix) < Mathf.Abs(x)))
            x += ix;
        if (Mathf.Abs(y) < Screen.width || (Mathf.Abs(y + iy) < Mathf.Abs(y)))
            y += iy;

        pos = new Vector3(x, y, 10);

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.rotation * (Vector3.forward + pos));
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Cursor.visible = false;
        //RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            DrawPoint(hit);            
        }
           

    }

    private void DrawPoint(RaycastHit hit)
    {
        transform.position = hit.point;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
    }
}
