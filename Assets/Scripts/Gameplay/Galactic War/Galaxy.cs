using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy : MonoBehaviour
{
    [SerializeField]
    uint m_numberOfSectorMin = 5;
    [SerializeField]
    uint m_numberOfSectorMax = 20;

    [SerializeField]
    Sprite[] m_galaxySprites;
    [SerializeField]
    float m_distanceBetweenSectorMin = 1;

    [SerializeField]
    Vector2 m_topLeft;
    [SerializeField]
    Vector2 m_bottomRight;

    uint m_numberOfSector;
    List<Sector> m_sectors = new List<Sector>();

    SpriteRenderer m_renderer;

    Transform m_sectorsContainer;

    Vector2 m_sectorSize;

    [SerializeField]
    GameObject m_sectorPrefab;
    [SerializeField]
    GameObject m_systemPrefab;

	// Use this for initialization
	void Start ()
    {
        m_numberOfSector = (uint)Random.Range(m_numberOfSectorMin, m_numberOfSectorMax);

        if (m_numberOfSector % 2 != 0)
            m_numberOfSector--;

        m_renderer = GetComponentInChildren<SpriteRenderer>();
        GameObject tmp = new GameObject("Sectors");
        m_sectorsContainer = tmp.transform;
        m_sectorsContainer.parent = transform;

        //m_renderer.sprite = m_galaxySprites[Random.Range(0, m_galaxySprites.Length)];
        
        CreateSectors();
        SetNeighbours();
	}

    // Update is called once per frame
    void Update ()
    {
		
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Vector2 bottomLeft = new Vector2(m_topLeft.x, m_bottomRight.y);
        Vector2 topRight = new Vector2(m_bottomRight.x, m_topLeft.y);
        Gizmos.DrawLine(m_bottomRight,bottomLeft);
        Gizmos.DrawLine(bottomLeft, m_topLeft);
        Gizmos.DrawLine(m_topLeft, topRight);
        Gizmos.DrawLine(topRight, m_bottomRight);
    }

    private void CreateSectors()
    {
        Vector2 totalArea = GetTotalArea();

        PoissonDiscSampler sampler = new PoissonDiscSampler(totalArea.x, totalArea.y, m_distanceBetweenSectorMin);
        List<Vector2> samples = new List<Vector2>();
        samples.AddRange(sampler.Samples());
        for (uint i = 0; i < m_numberOfSector; ++i)
        {
            GameObject sector = Instantiate(m_sectorPrefab, m_sectorsContainer);
            sector.name = "Sector " + (i + 1);
            Sector obj = sector.GetComponent<Sector>();
            obj.SetGalaxy(this);
            Vector3 position = samples[0] - GetOffset();
            samples.RemoveAt(0);
            if (samples.Count == 0)
                samples.AddRange(sampler.Samples());

            uint nbTry = 0;
            do
            {
                position = samples[0] - GetOffset();
                samples.RemoveAt(0);
                if (samples.Count == 0)
                    samples.AddRange(sampler.Samples());
                nbTry++;
                if(nbTry % 100 == 0)
                {
                    m_distanceBetweenSectorMin -= 0.1f;
                }
            }
            while (!IsAtAGoodDistance(position));
            sector.transform.position = position;
            m_sectors.Add(obj);

        }
    }

    private bool IsAtAGoodDistance(Vector3 position)
    {
        foreach (Sector sec in m_sectors)
        {
            if (Vector2.Distance(sec.transform.position, position) < m_distanceBetweenSectorMin)
                return false;
        }
        return true;
    }

    private void SetNeighbours()
    {
        float distance = 0.5f;

        bool allSectorHasNeighbours = false;
        while(!allSectorHasNeighbours)
        {
            allSectorHasNeighbours = true;

            foreach (Sector sector in m_sectors)
            {
                if (sector.HasNeighbours())
                    continue;

                allSectorHasNeighbours = false;

                foreach (Sector other in m_sectors)
                {
                    if (sector.GetInstanceID() == other.GetInstanceID())
                        continue;

                    if(Vector3.Distance(sector.transform.position, other.transform.position) <= distance)
                    {
                        sector.AddNeighbour(other);
                    }
                }
            }

            distance += 0.5f;
        }

        ConnectNeighbours();
    }

    private void ConnectNeighbours()
    {
        /*while(!CheckIfAllSectorAreConnected())
        {
            foreach (Sector sector in m_sectors)
            {

            }

        }*/
        CheckIfAllSectorAreConnected();
    }

    private bool CheckIfAllSectorAreConnected()
    {
        List<Sector> sectorsToCheck = new List<Sector>();
        List<Sector> sectorsAlreadyChecked = new List<Sector>();
        sectorsToCheck.Add(m_sectors[0]);
        sectorsAlreadyChecked.Add(m_sectors[0]);

        for(int i = 0; i < sectorsToCheck.Count; ++i)
        {
            Sector sector = sectorsToCheck[i];
            List<Sector> neighbours = sector.GetNeighbours();
            for(int j = 0; j < neighbours.Count; ++j)
            {
                Sector neighbour = neighbours[j];
                if(!sectorsAlreadyChecked.Contains(neighbour))
                {
                    sectorsToCheck.Add(neighbour);
                    sectorsAlreadyChecked.Add(neighbour);
                }
            }
        }

        foreach(Sector sector in sectorsAlreadyChecked)
        {
            print(sector.gameObject.name);
        }

        print(m_sectors.Count + " " + sectorsAlreadyChecked.Count);
        return sectorsAlreadyChecked.Count == m_sectors.Count;
    }
    
    public Vector2 GetTotalArea()
    {
        Vector2 areaSize = m_topLeft - m_bottomRight;
        areaSize.x = Mathf.Abs(areaSize.x);
        areaSize.y = Mathf.Abs(areaSize.y);
        return areaSize;
    }

    public Vector2 GetOffset()
    {
        return new Vector2(m_bottomRight.x, m_topLeft.y);
    }

    public GameObject GetSystemPrefab()
    {
        return m_systemPrefab;
    }

    public void ShowSectors()
    {
        m_renderer.enabled = true;
        foreach(Sector sector in m_sectors)
        {
            sector.ShowInGalaxy();
        }
    }

    public void HideSectors()
    {
        m_renderer.enabled = false;
        foreach (Sector sector in m_sectors)
        {
            sector.HideInGalaxy();
        }
    }
}
