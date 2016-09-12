using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public class SaveGame : MonoBehaviour {


    #if (UNITY_EDITOR)
        public static string path = @"Files/";
    #elif (UNITY_STANDALONE_WIN)
        public static string path = @"Project Buroscope a 1.7_Data/";
    #endif
    private const int number = 5; //The number of specifics element before the score
    private const int numberElementSave = 15; //The number of save section


    public static List<string> ReadText(string _path)
    {
        if (!CheckExitsFiles(_path))
            CreateFiles(_path);
        StreamReader reader = new StreamReader(path + _path);

        string line = reader.ReadLine();
        List<string> listS = new List<string>();

        while (line != null)
        {
            listS.Add(line);
            line = reader.ReadLine();
        }
        reader.Close();
        return listS;
    }

    public static void WriteInText(int value, int line, string _path)
    {
        List<string> listS = ReadText(_path);
        StreamWriter writer = new StreamWriter(path + _path);
        for (int i = 0; i < number; i++)
            writer.WriteLine(listS[i]);
        for (int i = number; i < numberElementSave + number; i++)
        {
            if (i == line + number && int.Parse(listS[i]) < value)
                writer.WriteLine(value.ToString());
            else if (listS[i] != null)
                writer.WriteLine(listS[i]);
            else writer.WriteLine("0");
        }


        
        writer.Close();
    }

    public static void CreateFiles(string _path)
    {
        using (StreamWriter sw = File.CreateText(path + _path))
        {
            sw.WriteLine(PlayerInformation.name);
            sw.WriteLine(PlayerInformation.avatarIndex);
            for (int i = 0; i < number - 2; i++)
                sw.WriteLine("---");
            for (int i = 0; i < numberElementSave; i++)
                sw.WriteLine("0");
            sw.Close();
        }
    }

    public static bool CheckExitsFiles(string _path)
    {
        return File.Exists(path + _path);
    }
}
