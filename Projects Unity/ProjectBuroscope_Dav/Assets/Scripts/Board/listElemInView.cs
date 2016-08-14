using UnityEngine;
using System.Collections.Generic;

public class listElemInView : MonoBehaviour {

    public List<Transform> listButton, listTextButton, listTextToChange;
    public Transform objectif;

    public void Instentiate()
    {
        foreach (Transform t in listButton)
        {
            t.GetComponent<LogoStat>().initialSize = t.localScale;
            t.GetComponent<LogoStat>().maxSize = t.localScale + t.localScale / 10;
        }

        if (listTextButton.Count > 0)
        {
            if (listTextButton[0].GetComponent<TextMesh>() != null)
            {
                foreach (Transform t in listTextButton)
                {
                    t.GetComponent<LogoStat>().initialSize = t.localScale;
                    t.GetComponent<LogoStat>().maxSize = t.localScale + t.localScale / 10;
                }
            }
            else if (listTextButton[0].GetComponent<UnityEngine.UI.Text>() != null)
            {
                foreach (RectTransform t in listTextButton)
                {
                    if (t.GetComponent<LogoStat>() != null)
                    {
                        t.GetComponent<LogoStat>().initialSize = t.localScale;
                        t.GetComponent<LogoStat>().maxSize = t.localScale + t.localScale / 10;
                    }
                }
            }
        }
    }
}
