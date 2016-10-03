using UnityEngine;
using System.Collections.Generic;

public class SlideBarManagement : MonoBehaviour {

    [SerializeField]
    private int min, max, scrore;  //---this value correspond to the element id in the list in the file save.save we star to 1 and not to 0
    public float x;
    public void SetSlideBar()
    {
        int score = 0;
        List<string> listS = SaveGame.ReadText(PlayerInformation.path);
        for (int i = (min - 1) + 5; i < (max + 5); i++)
            score += int.Parse(listS[i]);
        scrore = score;
        x = (float)score / (((float)max - ((float)min - 1f)) * 3f);
        transform.GetComponent<UnityEngine.UI.Slider>().value = x;
    }
          
}
