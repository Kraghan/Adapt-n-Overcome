using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    public static string s_currentSlot = "";

    [SerializeField]
    private uint m_slotNumber = 0;
    [SerializeField]
    private ObjectsToSave m_objectsToSave;

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

    public static string GetSavesPath()
    {
        return Application.persistentDataPath + "/Saves/";
    }

    public string GetSlotPath()
    {
        return GetSavesPath() + "Save " + m_slotNumber;
    }

    public static string GetCurrentSlotPath()
    {
        return GetSavesPath() + "Save " + s_currentSlot;
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

    public bool IsUsed()
    {
        if (!Directory.Exists(GetSavesPath()))
            Directory.CreateDirectory(GetSavesPath());

        if (!Directory.Exists(GetSlotPath()))
            Directory.CreateDirectory(GetSlotPath());

        DirectoryInfo info = new DirectoryInfo(GetSlotPath());
        FileInfo[] fileInfo = info.GetFiles();
        return fileInfo.Length != 0;
    }

    public void SetAsCurrentSlot()
    {
        s_currentSlot = m_slotNumber.ToString();
    }

    public void CreateCurrentSlot()
    {
        m_objectsToSave.CreateDatas();
    }
    
    public void LoadCurrentSlot()
    {
        m_objectsToSave.Load();
    }

    public void SaveCurrentSlot()
    {
        m_objectsToSave.Save();
    }
}
