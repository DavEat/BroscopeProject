using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class FindAllSaveFiles : MonoBehaviour {

    [SerializeField]
    private Dropdown drop;
    [SerializeField]
    private GameObject keyboard, textAllreadyExist, textUnvalidprofile, ChoseAvatarScreen, mainMenu, player, listAvatar;
    [SerializeField]
    private Transform spawn;

    public static FileInfo[] info;

    /*void Start ()
    {
        AssignFilesName();
    }*/
	
    public static FileInfo[] FindFiles()
    {
        DirectoryInfo dir = new DirectoryInfo(SaveGame.path);
        return dir.GetFiles("*.save");
    }

    public static FileInfo[] GetFilesInfo()
    {
        return info;
    }

    public void AssignFilesName()
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
            textAllreadyExist.GetComponent<Animator>().Play("FadeText");
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
        {
            gameObject.SetActive(false);

            PlayerInformation.name = keyboard.GetComponent<Keyboard>().GetTextDrop().text;
            PlayerInformation.path = PlayerInformation.name + ".save";

            PlayerInformation.avatarIndex = int.Parse(SaveGame.ReadText(PlayerInformation.path)[1]);

            GameObject obj = Instantiate(listAvatar.GetComponent<AvatarList>().GetAvatarList()[PlayerInformation.avatarIndex]) as GameObject;
            obj.transform.parent = player.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localEulerAngles = Vector3.zero;

            player.GetComponent<MovePerso>().canMove = true;
            Camera.main.transform.parent.GetComponent<NavMeshObstacle>().enabled = true;
            player.transform.position = spawn.position;
            //pointer.SetActive(true);
        }            
        else
        {
            Debug.Log("true");
            textUnvalidprofile.GetComponent<Animator>().Play("FadeText");
        }        
    }
}
