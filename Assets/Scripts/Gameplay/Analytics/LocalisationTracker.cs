using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocalisationTracker : MonoBehaviour
{
    [SerializeField]
    LocalisationTrackerPosition m_position;

    StatsManager m_manager;

	// Use this for initialization
	void Start ()
    {
        m_manager = GameObject.FindGameObjectWithTag("StatsManager").GetComponent<StatsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_manager.AddPositionTime(m_position);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        m_manager.AddPositionTime(m_position);
    }
}
