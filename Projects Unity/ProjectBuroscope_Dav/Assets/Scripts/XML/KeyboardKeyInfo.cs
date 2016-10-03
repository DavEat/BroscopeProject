using UnityEngine;

public class KeyboardKeyInfo : MonoBehaviour {

    [SerializeField]
    private char key;

    public void Writte()
    {
        transform.GetComponentInParent<Keyboard>().WriteLetter(key);
    }
}
