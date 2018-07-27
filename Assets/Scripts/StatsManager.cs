using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LocalisationTrackerPosition
{
    TOP_LEFT,
    TOP_MIDDLE,
    TOP_RIGHT,
    MIDDLE_LEFT,
    MIDDLE_MIDDLE,
    MIDDLE_RIGHT,
    BOTTOM_LEFT,
    BOTTOM_MIDDLE,
    BOTTOM_RIGHT
}

public class StatsManager : MonoBehaviour
{
    [SerializeField]
    LevelStats m_levelStats;
    [SerializeField]
    Text m_killText;
    [SerializeField]
    Text m_accuracyText;
    [SerializeField]
    Text m_cashText;
    [SerializeField]
    Text m_comboText;
    [SerializeField]
    Slider m_progressionGauge;

    private bool m_timeNearEnemyAlreadyUpdated = false;
    private float m_timeElapsedSinceLastShoot = 0;
    
    // Use this for initialization
    void Awake ()
    {
        m_levelStats.Reset();
    }

    private void FixedUpdate()
    {
        m_timeNearEnemyAlreadyUpdated = false;

        if(!Input.GetButton("Fire"))
        {
            m_timeElapsedSinceLastShoot += Time.fixedDeltaTime;
            m_levelStats.m_totalTimeElapsedWithoutShooting += Time.fixedDeltaTime;
        }
        else
        {   
            if(m_levelStats.m_maxTimeElapsedWithoutShooting < m_timeElapsedSinceLastShoot)
            {
                m_levelStats.m_maxTimeElapsedWithoutShooting = m_timeElapsedSinceLastShoot;
            }
        }
    }

    public void ForceReset()
    {
        m_levelStats.Reset();
    }

    public void AddKill()
    {
        m_levelStats.m_kills++;

        string str = m_levelStats.m_kills.ToString();
        if (str.Length < 5)
        {
            str = CompleteWithZero(str,5);
            m_killText.text = str;
        }
    }

    public void AddShoot()
    {
        m_levelStats.m_bulletShooted++;

        UpdateAccuracy();
    }

    public void AddTouched()
    {
        m_levelStats.m_bulletEffective++;
        UpdateAccuracy();
    }
	
    public void AddCash(uint cash)
    {
        m_levelStats.m_cashEarned+=cash;

        string str = m_levelStats.m_cashEarned.ToString();
        if (str.Length < 5)
        {
            str = CompleteWithZero(str,5);
            m_cashText.text = str + " $";
        }
    }

    public void SetCombo(uint combo)
    {
        m_levelStats.m_combo = combo;

        string str = m_levelStats.m_kills.ToString();
        if (str.Length < 3)
        {
            str = CompleteWithZero(str,3);
            m_comboText.text = str;
        }
    }

    public void SetProgression(float progression)
    {
        m_levelStats.m_progression = progression;

        m_progressionGauge.value = progression;
    }
    
    private void UpdateAccuracy()
    {
        float accuracy = (float)m_levelStats.m_bulletEffective / (float)m_levelStats.m_bulletShooted;

        int tmp = (int)(accuracy * 10000);
        accuracy = tmp / 100.0f;
        string str = accuracy.ToString() + " %";
        m_accuracyText.text = str;
    }

    private string CompleteWithZero(string str, uint numberOfNumber)
    {
        while(str.Length < numberOfNumber)
        {
            str = "0" + str;
        }

        return str;

    }

    public uint GetKills()
    {
        return m_levelStats.m_kills;
    }

    public void AddPositionTime(LocalisationTrackerPosition position)
    {
        switch(position)
        {
            case LocalisationTrackerPosition.BOTTOM_LEFT:
                m_levelStats.m_timeInBottomLeft += Time.fixedDeltaTime;
                break;

            case LocalisationTrackerPosition.BOTTOM_MIDDLE:
                m_levelStats.m_timeInBottomMiddle += Time.fixedDeltaTime;
                break;

            case LocalisationTrackerPosition.BOTTOM_RIGHT:
                m_levelStats.m_timeInBottomRight += Time.fixedDeltaTime;
                break;

            case LocalisationTrackerPosition.MIDDLE_LEFT:
                m_levelStats.m_timeInMiddleLeft += Time.fixedDeltaTime;
                break;

            case LocalisationTrackerPosition.MIDDLE_MIDDLE:
                m_levelStats.m_timeInMiddleMiddle += Time.fixedDeltaTime;
                break;

            case LocalisationTrackerPosition.MIDDLE_RIGHT:
                m_levelStats.m_timeInMiddleRight += Time.fixedDeltaTime;
                break;

            case LocalisationTrackerPosition.TOP_LEFT:
                m_levelStats.m_timeInTopLeft += Time.fixedDeltaTime;
                break;

            case LocalisationTrackerPosition.TOP_MIDDLE:
                m_levelStats.m_timeInTopMiddle += Time.fixedDeltaTime;
                break;

            case LocalisationTrackerPosition.TOP_RIGHT:
                m_levelStats.m_timeInTopRight += Time.fixedDeltaTime;
                break;
        }
    }

    public void AddTimeNearEnemy()
    {
        if(!m_timeNearEnemyAlreadyUpdated)
        {
            m_levelStats.m_timeNearEnemies += Time.fixedDeltaTime;
            m_timeNearEnemyAlreadyUpdated = true;
        }
    }
}
