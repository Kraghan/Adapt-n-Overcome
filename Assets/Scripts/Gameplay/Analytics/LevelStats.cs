using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Level Datas", menuName = "Datas")]
public class LevelStats : ScriptableObject
{
    public uint m_kills;
    public uint m_bulletShooted;
    public uint m_bulletEffective;
    public uint m_cashEarned;
    public uint m_combo;

    public float m_progression;

    public float m_timeInTopLeft;
    public float m_timeInMiddleLeft;
    public float m_timeInBottomLeft;
    public float m_timeInTopMiddle;
    public float m_timeInMiddleMiddle;
    public float m_timeInBottomMiddle;
    public float m_timeInTopRight;
    public float m_timeInMiddleRight;
    public float m_timeInBottomRight;

    public void Reset()
    {
        m_kills = 0;
        m_bulletShooted = 0;
        m_bulletEffective = 0;
        m_cashEarned = 0;
        m_combo = 0;
        m_progression = 0;

        m_timeInTopLeft = 0;
        m_timeInMiddleLeft = 0;
        m_timeInBottomLeft = 0;
        m_timeInTopMiddle = 0;
        m_timeInMiddleMiddle = 0;
        m_timeInBottomMiddle = 0;
        m_timeInTopRight = 0;
        m_timeInMiddleRight = 0;
        m_timeInBottomRight= 0;
    }
}
