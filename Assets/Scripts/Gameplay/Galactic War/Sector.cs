using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    [SerializeField]
    uint m_numberOfSolarSystemMin = 3;
    [SerializeField]
    uint m_numberOfSolarSystemMax = 15;

    uint m_numberOfSolarSystem;

    List<SolarSystem> m_systems = new List<SolarSystem>();

    Galaxy m_galaxyAttached;

    List<Sector> m_neighbours = new List<Sector>();

    SpriteRenderer m_renderer;

    Animator m_animator;

    bool m_inside = false;

    float m_distanceBetweenSystem = 1.5f;

    Transform m_systemContainer;

    Collider2D m_collider;

    // Use this for initialization
    void Start ()
    {
        m_numberOfSolarSystem = (uint)Random.Range(m_numberOfSolarSystemMin, m_numberOfSolarSystemMax);
        m_renderer = GetComponentInChildren<SpriteRenderer>();
        m_renderer.color = FactionHelper.GetFactionColor(GetMainFaction());
        transform.localScale = new Vector3(0.1f, 0.1f, 1);

        m_animator = GetComponentInChildren<Animator>();
        m_collider = GetComponent<Collider2D>();
        
        CreateSystems();
        m_systemContainer.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(m_inside && Input.GetMouseButton(0))
        {
            DisplaySolarSystems();
        }
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

        Gizmos.color = Color.green;
        foreach(Sector sector in m_neighbours)
        {
            Gizmos.DrawLine(transform.position, sector.transform.position);
        }
    }

    public void SetGalaxy(Galaxy galaxy)
    {
        m_galaxyAttached = galaxy;
    }

    public bool HasNeighbours()
    {
        return m_neighbours.Count != 0;
    }

    public void AddNeighbour(Sector other, bool stop = false)
    {
        if(!m_neighbours.Contains(other))
            m_neighbours.Add(other);

        if(!stop)
            other.AddNeighbour(this, true);
    }

    public List<Sector> GetNeighbours()
    {
        return m_neighbours;
    }

    public Factions GetMainFaction()
    {
        uint neutral = 0;

        foreach(SolarSystem system in m_systems)
        {
            switch(system.GetMainFaction())
            {
                case Factions.NEUTRAL:
                    neutral++;
                    break;
            }
        }

        return Factions.NEUTRAL;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
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

    public void DisplaySolarSystems()
    {
        m_systemContainer.gameObject.SetActive(true);
        m_galaxyAttached.HideSectors();
    }

    public void HideSolarSystems()
    {
        m_systemContainer.gameObject.SetActive(false);
        m_galaxyAttached.ShowSectors();
    }

    private void CreateSystems()
    {
        GameObject tmp = new GameObject("Solar Systems");
        tmp.transform.parent = transform;
        m_systemContainer = tmp.transform;

        Vector2 totalArea = m_galaxyAttached.GetTotalArea();

        PoissonDiscSampler sampler = new PoissonDiscSampler(totalArea.x, totalArea.y, m_distanceBetweenSystem);
        List<Vector2> samples = new List<Vector2>();
        samples.AddRange(sampler.Samples());
        for (uint i = 0; i < m_numberOfSolarSystem; ++i)
        {
            GameObject system = Instantiate(m_galaxyAttached.GetSystemPrefab(), m_systemContainer);
            system.name = "Solar System " + (i + 1);
            SolarSystem obj = system.GetComponent<SolarSystem>();
            obj.SetSector(this);
            Vector3 position = samples[0] - m_galaxyAttached.GetOffset();
            samples.RemoveAt(0);
            if (samples.Count == 0)
                samples.AddRange(sampler.Samples());

            uint nbTry = 0;
            do
            {
                position = samples[0] - m_galaxyAttached.GetOffset(); 
                samples.RemoveAt(0);
                if (samples.Count == 0)
                    samples.AddRange(sampler.Samples());
                nbTry++;
                if (nbTry % 100 == 0)
                {
                    m_distanceBetweenSystem -= 0.1f;
                }
            }
            while (!IsAtAGoodDistance(position));
            system.transform.position = position;
            m_systems.Add(obj);

        }
    }

    private bool IsAtAGoodDistance(Vector3 position)
    {
        foreach (SolarSystem sys in m_systems)
        {
            if (Vector2.Distance(sys.transform.position, position) < m_distanceBetweenSystem)
                return false;
        }
        return true;
    }

    public void ShowInGalaxy()
    {
        m_renderer.enabled = true;
        m_collider.enabled = true;
    }

    public void HideInGalaxy()
    {
        m_animator.SetBool("inside",false);
        m_renderer.enabled = false;
        m_collider.enabled = false;
    }
}
