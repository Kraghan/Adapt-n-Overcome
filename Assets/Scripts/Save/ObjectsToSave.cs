using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "Objects to save", menuName = "Objects to save")]
public class ObjectsToSave : ScriptableObject {
    [SerializeField]
    LevelStats m_levelStats;
    [SerializeField]
    PlayerStats m_playerStats;

    public void CreateDatas()
    {
        m_playerStats.Save();
        Directory.CreateDirectory(SaveSlot.GetCurrentSlotPath() + "/Levels");
    }

    public void Save()
    {
        m_levelStats.Save();
        m_playerStats.Save();
    }

    public void Load()
    {

    }
}
