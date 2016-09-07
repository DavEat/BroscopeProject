using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class FindAllSaveFiles : MonoBehaviour {

    [SerializeField]
    private Dropdown drop;
    [SerializeField]
    private GameObject keyboard, textAllreadyExist, textUnvalidprofile, ChoseAvatarScreen, mainMenu, player;

    private static FileInfo[] info = FindFiles();

    void Awake ()
    {
        AssignFilesName();
    }
	
    public static FileInfo[] FindFiles()
    {
        DirectoryInfo dir = new DirectoryInfo(@"Files");
        return dir.GetFiles("*.save");
    }

    public static FileInfo[] GetFilesInfo()
    {
        return info;
    }

    private void AssignFilesName()
    {
        List<Dropdown.OptionData> listDropOption = new List<Dropdown.OptionData>();
        foreach (FileInfo f in info)
        {
            string s = "" + f.Name[0];

            for (int i = 1; i < f.Name.Length - 5; i++)
                s += f.Name[i];
            //Debug.Log(" files : " + s);
            listDropOption.Add(new Dropdown.OptionData(s));
        }
        drop.AddOptions(listDropOption);
    }

    public void OnChangeValue()
    {
        if (drop.value == 1)
        {
            keyboard.SetActive(true);
            keyboard.GetComponent<Keyboard>().GetTextDrop().text = "Entrez le nom du profile";
            if (!keyboard.GetComponent<Keyboard>().GetFirstClick())
                keyboard.GetComponent<Keyboard>().SetFirstClick(true);
        }
        else if (keyboard.activeSelf)
        {
            if (keyboard.GetComponent<Keyboard>().GetValidatioButton().activeSelf)
                keyboard.GetComponent<Keyboard>().GetValidatioButton().SetActive(false);
            keyboard.SetActive(false);
        }
    }

    public void Validation()
    {
        string name = keyboard.GetComponent<Keyboard>().GetTextDrop().text;

        if (SaveGame.CheckExitsFiles(name + ".save"))
        {
            if (!textAllreadyExist.activeSelf)
                textAllreadyExist.SetActive(true);
            else textAllreadyExist.GetComponent<Animator>().Play("TextFade");
        }
        else
        {
            PlayerInformation.waitingName = name;
            ChoseAvatarScreen.SetActive(true);
        }
    }

    public void Finalisation(int avatarIndex)
    {
        PlayerInformation.name = PlayerInformation.waitingName;
        PlayerInformation.path = PlayerInformation.waitingName + ".save";
        PlayerInformation.avatarIndex = avatarIndex;

        SaveGame.CreateFiles(PlayerInformation.path);

        drop.options.Add(new Dropdown.OptionData(PlayerInformation.name));
        drop.value = drop.options.Count - 1;
        keyboard.SetActive(false);
        keyboard.GetComponent<Keyboard>().GetValidatioButton().SetActive(false);
    }

    public void ReturnMainMenu()
    {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ReturnChoseProfile()
    {
        ChoseAvatarScreen.SetActive(false);
        gameObject.SetActive(true);
    }

    public void Play()
    {
        if (drop.value > 1)
            gameObject.SetActive(false);
        else if (textUnvalidprofile.activeSelf)
            textUnvalidprofile.GetComponent<Animator>().Play("TextFade");
        else
            textUnvalidprofile.SetActive(true);

        PlayerInformation.name = keyboard.GetComponent<Keyboard>().GetTextDrop().text;
        PlayerInformation.path = PlayerInformation.name + ".save";

        player.GetComponent<MovePerso>().canMove = true;
    }
}
