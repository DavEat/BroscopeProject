using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class XmlReader : MonoBehaviour
{
    public TextAsset dictionary;

    public string languageName, nodeName;
    public int currentCours, currentSection, currentElementGroupe, currentElement;

    public List<List<List<string>>> cours = new List<List<List<string>>>();
    public List<List<List<List<string>>>> programmeQCM = new List<List<List<List<string>>>>();
    private List<List<List<string>>> sectionListProgram;
    private List<List<string>> sectionList, sectionListProg;
    List<string> obj, objProg;

    void Awake()
    {
        Reader();
    }

    /*void Update() //---For testing---
    {
        if (currentElement != -1)
            languageName = programmeQCM[currentCours][currentSection][currentElementGroupe][currentElement];
        else
            languageName = cours[currentCours][currentSection][currentElement];
    }*/

    public string getXmlValueCours(int currentCours, int currentSection, int currentElement)
    {
        return cours[currentCours][currentSection][currentElement];
    }

    public string getXmlValueProg(int currentCours, int currentSection, int currentElementGroupe, int currentElement)
    {
        return programmeQCM[currentCours][currentSection][currentElementGroupe][currentElement];
    }

    void Reader()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dictionary.text);
        XmlNodeList languagesList = xmlDoc.GetElementsByTagName("cours");
        foreach (XmlNode languageValue in languagesList)
        {
            XmlNodeList languageContent = languageValue.ChildNodes;
            sectionList = new List<List<string>>();
            sectionListProgram = new List<List<List<string>>>();

            foreach (XmlNode sectionContent in languageContent)
            {
                XmlNodeList dialogueContent = sectionContent.ChildNodes;                

                if (sectionContent.Name == "programme" || sectionContent.Name == "qcm")
                {
                    sectionListProg = new List<List<string>>();
                    foreach (XmlNode value in dialogueContent)
                    {
                        XmlNodeList valuu = value.ChildNodes;

                        objProg = new List<string>();
                        foreach (XmlNode val in valuu)
                        {
                            objProg.Add(val.InnerText);
                        }
                        sectionListProg.Add(objProg);
                    }
                    sectionListProgram.Add(sectionListProg);
                }
                else
                {
                    obj = new List<string>();
                    foreach (XmlNode value in dialogueContent)
                    {
                        obj.Add(value.InnerText);
                    }
                    sectionList.Add(obj);
                }
            }
            cours.Add(sectionList);
            programmeQCM.Add(sectionListProgram);
        }
    }
}
