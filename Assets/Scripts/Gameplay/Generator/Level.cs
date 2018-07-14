using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    List<Wave> m_waves = new List<Wave>();

    float m_time = 0;
    float m_timeElapsed = 0;

    Wave m_currentWave = null;

	// Update is called once per frame
	void FixedUpdate ()
    {
        m_timeElapsed += Time.fixedDeltaTime;
        
        if(m_timeElapsed >= m_time)
        {
            EndLevel();
        }

        if(m_waves.Count != 0 && m_timeElapsed >= m_waves[0].GetTimeBeforeLaunch())
        {
            m_currentWave = m_waves[0];
            m_waves.RemoveAt(0);
        }
	}

    public void AddWave(Wave wave)
    {
        wave.SetTimeBeforeLaunch(m_time);
        m_time += wave.GetDuration();

        m_waves.Add(wave);
    }

    private void EndLevel()
    {

    }
}
