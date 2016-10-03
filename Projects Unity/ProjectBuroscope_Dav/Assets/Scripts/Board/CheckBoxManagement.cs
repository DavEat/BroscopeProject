using UnityEngine;
using System.Collections.Generic;

public class CheckBoxManagement : MonoBehaviour {

    public bool checkedd;

    public Sprite uncheckSprite, checkSprite;
    [SerializeField]
    private List<Transform> otherCheck; //---Other check boc in the same list

    public void Mangement()
    {
        if (!checkedd)
        {
            checkedd = true;
            transform.GetComponent<UnityEngine.UI.Image>().sprite = checkSprite;

            foreach (Transform t in otherCheck)
                if (t.GetComponent<CheckBoxManagement>().checkedd)
                {
                    t.GetComponent<CheckBoxManagement>().checkedd = false;
                    t.GetComponent<UnityEngine.UI.Image>().sprite = uncheckSprite;
                }
        }
    }
}
