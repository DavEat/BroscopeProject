using UnityEngine;
using System.Collections.Generic;

public class listElemInView : MonoBehaviour {

    public List<Transform> listButton, listTextButton;

    public void Instentiate()
    {
        foreach (Transform t in listButton)
        {
            t.GetComponent<LogoStat>().initialSize = t.localScale;
            t.GetComponent<LogoStat>().maxSize = t.localScale + t.localScale / 10;
        }

        foreach (Transform t in listTextButton)
        {            
            t.GetComponent<LogoStat>().initialSize = t.localScale;
            t.GetComponent<LogoStat>().maxSize = t.localScale + t.localScale / 10;
        }
    }
}
