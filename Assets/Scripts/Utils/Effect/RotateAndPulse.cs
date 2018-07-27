using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndPulse : MonoBehaviour
{
    [SerializeField]
    float m_rotateSpeed;
    [SerializeField]
    float m_pulseTime = 0.5f;
    [SerializeField]
    float m_pulseStrength = 0.25f;

	// Use this for initialization
	void Start ()
    {
        float rotation = Random.Range(0, 360);
        transform.Rotate(new Vector3(0, 0, rotation));
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0, 0, m_rotateSpeed * Time.deltaTime));

        if(m_pulseTime > 0)
        {
            float scaleFactor = 1 + Mathf.Lerp(0, m_pulseStrength, Mathf.PingPong(Time.time, m_pulseTime));

            transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
        }
    }
}
