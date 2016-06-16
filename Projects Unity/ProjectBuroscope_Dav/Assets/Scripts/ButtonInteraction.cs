using UnityEngine;

public class ButtonInteraction : MonoBehaviour {

    public static void ButtonEnter (Transform t)
    {
        t.localScale += t.localScale / 10;
    }

    public static void ButtonExit(Transform t)
    {
        t.localScale -= t.localScale / 11;
    }

    public static void ButtonGrowing(Transform t, int scale)
    {
    }
}
