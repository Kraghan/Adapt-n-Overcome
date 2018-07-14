using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    Collider2D m_collider;
    ParticleSystem m_particles;
    SpriteRenderer[] m_renderers;

    Pool m_originPool = null;

    protected override void Start()
    {
        base.Start();
        m_particles = GetComponentInChildren<ParticleSystem>();
        if (m_particles)
        {
            m_particles.gameObject.SetActive(false);
            m_particles.Stop();
        }

        m_renderers = GetComponentsInChildren<SpriteRenderer>();

        m_collider = GetComponent<Collider2D>();

    }

    public override void Die()
    {
        m_manager.AddKill();

        if (m_particles)
        {
            m_particles.gameObject.SetActive(true);
            m_particles.Play();
        }

        foreach(SpriteRenderer renderer in m_renderers)
        {
            renderer.enabled = false;
        }

        m_collider.enabled = false;

        StartCoroutine(OnParticlesComplete());
    }

    IEnumerator OnParticlesComplete()
    {
        while(m_particles.IsAlive())
        {
            yield return new WaitForSeconds(0.5f);
        }

        if (m_originPool)
            m_originPool.ReleaseObject(gameObject);
        else
            Destroy(gameObject);
    }

    public void SetAlive()
    {
        m_collider.enabled = true;
        foreach (SpriteRenderer renderer in m_renderers)
        {
            renderer.enabled = false;
        }
        m_particles.gameObject.SetActive(false);
    }

    public void SetPool(Pool pool)
    {
        m_originPool = pool;
    }
}
