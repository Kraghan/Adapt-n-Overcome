using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSlot : MonoBehaviour {

    public static string s_currentSlot = "";

    [SerializeField]
    private uint m_slotNumber = 0;

	public void Load()
    {
        s_currentSlot = m_slotNumber.ToString();

    }

    public void Erase()
    {
        // Delete root files
        DirectoryInfo info = new DirectoryInfo(GetSlotPath());
        FileInfo[] fileInfo = info.GetFiles();

        foreach (FileInfo file in fileInfo)
        {
            file.Delete();
        }

        // Delete levels datas
        info = new DirectoryInfo(GetSlotPath()+"/Levels");
        fileInfo = info.GetFiles();

        foreach (FileInfo file in fileInfo)
        {
            file.Delete();
        }
    }

    public string GetSlotPath()
    {
        return Application.persistentDataPath + "/Save " + m_slotNumber;
    }

    public static string GetCurrentSlotPath()
    {
        return Application.persistentDataPath + "/Save " + s_currentSlot;
    }

    public static string Extract(string baseString)
    {
        bool foundColumn = false;
        bool startExtract = false;
        string extracted = "";
        foreach(char letter in baseString)
        {
            if(startExtract)
            {
                extracted += letter;
                continue;
            }

            if(foundColumn && !startExtract)
            {
                startExtract = true;
                continue;
            }

            if (letter == ':')
            {
                foundColumn = true;
                continue;
            }
        }

        return extracted;
    }
}
