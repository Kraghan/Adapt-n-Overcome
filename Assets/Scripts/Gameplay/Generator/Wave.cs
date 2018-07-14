using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveType
{
    BURST,
    FLOW
}

public class Wave : MonoBehaviour
{

    float m_timeBeforeLaunch = 0;
    float m_duration = 0;

    GameObject m_enemy;

    WaveType m_type;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetEnemy(GameObject enemy)
    {
        m_enemy = enemy;
    }

    public void SetTimeBeforeLaunch(float time)
    {
        m_timeBeforeLaunch = time;
    }

    public void SetDuration(float duration)
    {
        m_duration = duration;
    }

    public float GetDuration()
    {
        return m_duration;
    }

    public float GetTimeBeforeLaunch()
    {
        return m_timeBeforeLaunch;
    }
}
