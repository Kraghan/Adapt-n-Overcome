    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName="Level Datas", menuName = "Level Datas")]
public class LevelStats : ScriptableObject
{
    // Level global infos
    public uint m_levelID = 0;
    public Factions m_faction = Factions.NEUTRAL;

    // Level global stats
    public uint m_kills;
    public uint m_bulletShooted;
    public uint m_bulletEffective;
    public uint m_cashEarned;
    public uint m_combo;
    public float m_progression;
    public uint m_numberOfHit;
    public uint m_numberOfDeath;

    public float m_maxTimeElapsedWithoutShooting;
    public float m_totalTimeElapsedWithoutShooting;

    // Level tracking info
    public float m_timeInTopLeft;
    public float m_timeInMiddleLeft;
    public float m_timeInBottomLeft;
    public float m_timeInTopMiddle;
    public float m_timeInMiddleMiddle;
    public float m_timeInBottomMiddle;
    public float m_timeInTopRight;
    public float m_timeInMiddleRight;
    public float m_timeInBottomRight;

    public float m_timeNearEnemies;

    public void Reset()
    {
        m_kills = 0;
        m_bulletShooted = 0;
        m_bulletEffective = 0;
        m_cashEarned = 0;
        m_combo = 0;
        m_progression = 0;
        m_numberOfDeath = 0;
        m_numberOfHit = 0;

        m_maxTimeElapsedWithoutShooting = 0;
        m_totalTimeElapsedWithoutShooting = 0;

        m_timeInTopLeft = 0;
        m_timeInMiddleLeft = 0;
        m_timeInBottomLeft = 0;
        m_timeInTopMiddle = 0;
        m_timeInMiddleMiddle = 0;
        m_timeInBottomMiddle = 0;
        m_timeInTopRight = 0;
        m_timeInMiddleRight = 0;
        m_timeInBottomRight= 0;

        m_timeNearEnemies = 0;
    }

    public void Save()
    {
        string path = SaveSlot.GetCurrentSlotPath() + "/Levels/";

        string datas = "";

        datas += SaveLevelInfo();
        datas += SaveLevelGlobalStats();
        datas += SaveLevelTracking();

        StreamWriter stream = new StreamWriter(path + "Level " + m_levelID);

        stream.WriteLine(datas);
        stream.Close();
        
    }

    private string SaveLevelInfo()
    {
        string datas = "ID : " + m_levelID + "\n";
        datas += "Factions : " + FactionHelper.FactionsToString(m_faction) + "\n";

        return datas;
    }

    private string SaveLevelGlobalStats()
    {
        string datas = "Kills : " + m_kills + "\n";
        datas += "Shoot : " + m_bulletShooted + "\n";
        datas += "Efficient : " + m_bulletEffective + "\n";
        datas += "Cash : " + m_cashEarned + "\n";
        datas += "Combo : " + m_combo + "\n";
        datas += "Progression : " + m_progression + "\n";
        datas += "Hits : " + m_numberOfHit + "\n";
        datas += "Death : " + m_numberOfDeath + "\n";

        datas += "Time without shoot : " + m_maxTimeElapsedWithoutShooting;
        datas += "Total time without shoot : " + m_totalTimeElapsedWithoutShooting;

        return datas;
    }

    private string SaveLevelTracking()
    {
        string datas = "TL : " + m_timeInTopLeft + "\n";
        datas += "ML : " + m_timeInMiddleLeft + "\n";
        datas += "BL : " + m_timeInBottomLeft + "\n";
        datas += "TM : " + m_timeInTopMiddle + "\n";
        datas += "MM : " + m_timeInMiddleMiddle + "\n";
        datas += "BM : " + m_timeInBottomMiddle + "\n";
        datas += "TR : " + m_timeInTopRight + "\n";
        datas += "MR : " + m_timeInMiddleRight + "\n";
        datas += "BR : " + m_timeInBottomRight + "\n";

        datas += "Time near enemies : " + m_timeNearEnemies + "\n";

        return datas;
    }
    
    public static LevelStats Load(uint levelID)
    {
        string path = SaveSlot.GetCurrentSlotPath() + "/Levels/";

        if (!File.Exists(path + "Level " + levelID))
            return null;

        LevelStats stats = new LevelStats();
        stats.Reset();

        StreamReader stream = new StreamReader(path + "Level " + levelID);

        stats.LoadLevelInfo(stream);
        stats.LoadLevelGlobalStats(stream);
        stats.LoadLevelTracking(stream);

        return stats;
    }

    private void LoadLevelInfo(StreamReader stream)
    {
        m_levelID = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_faction = FactionHelper.StringToFactions(SaveSlot.Extract(stream.ReadLine()));
    }

    private void LoadLevelGlobalStats(StreamReader stream)
    {
        m_kills = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_bulletShooted = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_bulletEffective = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_cashEarned = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_combo = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_progression = float.Parse(SaveSlot.Extract(stream.ReadLine()));

        m_numberOfHit = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_numberOfDeath = uint.Parse(SaveSlot.Extract(stream.ReadLine()));

        m_maxTimeElapsedWithoutShooting = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalTimeElapsedWithoutShooting = float.Parse(SaveSlot.Extract(stream.ReadLine()));

    }

    private void LoadLevelTracking(StreamReader stream)
    {
        m_timeInTopLeft = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_timeInMiddleLeft = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_timeInBottomLeft = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_timeInTopMiddle = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_timeInMiddleMiddle = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_timeInBottomMiddle = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_timeInTopRight = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_timeInMiddleRight = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_timeInBottomRight = float.Parse(SaveSlot.Extract(stream.ReadLine()));

        m_timeNearEnemies = float.Parse(SaveSlot.Extract(stream.ReadLine()));
    }
}
