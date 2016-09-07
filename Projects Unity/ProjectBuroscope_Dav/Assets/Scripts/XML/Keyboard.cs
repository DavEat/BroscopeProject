using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    [SerializeField]
    private GameObject validationButton;
    [SerializeField]
    private Text text;
    private bool shift = true, firstclick = true;

    public void Shifting()
    {
        if (shift)
            for (int i = 0; i < transform.childCount - 3; i++)
                transform.GetChild(i).GetChild(0).GetComponent<Text>().text = transform.GetChild(i).GetChild(0).GetComponent<Text>().text.ToLower();
        else
            for (int i = 0; i < transform.childCount - 3; i++)
                transform.GetChild(i).GetChild(0).GetComponent<Text>().text = transform.GetChild(i).GetChild(0).GetComponent<Text>().text.ToUpper();
        shift = !shift;
    }

    public void Eraise()
    {
        string s = "";
        for (int i = 0; i < text.text.Length - 1; i++)
            s += text.text[i];
        text.text = s;
    }

    public void WriteLetter(char letter)
    {
        if (firstclick)
        {
            text.text = "";
            firstclick = false;
            validationButton.SetActive(true);
        }
        if (shift)
            text.text += letter;
        else text.text += letter.ToString().ToLower();
    }

    public bool GetFirstClick()
    {
        return this.firstclick;
    }

    public void SetFirstClick(bool value)
    {
        this.firstclick = value;
    }

    public GameObject GetValidatioButton()
    {
        return this.validationButton;
    }

    public Text GetTextDrop()
    {
        return this.text;
    }
}
