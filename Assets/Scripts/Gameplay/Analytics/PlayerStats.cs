using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[CreateAssetMenu(fileName = "Player Datas", menuName = "Player Datas")]
public class PlayerStats : ScriptableObject
{
    public string m_playerName = "";

    public uint m_levelTried = 0;
    public uint m_levelCompleted = 0;
    public float m_totalTimePlayed = 0;

    public uint m_totalKills = 0;
    public uint m_totalBulletShooted = 0;
    public uint m_totalBulletEffective = 0;
    public uint m_totalCashEarned = 0;
    public uint m_totalCashSpent = 0;
    public uint m_maxCombo = 0;
    public uint m_totalNumberOfHit = 0;
    public uint m_totalNumberOfDeath = 0;

    public float m_maxTimeElapsedWithoutShooting = 0;
    public float m_totalTimeElapsedWithoutShooting = 0;

    public float m_totalTimeInTopLeft;
    public float m_totalTimeInMiddleLeft;
    public float m_totalTimeInBottomLeft;
    public float m_totalTimeInTopMiddle;
    public float m_totalTimeInMiddleMiddle;
    public float m_totalTimeInBottomMiddle;
    public float m_totalTimeInTopRight;
    public float m_totalTimeInMiddleRight;
    public float m_totalTimeInBottomRight;

    public float m_totalTimeNearEnemies;

    public LocalisationTrackerPosition m_preferedPosition;

    public void Save()
    {
        string path = SaveSlot.GetCurrentSlotPath() + "/";

        string datas = "";

        datas += SavePlayerInfo();
        datas += SaveGlobalStats();
        datas += SaveTracking();

        StreamWriter stream = new StreamWriter(path + "PlayerStats");

        stream.WriteLine(datas);
        stream.Close();

    }

    private string SavePlayerInfo()
    {
        string datas = "Player Name : " + m_playerName + "\n";
        datas += "Levels tried : " + m_levelTried + "\n";
        datas += "Levels completed : " + m_levelCompleted + "\n";
        datas += "Time played : " + m_totalTimePlayed + "\n";

        return datas;
    }

    private string SaveGlobalStats()
    {
        string datas = "Kills : " + m_totalKills + "\n";
        datas += "Shoot : " + m_totalBulletShooted + "\n";
        datas += "Efficient : " + m_totalBulletEffective + "\n";
        datas += "Cash : " + m_totalCashEarned + "\n";
        datas += "Cash spent : " + m_totalCashSpent + "\n";
        datas += "Combo : " + m_maxCombo + "\n";
        datas += "Time near enemies : " + m_totalTimeNearEnemies + "\n";

        datas += "Hits : " + m_totalNumberOfHit + "\n";
        datas += "Deaths : " + m_totalNumberOfDeath + "\n";

        return datas;
    }

    private string SaveTracking()
    {
        string datas = "TL : " + m_totalTimeInTopLeft + "\n";
        datas += "ML : " + m_totalTimeInMiddleLeft + "\n";
        datas += "BL : " + m_totalTimeInBottomLeft + "\n";
        datas += "TM : " + m_totalTimeInTopMiddle + "\n";
        datas += "MM : " + m_totalTimeInMiddleMiddle + "\n";
        datas += "BM : " + m_totalTimeInBottomMiddle + "\n";
        datas += "TR : " + m_totalTimeInTopRight + "\n";
        datas += "MR : " + m_totalTimeInMiddleRight + "\n";
        datas += "BR : " + m_totalTimeInBottomRight + "\n";

        return datas;
    }

    public static PlayerStats Load(uint levelID)
    {
        string path = SaveSlot.GetCurrentSlotPath() + "/";

        if (!File.Exists(path + "PlayerStats"))
            return null;

        PlayerStats stats = new PlayerStats();

        StreamReader stream = new StreamReader(path + "PlayerStats");

        stats.LoadPlayerInfo(stream);
        stats.LoadGlobalStats(stream);
        stats.LoadTracking(stream);

        return stats;
    }

    private void LoadPlayerInfo(StreamReader stream)
    {
        m_playerName = SaveSlot.Extract(stream.ReadLine());
        m_levelTried = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_levelCompleted = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalTimePlayed = float.Parse(SaveSlot.Extract(stream.ReadLine()));
    }

    private void LoadGlobalStats(StreamReader stream)
    {
        m_totalKills = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalBulletShooted = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalBulletEffective = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalCashEarned = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalCashSpent = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_maxCombo = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalTimeNearEnemies = float.Parse(SaveSlot.Extract(stream.ReadLine()));

        m_totalNumberOfHit = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalNumberOfDeath = uint.Parse(SaveSlot.Extract(stream.ReadLine()));
    }

    private void LoadTracking(StreamReader stream)
    {
        m_totalTimeInTopLeft = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalTimeInMiddleLeft = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalTimeInBottomLeft = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalTimeInTopMiddle = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalTimeInMiddleMiddle = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalTimeInBottomMiddle = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalTimeInTopRight = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalTimeInMiddleRight = float.Parse(SaveSlot.Extract(stream.ReadLine()));
        m_totalTimeInBottomRight = float.Parse(SaveSlot.Extract(stream.ReadLine()));
    }

    public void SetPlayerName(InputField field)
    {
        m_playerName = field.text.Trim();
    }

    public float GetAverageTimeBetweenBullets()
    {
        return m_totalTimeElapsedWithoutShooting / (float)(m_totalBulletShooted + 1f);
    }
}
