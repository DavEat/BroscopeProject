using UnityEngine;
using System.Collections.Generic;

public class BlackFadeGesture : MonoBehaviour {

    [SerializeField]
    private List<GameObject> listScreen;
    private int i = 0;

    public void NextScreen()
    {
        if (i > 0)
            listScreen[i - 1].SetActive(false);
        if (i < listScreen.Count)
            listScreen[i].SetActive(true);
    }

    public void fadeIn()
    {
        if (i >= listScreen.Count - 1)
            gameObject.SetActive(false);
        else
        {
            GetComponent<Animator>().Play("FadeIn");
            i++;
        }
    }

    public void fadeOut()
    {
        GetComponent<Animator>().Play("BlackFade");
    }
}
