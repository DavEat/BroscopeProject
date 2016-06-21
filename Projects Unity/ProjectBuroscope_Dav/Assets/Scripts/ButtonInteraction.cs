using UnityEngine;

public class ButtonInteraction : MonoBehaviour {

    public static void ButtonChangeSize (Transform t, Vector2 size)
    {
        t.localScale = size;
    }

    public static void ButtonGrowing(Transform t, int scale)
    {
    }
}
