using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    [SerializeField]
    Factions m_owner;

    SolarSystem m_solarSystemAttached;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Factions GetOwner()
    {
        return m_owner;
    }
}
