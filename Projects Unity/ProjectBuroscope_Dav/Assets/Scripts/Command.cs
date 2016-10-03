using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Command : MonoBehaviour {

    void Update ()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            File.Delete(SaveGame.path + PlayerInformation.path);
            SaveGame.CreateFiles(PlayerInformation.path);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
