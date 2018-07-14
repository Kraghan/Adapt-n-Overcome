using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{

    List<GameObject> m_objectInPool = new List<GameObject>();
    List<GameObject> m_objectInUse = new List<GameObject>();

    GameObject m_model = null;
	
    public void FillPoolWith(GameObject objectModel, uint numberOfObject)
    {
        for(int i = m_objectInPool.Count - 1; i >= 0; --i)
        {
            Destroy(m_objectInPool[i]);
        }
        m_objectInPool.Clear();

        for (int i = m_objectInUse.Count - 1; i >= 0; --i)
        {
            Destroy(m_objectInUse[i]);
        }
        m_objectInUse.Clear();

        m_model = objectModel;

        for (int i = 0; i < numberOfObject; ++i)
        {
            GameObject newObject = Instantiate(m_model);
            newObject.transform.parent = transform;
            newObject.SetActive(false);
            m_objectInPool.Add(newObject);
        }
    }

    public GameObject PickObject()
    {        
        if (m_objectInPool.Count == 0)
        {
            for (int i = 0; i < 10; ++i)
            {
                GameObject newObject = Instantiate(m_model);
                newObject.transform.parent = transform;
                newObject.SetActive(false);
                m_objectInPool.Add(newObject);
            }
        }

        GameObject obj = m_objectInPool[0];
        m_objectInPool.RemoveAt(0);
        obj.SetActive(true);
        m_objectInUse.Add(obj);
        return obj;
    }

    public void ReleaseObject(GameObject objectToRelease)
    {
        for(int i = 0; i < m_objectInUse.Count; ++i)
        {
            if (objectToRelease.GetInstanceID() == m_objectInUse[i].GetInstanceID())
            {
                m_objectInUse.RemoveAt(i);
                objectToRelease.SetActive(false);
                m_objectInPool.Add(objectToRelease);
            }
        }
    }
}
