using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private GameObject playScreen, creditScreen;

    public void MainMenuForCredit()
    {
        creditScreen.SetActive(false);
        gameObject.SetActive(true);
    }

    public void Play()
    {
        gameObject.SetActive(false);
        playScreen.SetActive(true);
        FindAllSaveFiles.info = FindAllSaveFiles.FindFiles();
        playScreen.GetComponent<FindAllSaveFiles>().AssignFilesName();        
    }

    public void Credit()
    {
        gameObject.SetActive(false);
        creditScreen.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
