using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour {

    [SerializeField]
    uint m_numberOfPlanetMin = 3;
    [SerializeField]
    uint m_numberOfPlanetMax = 9;

    uint m_numberOfPlanets;

    List<Planet> m_planets = new List<Planet>();

    [SerializeField]
    float m_sunSizeMin = 0.5f;
    [SerializeField]
    float m_sunSizeMax = 3;
    float m_sunSize = 1;

    [SerializeField]
    float m_sunTemperatureMin = 3000f;
    [SerializeField]
    float m_sunTemperatureMax = 50000f;
    float m_sunTemperature;

    Color m_sunColor;

    Sector m_sectorAttached;

    Animator m_animator;

    bool m_inside = false;

    [SerializeField]
    SpriteRenderer m_sunRenderer;
    [SerializeField]
    SpriteRenderer m_auraRenderer;

    // Use this for initialization
    void Start ()
    {
        m_numberOfPlanets = (uint)Random.Range(m_numberOfPlanetMin, m_numberOfPlanetMax);

        m_sunSize = Random.Range(m_sunSizeMin, m_sunSizeMax);
        m_sunTemperature = Random.Range(m_sunTemperatureMin, m_sunTemperatureMax);
        m_sunColor = Mathf.CorrelatedColorTemperatureToRGB(m_sunTemperature);

        m_animator = GetComponentInChildren<Animator>();

        m_sunRenderer.color = m_sunColor;

        m_auraRenderer.color = FactionHelper.GetFactionColor(GetMainFaction());
	}

    public Factions GetMainFaction()
    {
        uint neutral = 0;

        foreach (Planet planet in m_planets)
        {
            switch (planet.GetOwner())
            {
                case Factions.NEUTRAL:
                    neutral++;
                    break;
            }
        }

        return Factions.NEUTRAL;
    }

    public void SetSector(Sector sector)
    {
        m_sectorAttached = sector;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_animator.SetBool("inside", true);
            m_inside = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_animator.SetBool("inside", false);
            m_inside = true;
        }
    }
}
