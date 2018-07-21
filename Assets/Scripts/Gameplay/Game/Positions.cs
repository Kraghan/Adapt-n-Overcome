using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Positions
{
    [SerializeField]
    private Vector3 m_position;
    [SerializeField]
    private Timer m_timeToStop;

    public Positions(Vector3 position, float timeToWait)
    {
        m_position = position;
        m_timeToStop = new Timer(timeToWait);
    }

    public Vector3 GetPosition()
    {
        return m_position;
    }

    public bool Complete()
    {
        return m_timeToStop.IsTimedOut();
    }

    public void UpdateTimer()
    {
        m_timeToStop.FixedUpdateTimer();
    }
}
